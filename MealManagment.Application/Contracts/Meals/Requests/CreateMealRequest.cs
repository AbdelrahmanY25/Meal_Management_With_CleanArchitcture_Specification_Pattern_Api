namespace MealManagment.Application.Contracts.Meals.Requests;

public record CreateMealRequest
(
	string Name,
	string Description,
	decimal Price,
	bool HasOptionGroup,
	IEnumerable<MealOptionRequest> Options
);