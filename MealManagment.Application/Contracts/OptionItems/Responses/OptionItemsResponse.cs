namespace MealManagment.Application.Contracts.OptionItems.Responses;

public record OptionItemsResponse
(
	string Id,
	string Name,
	decimal Price,
	bool IsPobular
);