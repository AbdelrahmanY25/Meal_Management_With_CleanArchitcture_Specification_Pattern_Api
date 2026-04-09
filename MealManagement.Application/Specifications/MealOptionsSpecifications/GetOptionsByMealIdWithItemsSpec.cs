namespace MealManagement.Application.Specifications.MealOptionsSpecifications;

internal class GetOptionsByMealIdWithItemsSpec : Specification<MealOptionGroup>
{
	internal GetOptionsByMealIdWithItemsSpec(string mealId)
	{
		AsNoTracking = false;
		
		Filter = opt => opt.MealId == mealId;

		Includes = [opt => opt.Items];
	}
}
