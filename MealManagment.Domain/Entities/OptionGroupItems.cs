namespace MealManagment.Domain.Entities;

public class OptionGroupItems
{
	public string Id { get; set; } = Guid.CreateVersion7().ToString();
	public string OptionGroupId { get; set; } = string.Empty;

	public string Name { get; set; } = string.Empty;
	public decimal Price { get; set; }
	public bool IsPobular { get; set; } = false;

	public MealOptionGroup OptionGroup { get; set; } = default!;
}