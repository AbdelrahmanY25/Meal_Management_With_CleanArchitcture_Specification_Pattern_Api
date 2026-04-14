namespace MealManagement.Domain.Entities;

public sealed class MealOptionsItem
{
	public string Id { get; private set; } = Guid.CreateVersion7().ToString();
	public string OptionId { get; private set; } = string.Empty;

	public string Name { get; private set; } = string.Empty;
	public decimal Price { get; private set; }
	public bool IsPopular { get; private set; } = false;

	public MealOption Option { get; private set; } = default!;

	public void AssignToOption(string optionId) => 
		OptionId = optionId;

	public void Update(decimal price, bool isPopular)
	{
		Price = price < 0 ? 0 : price;
		IsPopular = isPopular;
	}
}