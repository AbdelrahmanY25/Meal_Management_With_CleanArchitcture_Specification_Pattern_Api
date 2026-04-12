namespace MealManagement.Domain.Entities;

public sealed class MealOption
{
	public string Id { get; init; } = Guid.CreateVersion7().ToString();
	public string MealId { get; init; } = string.Empty;

	public string Name { get; init; } = string.Empty;

	public Meal Meal { get; init; } = default!;
	public ICollection<MealOptionsItem> Items { get; init; } = [];
}