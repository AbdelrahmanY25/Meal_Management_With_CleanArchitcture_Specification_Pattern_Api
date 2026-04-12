namespace MealManagement.Application.Specifications.MealOptionsSpecifications;

internal class GetOptionsByMealIdSpec : Specification<MealOption>
{
	internal GetOptionsByMealIdSpec(string mealId)
	{		
		AddFilter(opt => opt.MealId == mealId);

		AddInclude(opt => opt.Items);
	}
}
