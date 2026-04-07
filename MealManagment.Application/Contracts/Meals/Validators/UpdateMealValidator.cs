namespace MealManagment.Application.Contracts.Meals.Validators;

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
			.NotEmpty()
			.GreaterThanOrEqualTo(0);

		RuleFor(m => m.HasOptionGroup)
			.Must((mealRequest, hasGroup) =>
			{
				return hasGroup == (mealRequest.Options is not null && mealRequest.Options.Any());
			})
			.WithMessage("HasOptionGroup must be true if options are provided, and false if no options are provided.");

		RuleFor(m => m.Options)
			.Must(o =>
				(o.Any() && o.Count() <= 20) &&
				(o.Count() == o.DistinctBy(x => x.Name).Count())
			)
			.When(m => m.Options is not null && m.Options.Any())
			.WithMessage("Option groups must be with unique names and display orders, and max 25 options.");

		RuleForEach(m => m.Options)
			.SetValidator(new CreateMealOptionValidator())
			.When(m => m.Options is not null && m.Options.Any());
	}
}
