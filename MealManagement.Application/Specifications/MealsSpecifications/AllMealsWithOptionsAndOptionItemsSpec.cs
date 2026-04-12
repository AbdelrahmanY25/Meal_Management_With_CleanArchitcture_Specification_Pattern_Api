namespace MealManagement.Application.Specifications.MealsSpecifications;

internal class AllMealsWithOptionsAndOptionItemsSpec : Specification<Meal, MealResponse>
{
	internal AllMealsWithOptionsAndOptionItemsSpec()
	{
		Selector = MealSelectors.MealResponseSelector;
	}
}