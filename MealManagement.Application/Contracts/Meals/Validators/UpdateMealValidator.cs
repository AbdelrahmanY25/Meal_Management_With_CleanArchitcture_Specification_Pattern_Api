namespace MealManagement.Application.Contracts.Meals.Validators;

public class UpdateMealValidator : AbstractValidator<UpdateMealRequest>
{
	public UpdateMealValidator()
	{
		RuleFor(x => x.Name)
			.NotEmpty()
			.MinimumLength(2)
			.MaximumLength(30);

		RuleFor(m => m.Description)
			.NotEmpty()
			.MinimumLength(2)
			.MaximumLength(100);

		RuleFor(m => m.Price)
			.GreaterThanOrEqualTo(0);

		RuleFor(m => m.HasOptions)
			.Must((mealRequest, hasGroup) =>
			{
				return hasGroup == (mealRequest.Options is not null && mealRequest.Options.Any());
			})
			.WithMessage("HasOptionGroup must be true if options are provided, and false if no options are provided.");

		RuleFor(m => m.Options)
			.Must(o =>
				{
					var list = o!.ToList();
					return list.Count >= 1 && list.Count <= 20 && list.Count == list.DistinctBy(opt => opt.Name).Count();
				}
			)
			.When(m => m.Options is not null && m.Options.Any())
			.WithMessage("Meal options must be with unique names, and max 20 options.");

		RuleForEach(m => m.Options)
			.SetValidator(new MealOptionValidator())
			.When(m => m.Options is not null && m.Options.Any());
	}
}
