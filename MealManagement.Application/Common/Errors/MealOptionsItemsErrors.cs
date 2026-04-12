namespace MealManagement.Application.Common.Errors;

public static class MealOptionsItemsErrors
{
	public static readonly Error InvalidMealOptionsItemsCount =
		new("MealOptionsItems.InvalidMealOptionsItemsCount",
		"The meal option items count is invalid must be between 1 and 10 per option.",
		StatusCodes.Status400BadRequest);

	public static readonly Error DuplicatedItemName =
		new("MealOptionsItems.DuplicatedItemName",
		"Meal option items must be with unique names.",
		StatusCodes.Status409Conflict);
}
