namespace MealManagement.Domain.Entities;

public sealed class MealOptionsItem
{
	public string Id { get; set; } = Guid.CreateVersion7().ToString();
	public string OptionGroupId { get; set; } = string.Empty;

	public string Name { get; set; } = string.Empty;
	public decimal Price { get; set; }
	public bool IsPopular { get; set; } = false;

	public MealOption Option { get; set; } = default!;
}