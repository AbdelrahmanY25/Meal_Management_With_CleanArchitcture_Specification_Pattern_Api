namespace MealManagment.Application.Specifications.MealsSpecifications;

public class AllMealsWithOptionsAdnOptionItemsSpec : Specification<Meal, MealResponse>
{
	public AllMealsWithOptionsAdnOptionItemsSpec()
	{
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