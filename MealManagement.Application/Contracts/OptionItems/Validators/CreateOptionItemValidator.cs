namespace MealManagement.Application.Contracts.OptionItems.Validators;

internal class CreateOptionItemValidator : AbstractValidator<OptionItemRequest>
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