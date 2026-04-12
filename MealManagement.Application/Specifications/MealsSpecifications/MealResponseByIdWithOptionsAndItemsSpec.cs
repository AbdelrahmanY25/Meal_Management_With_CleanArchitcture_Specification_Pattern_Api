namespace MealManagement.Application.Specifications.MealsSpecifications;

internal sealed class MealResponseByIdWithOptionsAndItemsSpec : Specification<Meal, MealResponse>
{
	internal MealResponseByIdWithOptionsAndItemsSpec(string mealId)
	{
		AddFilter(m => m.Id == mealId);

		Selector = MealSelectors.MealResponseSelector;
	}
}