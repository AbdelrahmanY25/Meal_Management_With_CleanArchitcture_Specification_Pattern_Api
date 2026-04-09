namespace MealManagement.Application.Common.Errors;

public static class MealErrors
{
	public static readonly Error MealNotFound = 
		new("Meal.MealNotFound", "The meal was not found.", StatusCodes.Status404NotFound);

	public static readonly Error DuplicatedMealName = 
		new("Meal.DuplicatedMealName", "A meal with the same name already exists.", StatusCodes.Status409Conflict);

	public static readonly Error InvalidMealOptions = 
		new("Meal.InvalidMealOptions", "The meal options are invalid.", StatusCodes.Status400BadRequest);
}
