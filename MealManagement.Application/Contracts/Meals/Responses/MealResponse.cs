namespace MealManagement.Application.Contracts.Meals.Responses;

public sealed record MealResponse
(
	string Id,
	string Name,
	string Description,
	decimal Price,
	bool HasOptionGroup,
	IReadOnlyCollection<MealOptionsResponse> Options
);