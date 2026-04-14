namespace MealManagement.Domain.Entities;

public sealed class MealOption
{
	public string Id { get; private set; } = Guid.CreateVersion7().ToString();
	public string MealId { get; private set; } = string.Empty;

	public string Name { get; private set; } = string.Empty;

	public Meal Meal { get; private set; } = default!;
	public ICollection<MealOptionsItem> Items { get; private set; } = [];

	public void AssignToMeal(string mealId) =>
		MealId = mealId;
}