namespace MealManagement.Application.Contracts.MealOptions.Validators;

internal class CreateMealOptionValidator : AbstractValidator<MealOptionRequest>
{
	public CreateMealOptionValidator()
	{
		RuleFor(x => x.Name)
			.NotEmpty()
			.MinimumLength(2)
			.MaximumLength(30);

		RuleFor(x => x.Items)
			.Must(x => 
				(x.Any() && x.Count() <= 20) &&
				(x.Count() == x.DistinctBy(x => x.Name).Count())
			)
			.When(x => x.Items is not null && x.Items.Any())
			.WithMessage("Option items must be between 1 and 20 with unique names and display orders.");

		RuleForEach(x => x.Items)
			.SetValidator(new CreateOptionItemValidator())
			.When(x => x.Items is not null && x.Items.Any());
	}
}