namespace MealManagment.Application.Contracts.OptionItems.Requests;

public record OptionItemRequest
(
	string Name,
	decimal Price,
	bool IsPobular
);