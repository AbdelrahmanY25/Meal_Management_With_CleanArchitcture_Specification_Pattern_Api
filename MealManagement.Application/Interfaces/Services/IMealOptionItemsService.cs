namespace MealManagement.Application.Interfaces.Services;

public interface IMealOptionItemsService
{
	Task UpdateAsync(string optionId, IReadOnlyList<OptionGroupItems> mealOptionItemsDb, IReadOnlyList<OptionItemRequest> mealOptionItemsReq, CancellationToken cancellationToken);
	void DeleteMany(IEnumerable<OptionGroupItems> mealOptionItemsDb);
}
