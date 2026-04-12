namespace MealManagement.Infrastructure.Persistence.Configurations;

internal class MealsConfigurations : IEntityTypeConfiguration<Meal>
{
	public void Configure(EntityTypeBuilder<Meal> builder)
	{	
		builder.ToTable("Meals", "Menu");

		builder.Property(m => m.Name)
			.HasMaxLength(30);

		builder.Property(m => m.Description)
			.HasMaxLength(100);

		builder.Property(m => m.Price)
			.HasPrecision(5, 2);

		builder.HasIndex(m => m.Name)
			.IsUnique();
	}
}