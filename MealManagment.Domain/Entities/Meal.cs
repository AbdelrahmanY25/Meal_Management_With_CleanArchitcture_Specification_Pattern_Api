namespace MealManagment.Domain.Entities;

public class Meal
{
	public string Id { get; set; } = Guid.CreateVersion7().ToString();
	public string Name { get; set; } = string.Empty;
	public string Description { get; set; } = string.Empty;
	public decimal Price { get; set; }
	public bool HasOptionGroup { get; set; } = false;

	public ICollection<MealOptionGroup> MealOptionGroups { get; set; } = [];
}