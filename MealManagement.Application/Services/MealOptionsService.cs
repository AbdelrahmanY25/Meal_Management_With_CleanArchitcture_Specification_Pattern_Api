namespace MealManagement.Application.Services;

internal class MealOptionsService(IRepository<MealOptionGroup> mealOptionsRepository, IMealOptionItemsService itemsService) : IMealOptionsService
{
	public async Task UpdateAsync(string mealId,
		IReadOnlyList<MealOptionGroup> mealOptionsDb,
		IReadOnlyList<MealOptionRequest> mealOptionsReq,
		CancellationToken cancellationToken)
	{
		mealOptionsDb ??= [];
		mealOptionsReq ??= [];

		var dbNames = mealOptionsDb.ToDictionary(r => r.Name);

		var reqNames = mealOptionsReq.ToDictionary(r => r.Name);

		var deletedOptions = mealOptionsDb.Where(db => !reqNames.ContainsKey(db.Name)).ToList();

		if (deletedOptions.Count > 0)
			DeleteMany(deletedOptions);

		var updatedOptionsDb = mealOptionsDb.Where(db => reqNames.ContainsKey(db.Name)).ToList();
		
		foreach (var optionDb in updatedOptionsDb)
		{
			var matchingReq = reqNames[optionDb.Name];

			await itemsService
				.UpdateAsync(optionDb.Id, (IReadOnlyList<OptionGroupItems>)optionDb.Items, matchingReq.Items.ToList() ?? [], cancellationToken);
		}
		
		var newOptions = mealOptionsReq.Where(req => !dbNames.ContainsKey(req.Name)).ToList();

		if (newOptions.Count > 0)
			await AddManyAsync(mealId, newOptions, cancellationToken);
	}

	private async Task AddManyAsync(string mealId, IReadOnlyList<MealOptionRequest> mealOptionsReq, CancellationToken cancellationToken)
	{
		IReadOnlyList<MealOptionGroup> newOptions = [.. mealOptionsReq.Select(x => new MealOptionGroup
			{
				MealId = mealId,
				Name = x.Name,
				Items = [.. x.Items.Select(i => new OptionGroupItems
				{
					Name = i.Name,
					IsPopular = i.IsPopular,
					Price = i.Price
				})]
			})];

		await mealOptionsRepository.AddRangeAsync(newOptions, cancellationToken);
	}

	private void DeleteMany(IReadOnlyList<MealOptionGroup> mealOptionsDb)
	{
		itemsService.DeleteMany([.. mealOptionsDb.SelectMany(x => x.Items)]);
		mealOptionsRepository.DeleteRange(mealOptionsDb);
	}
}