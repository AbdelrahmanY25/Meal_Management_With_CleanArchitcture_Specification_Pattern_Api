namespace MealManagement.Application.Interfaces.Services;

public interface IMealOptionItemsService
{
	Task UpdateAsync(string optionId, IReadOnlyList<MealOptionsItem> mealOptionItemsDb, IReadOnlyList<OptionItemRequest> mealOptionItemsReq, CancellationToken cancellationToken);
	void DeleteMany(IEnumerable<MealOptionsItem> mealOptionItemsDb);
}
