namespace MealManagement.Application.Specifications.MealOptionsSpecifications;

internal class GetOptionsByMealIdSpec : Specification<MealOptionGroup>
{
	internal GetOptionsByMealIdSpec(string mealId)
	{		
		Filter = opt => opt.MealId == mealId;

		Includes = [op => op.Items];
	}
}
