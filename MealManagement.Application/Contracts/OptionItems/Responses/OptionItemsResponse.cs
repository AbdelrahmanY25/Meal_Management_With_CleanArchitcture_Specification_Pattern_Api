namespace MealManagement.Application.Contracts.OptionItems.Responses;

public sealed record OptionItemsResponse
(
	string Id,
	string Name,
	decimal Price,
	bool IsPobular
);