namespace MealManagment.Application.Interfaces.Services;

public interface IMealOptionsService
{
	Task UpdateAsync(bool isMealDbHasOptions, string mealId, IEnumerable<MealOptionGroup> meaOptionsDb, IEnumerable<MealOptionRequest> mealOptionsReq, CancellationToken cancellationToken);
}
