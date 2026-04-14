namespace MealManagement.Application.Contracts.Meals.Requests;

public sealed record MealRequest
(
	string Name,
	string Description,
	decimal Price,
	bool HasOptions,
	IReadOnlyList<MealOptionRequest>? Options
);