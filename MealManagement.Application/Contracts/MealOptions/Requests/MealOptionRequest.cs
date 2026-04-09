namespace MealManagement.Application.Contracts.MealOptions.Requests;

public sealed record MealOptionRequest
(
	string Name,
	IEnumerable<OptionItemRequest> Items
);