namespace MealManagement.Application.Specifications.MealsSpecifications;

internal class MealByIdSpec : Specification<Meal>
{
	internal MealByIdSpec(string mealId)
	{
		AsNoTracking = false;
		
		Filter = m => m.Id == mealId;

		Includes = [m => m.MealOptionGroups];
		IncludeStrings = ["MealOptionGroups.Items"];
	}
}