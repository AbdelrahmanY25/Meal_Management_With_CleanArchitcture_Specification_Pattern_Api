namespace MealManagement.Application.Contracts.MealOptions.Responses;

public sealed record MealOptionsResponse
(
	string Id,
	string Name,
	IReadOnlyCollection<OptionItemsResponse> Items
);