namespace MealManagement.Application.Specifications.MealsSpecifications;

internal class MealByIdSpec : Specification<Meal>
{
	internal MealByIdSpec(string mealId)
	{
		AsNoTracking = false;
		
		AddFilter(m => m.Id == mealId);
	}
}