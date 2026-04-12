namespace MealManagement.Application.Specifications.MealsSpecifications;

internal static class MealSelectors
{
	internal static readonly Expression<Func<Meal, MealResponse>> MealResponseSelector =
		m => new MealResponse(
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
					i.IsPopular
				)).ToList()
			)).ToList()
		);

}
