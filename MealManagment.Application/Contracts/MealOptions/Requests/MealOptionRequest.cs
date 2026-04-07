namespace MealManagment.Application.Contracts.MealOptions.Requests;

public record MealOptionRequest
(
	string Name,
	IEnumerable<OptionItemRequest> Items
);