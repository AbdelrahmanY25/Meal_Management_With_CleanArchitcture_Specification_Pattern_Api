namespace MealManagement.Application.Services;

internal class MealOptionItemsService(IRepository<MealOptionsItem> itemsRepository) : IMealOptionItemsService
{
	public async Task UpdateAsync(string optionId,
		IReadOnlyList<MealOptionsItem> mealOptionItemsDb,
		IReadOnlyList<OptionItemRequest> mealOptionItemsReq,
		CancellationToken cancellationToken)
	{
		var dbName = mealOptionItemsDb.ToDictionary(db => db.Name);
		var reqName = mealOptionItemsReq.ToDictionary(req => req.Name);

		var deletedItems = mealOptionItemsDb.Where(db => !reqName.ContainsKey(db.Name)).ToList();

		if (deletedItems.Count > 0)
			DeleteMany(deletedItems);

		var updatedItemsDb = mealOptionItemsDb
			.Where(db => reqName.ContainsKey(db.Name) && HasChanged(db, reqName))
			.ToList();

		foreach (var itemDb in updatedItemsDb)
		{
			var matchingReq = reqName[itemDb.Name];

			itemDb.Price = matchingReq.Price;
			itemDb.IsPopular = matchingReq.IsPopular;
		}

		var newItems = mealOptionItemsReq.Where(req => !dbName.ContainsKey(req.Name)).ToList();

		if (newItems.Count > 0)
			AddMany(optionId, newItems);
	}

	public void DeleteMany(IEnumerable<MealOptionsItem> mealOptionItemsDb)
	{
		itemsRepository.DeleteRange(mealOptionItemsDb);
	}
	


	private void AddMany(string mealOptionGroupId, IReadOnlyList<OptionItemRequest> mealOptionItemsReq)
	{
		IEnumerable<MealOptionsItem> newItems = [.. mealOptionItemsReq.Select(x => new MealOptionsItem
		{
			OptionGroupId = mealOptionGroupId,
			Name = x.Name,
			IsPopular = x.IsPopular,
			Price = x.Price
		})];

		itemsRepository.AddRange(newItems);
	}
	
	private static bool HasChanged(MealOptionsItem db, Dictionary<string, OptionItemRequest> reqName) =>
		reqName[db.Name].Price != db.Price ||
		reqName[db.Name].IsPopular != db.IsPopular;
}