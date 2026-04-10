namespace MealManagement.Application.Interfaces.Services;

public interface IMealOptionsService
{
	Task UpdateAsync(string mealId, IReadOnlyList<MealOptionGroup> mealOptionsDb, IReadOnlyList<MealOptionRequest> mealOptionsReq, CancellationToken cancellationToken);
}
