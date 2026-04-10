namespace MealManagement.Application.Interfaces.Services;

public interface IMealOptionItemsService
{
	Task UpdateAsync(string mealOptionGroupId, IReadOnlyList<OptionGroupItems> mealOptionItemsDb, IReadOnlyList<OptionItemRequest> mealOptionItemsReq, CancellationToken cancellationToken);
	void DeleteMany(IReadOnlyList<OptionGroupItems> mealOptionItemsDb);
}
