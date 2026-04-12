namespace MealManagement.Application.Services;

internal class MealOptionItemsService(IRepository<OptionGroupItems> itemsRepository) : IMealOptionItemsService
{
	public async Task UpdateAsync(string optionId,
		IReadOnlyList<OptionGroupItems> mealOptionItemsDb,
		IReadOnlyList<OptionItemRequest> mealOptionItemsReq,
		CancellationToken cancellationToken)
	{
		var dbName = mealOptionItemsDb.ToDictionary(db => db.Name);
		var reqName = mealOptionItemsReq.ToDictionary(req => req.Name);

		var deletedItems = mealOptionItemsDb.Where(db => !reqName.ContainsKey(db.Name)).ToList();

		if (deletedItems.Count > 0)
			DeleteMany(deletedItems);

		var updatedItemsDb = mealOptionItemsDb
			.Where(db => reqName.ContainsKey(db.Name) &&
				(reqName[db.Name].Price != db.Price || reqName[db.Name].IsPopular != db.IsPopular))
			.ToList();

		foreach (var itemDb in updatedItemsDb)
		{
			var matchingReq = reqName[itemDb.Name];
			itemDb.Price = matchingReq.Price;
			itemDb.IsPopular = matchingReq.IsPopular;
		}	
		
		var newItems = mealOptionItemsReq.Where(req => !dbName.ContainsKey(req.Name)).ToList();

		if (newItems.Count > 0)
			await AddManyAsync(optionId, newItems, cancellationToken);
	}

	private async Task AddManyAsync(string mealOptionGroupId, IReadOnlyList<OptionItemRequest> mealOptionItemsReq, CancellationToken cancellationToken)
	{
		IEnumerable<OptionGroupItems> newItems = [.. mealOptionItemsReq.Select(x => new OptionGroupItems
		{
			OptionGroupId = mealOptionGroupId,
			Name = x.Name,
			IsPopular = x.IsPopular,
			Price = x.Price
		})];

		await itemsRepository.AddRangeAsync(newItems, cancellationToken);
	}

	public void DeleteMany(IEnumerable<OptionGroupItems> mealOptionItemsDb)
	{
		itemsRepository.DeleteRange(mealOptionItemsDb);
	}
}