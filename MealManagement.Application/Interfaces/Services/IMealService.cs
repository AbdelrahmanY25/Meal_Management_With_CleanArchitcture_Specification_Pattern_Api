namespace MealManagement.Application.Interfaces.Services;

public interface IMealService
{
	Task<Result<MealResponse>> AddAsync(CreateMealRequest request, CancellationToken cancellationToken = default);
	Task<Result> UpdateAsync(string mealId, UpdateMealRequest request, CancellationToken cancellationToken = default);
	Task<Result<MealResponse>> GetMealAsync(string mealId, CancellationToken cancellationToken = default);
	Task<IReadOnlyCollection<MealResponse>> GetAllAsync(CancellationToken cancellationToken = default);
}