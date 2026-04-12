namespace MealManagement.Application.Contracts.Meals.Requests;

public sealed record UpdateMealRequest
(
	string Name,
	string Description,
	decimal Price,
	bool HasOptionGroup,
	IReadOnlyList<MealOptionRequest> Options
);