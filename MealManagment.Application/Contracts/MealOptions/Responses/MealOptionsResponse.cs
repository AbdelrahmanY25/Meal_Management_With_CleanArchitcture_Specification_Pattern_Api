namespace MealManagment.Application.Contracts.MealOptions.Responses;

public record MealOptionsResponse
(
	string Id,
	string Name,
	IReadOnlyCollection<OptionItemsResponse> Items
);