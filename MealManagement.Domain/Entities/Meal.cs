namespace MealManagement.Domain.Entities;

public sealed class Meal
{
	public string Id { get; init; } = Guid.CreateVersion7().ToString();
	public string Name { get; init; } = string.Empty;
	public string Description { get; init; } = string.Empty;
	public decimal Price { get; init; }
	public bool HasOptions { get; init; } = false;

	public ICollection<MealOption> Options { get; init; } = [];
}