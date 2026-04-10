namespace MealManagement.Application.Specifications.MealsSpecifications;

internal class MealByIdSpec : Specification<Meal, Meal>
{
	internal MealByIdSpec(string mealId)
	{
		AsNoTracking = false;
		
		Filter = m => m.Id == mealId;

		Selector = m => new Meal
		{
			Id = m.Id,
			Name = m.Name,
			Description = m.Description,
			Price = m.Price,
			HasOptionGroup = m.HasOptionGroup,
			MealOptionGroups = m.MealOptionGroups.Select(g => new MealOptionGroup
			{
				Id = g.Id,
				MealId = g.MealId,
				Name = g.Name,
				Items = g.Items.Select(i => new OptionGroupItems
				{
					Id = i.Id,
					OptionGroupId = i.OptionGroupId,
					Name = i.Name,
					Price = i.Price,
					IsPopular = i.IsPopular
				}).ToList()
			}).ToList()
		};
	}
}