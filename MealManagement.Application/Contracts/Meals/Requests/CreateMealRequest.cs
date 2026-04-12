namespace MealManagement.Application.Contracts.Meals.Requests;

public sealed record CreateMealRequest
(
	string Name,
	string Description,
	decimal Price,
	bool HasOptions,
	IReadOnlyList<MealOptionRequest>? Options
);