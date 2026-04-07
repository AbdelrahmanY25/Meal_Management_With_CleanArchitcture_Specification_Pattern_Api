namespace MealManagment.Application.Interfaces.Services;

public interface IMealOptionItemsService
{
	Task UpdateAsync(string mealOptionGroupId, IEnumerable<OptionGroupItems> mealOptionItemsDb, IEnumerable<OptionItemRequest> mealOptionItemsReq, CancellationToken cancellationToken);
	void DeleteMany(IEnumerable<OptionGroupItems> mealOptionItemsDb);
}
