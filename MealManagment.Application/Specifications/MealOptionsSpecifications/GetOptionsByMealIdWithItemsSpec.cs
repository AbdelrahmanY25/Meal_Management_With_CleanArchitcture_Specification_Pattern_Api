namespace MealManagment.Application.Specifications.MealOptionsSpecifications;

public class GetOptionsByMealIdWithItemsSpec : Specification<MealOptionGroup>
{
	public GetOptionsByMealIdWithItemsSpec(string mealId)
	{
		AsNoTracking = false;
		
		Filter = opt => opt.MealId == mealId;

		Includes = [opt => opt.Items];
	}
}
