namespace MealManagement.Application.Common.Errors;

public static class MealOptionsErrors
{
	public static readonly Error InvalidMealOptionsCount = 
		new("MealOptions.InvalidMealOptionsCount",
		"The meal options count is invalid must be between 1 and 20 per meal.",
		StatusCodes.Status400BadRequest);

	public static readonly Error DuplicatedOptionName =
		new("MealOptions.DuplicatedOptionName",
		"Meal options must be with unique names.",
		StatusCodes.Status409Conflict);
}