namespace MealManagement.Application.Contracts.MealOptions.Validators;

public class MealOptionValidator : AbstractValidator<MealOptionRequest>
{
	public MealOptionValidator()
	{
		RuleFor(x => x.Name)
			.NotEmpty()
			.MinimumLength(2)
			.MaximumLength(30);

		RuleFor(x => x.Items)
			.Must(x => 
				{
   					var list = x.ToList();
					return list.Count >= 1 && list.Count <= 10 && list.Count == list.DistinctBy(item => item.Name).Count();
				}
			)
			.When(x => x.Items is not null && x.Items.Any())
			.WithMessage("Option items must be between 1 and 10 with unique names.");

		RuleForEach(x => x.Items)
			.SetValidator(new OptionItemValidator())
			.When(x => x.Items is not null && x.Items.Any());
	}
}