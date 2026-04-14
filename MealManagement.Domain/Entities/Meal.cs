namespace MealManagement.Domain.Entities;

public sealed class Meal
{
	public string Id { get; private set; } = Guid.CreateVersion7().ToString();
	
	public string Name { get; private set; } = string.Empty;
	public string Description { get; private set; } = string.Empty;
	public decimal Price { get; private set; }
	public bool HasOptions { get; private set; } = false;

	public ICollection<MealOption> Options { get; private set; } = [];

	public void Update(string name, string description, decimal price, bool hasOptions)
	{
		Name = name;
		Description = description;
		Price = price < 0 ? 0 : price;
		HasOptions = hasOptions;
	}
}