namespace MealManagement.Application.Services;

internal class MealOptionItemsService(IRepository<MealOptionsItem> itemsRepository) : IMealOptionItemsService
{
	private readonly IRepository<MealOptionsItem> _itemsRepository = itemsRepository;

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

			itemDb.Update(matchingReq.Price, matchingReq.IsPopular);
		}

		var newItems = mealOptionItemsReq.Where(req => !dbName.ContainsKey(req.Name)).ToList();

		if (newItems.Count > 0)
			AddMany(optionId, newItems);
	}

	public void DeleteMany(IEnumerable<MealOptionsItem> mealOptionItemsDb)
	{
		_itemsRepository.DeleteRange(mealOptionItemsDb);
	}
	


	private void AddMany(string mealOptionGroupId, IReadOnlyList<OptionItemRequest> mealOptionItemsReq)
	{
		var newItems = mealOptionItemsReq.Adapt<List<MealOptionsItem>>();

		foreach (var item in newItems)
			item.AssignToOption(mealOptionGroupId);

		_itemsRepository.AddRange(newItems);
	}
	
	private static bool HasChanged(MealOptionsItem db, Dictionary<string, OptionItemRequest> reqName) =>
		reqName[db.Name].Price != db.Price ||
		reqName[db.Name].IsPopular != db.IsPopular;
}