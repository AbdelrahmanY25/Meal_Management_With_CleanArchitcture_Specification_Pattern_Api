namespace MealManagment.Application.Services;

public class MealOptionItemsService(IRepository<OptionGroupItems> itemsRepository) : IMealOptionItemsService
{
	private readonly IRepository<OptionGroupItems> _itemsRepository = itemsRepository;

	public async Task UpdateAsync(string mealOptionGroupId,
		IEnumerable<OptionGroupItems> mealOptionItemsDb,
		IEnumerable<OptionItemRequest> mealOptionItemsReq,
		CancellationToken cancellationToken)
	{
		var deletedItems = mealOptionItemsDb!.Where(db => mealOptionItemsReq!.All(req => req.Name != db.Name)).ToList();

		if (deletedItems.Count > 0)
			DeleteMany(deletedItems);

		var updatedItemsDb = mealOptionItemsDb!.Where(db => mealOptionItemsReq!.Any(req => req.Name == db.Name)).ToList();

		var updatedItemsReq = mealOptionItemsReq!.Where(req => mealOptionItemsDb!.Any(db => db.Name == req.Name)).ToList();

		if (updatedItemsDb.Count > 0 && updatedItemsReq.Count > 0) 
		{
			updatedItemsDb.ForEach(db =>
			{
				var req = updatedItemsReq.FirstOrDefault(r => r.Name == db.Name);
				
				if (req is not null)
				{
					db.Price = req.Price;
					db.IsPobular = req.IsPobular;
				}
			});
		}

		var newItems = mealOptionItemsReq!.Where(req => mealOptionItemsDb!.All(db => db.Name != req.Name)).ToList();

		if (newItems.Count > 0)
			await AddManyAsync(mealOptionGroupId, newItems, cancellationToken);
	}

	private async Task AddManyAsync(string mealOptionGroupId, IEnumerable<OptionItemRequest> mealOptionItemsReq, CancellationToken cancellationToken)
	{
		IEnumerable<OptionGroupItems> newItems = [.. mealOptionItemsReq.Select(x => new OptionGroupItems
		{
			OptionGroupId = mealOptionGroupId,
			Name = x.Name,
			IsPobular = x.IsPobular,
			Price = x.Price
		})];

		await _itemsRepository.AddRangeAsync(newItems, cancellationToken);
	}

	public void DeleteMany(IEnumerable<OptionGroupItems> mealOptionItemsDb)
	{
		_itemsRepository.DeleteRange(mealOptionItemsDb);
	}
}
