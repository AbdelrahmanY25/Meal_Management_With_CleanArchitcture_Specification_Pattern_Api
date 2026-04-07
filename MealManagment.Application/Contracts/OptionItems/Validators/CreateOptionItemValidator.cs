namespace MealManagment.Application.Contracts.OptionItems.Validators;

public class CreateOptionItemValidator : AbstractValidator<OptionItemRequest>
{
	public CreateOptionItemValidator()
	{
		RuleFor(x => x.Name)
			.NotEmpty()
			.MinimumLength(2)
			.MaximumLength(30);

		RuleFor(x => x.Price)
			.GreaterThanOrEqualTo(0);
	}
}