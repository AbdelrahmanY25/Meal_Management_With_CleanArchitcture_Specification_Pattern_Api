namespace MealManagement.Application.Contracts.MealOptions.Requests;

public sealed record MealOptionRequest
(
	string Name,
	IReadOnlyList<OptionItemRequest> Items
);