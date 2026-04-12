namespace MealManagement.Application.Contracts.OptionItems.Validators;

public class OptionItemValidator : AbstractValidator<OptionItemRequest>
{
	public OptionItemValidator()
	{
		RuleFor(x => x.Name)
			.NotEmpty()
			.MinimumLength(2)
			.MaximumLength(30);

		RuleFor(x => x.Price)
			.GreaterThanOrEqualTo(0);
	}
}