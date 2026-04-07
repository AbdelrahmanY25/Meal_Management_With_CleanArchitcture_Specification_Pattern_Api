namespace MealManagment.Application.Specifications.MealsSpecifications;

public sealed class MealResponseByIdWithOptionsAndItemsSpec : Specification<Meal, MealResponse>
{
	public MealResponseByIdWithOptionsAndItemsSpec(string mealId)
	{
		Filter = m => m.Id == mealId;

		Selector = m => new MealResponse(
			m.Id,
			m.Name,
			m.Description,
			m.Price,
			m.HasOptionGroup,
			m.MealOptionGroups.Select(g => new MealOptionsResponse(
				g.Id,
				g.Name,
				g.Items.Select(i => new OptionItemsResponse(
					i.Id,
					i.Name,
					i.Price,
					i.IsPobular
				)).ToList()
			)).ToList()
		);
	}
}