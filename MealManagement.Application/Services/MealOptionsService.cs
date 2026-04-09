namespace MealManagement.Application.Services;

internal class MealOptionsService(IRepository<MealOptionGroup> mealOptionsReqpository, IMealOptionItemsService itemsService) : IMealOptionsService
{
	private readonly IMealOptionItemsService _itemsService = itemsService;
	private readonly IRepository<MealOptionGroup> _mealOptionsReqpository = mealOptionsReqpository;

	public async Task UpdateAsync(bool isMealDbHasOptions, string mealId,
		IEnumerable<MealOptionGroup> mealOptionsDb,
		IEnumerable<MealOptionRequest> mealOptionsReq,
		CancellationToken cancellationToken)
	{
		var deletedOptions = mealOptionsDb!.Where(db => mealOptionsReq!.All(req => req.Name != db.Name)).ToList();

		if (deletedOptions.Count > 0)
			DeleteMany(deletedOptions);

		var updatedOptionsDb = mealOptionsDb!.Where(db => mealOptionsReq!.Any(req => req.Name == db.Name)).ToList();

		var updatedOptionsReq = mealOptionsReq!.Where(req => mealOptionsDb!.Any(db => db.Name == req.Name)).ToList();
		
		if (updatedOptionsDb.Count > 0 && updatedOptionsReq.Count > 0)
		{
			foreach (var optionDb in updatedOptionsDb) {
				await _itemsService.UpdateAsync(optionDb.Id, updatedOptionsDb.SelectMany(x => x.Items), updatedOptionsReq.SelectMany(x => x.Items), cancellationToken);
			}
		}

		var newOptions = mealOptionsReq!.Where(req => mealOptionsDb!.All(db => db.Name != req.Name)).ToList();

		if (newOptions.Count > 0)
			await AddManyAsync(mealId, newOptions, cancellationToken);
	}

	private async Task AddManyAsync(string mealId, IEnumerable<MealOptionRequest> mealOptionsReq, CancellationToken cancellationToken)
	{
		IEnumerable<MealOptionGroup> newOptions = [.. mealOptionsReq.Select(x => new MealOptionGroup
			{
				MealId = mealId,
				Name = x.Name,
				Items = [.. x.Items.Select(i => new OptionGroupItems
				{
					Name = i.Name,
					IsPobular = i.IsPobular,
					Price = i.Price
				})]
			})];

		await _mealOptionsReqpository.AddRangeAsync(newOptions, cancellationToken);
	}

	private void DeleteMany(IEnumerable<MealOptionGroup> mealOptionsDb)
	{
		_itemsService.DeleteMany(mealOptionsDb.SelectMany(x => x.Items));
		_mealOptionsReqpository.DeleteRange(mealOptionsDb);
	}
}