namespace MealManagement.Application.Interfaces.Services;

public interface IMealOptionsService
{
	Task<Result> UpdateAsync(string mealId, IReadOnlyList<MealOptionRequest> mealOptionsReq, CancellationToken cancellationToken);
}