namespace MealManagement.Application.Contracts.OptionItems.Requests;

public sealed record OptionItemRequest
(
	string Name,
	decimal Price,
	bool IsPopular
);