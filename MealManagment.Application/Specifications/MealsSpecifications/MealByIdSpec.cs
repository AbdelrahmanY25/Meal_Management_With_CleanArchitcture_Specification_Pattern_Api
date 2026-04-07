namespace MealManagment.Application.Specifications.MealsSpecifications;

public class MealByIdSpec : Specification<Meal>
{
	public MealByIdSpec(string mealId)
	{
		AsNoTracking = false;
		
		Filter = m => m.Id == mealId;

		Includes = [m => m.MealOptionGroups];
		IncludeStrings = ["MealOptionGroups.Items"];
	}
}