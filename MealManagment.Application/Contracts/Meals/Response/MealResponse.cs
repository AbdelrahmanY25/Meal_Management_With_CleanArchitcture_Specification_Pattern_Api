namespace MealManagment.Application.Contracts.Meals.Response;

public record MealResponse
(
	string Id,
	string Name,
	string Description,
	decimal Price,
	bool HasOptionGroup,
	IReadOnlyCollection<MealOptionsResponse> Options
);