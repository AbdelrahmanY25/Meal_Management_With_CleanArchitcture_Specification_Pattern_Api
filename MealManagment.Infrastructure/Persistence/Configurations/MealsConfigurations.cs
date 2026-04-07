namespace MealManagment.Infrastructure.Persistence.Configurations;

internal class MealsConfigurations : IEntityTypeConfiguration<Meal>
{
	public void Configure(EntityTypeBuilder<Meal> builder)
	{	
		builder.ToTable("Meals", "Menu");

		builder.HasMany(m => m.MealOptionGroups)
			.WithOne(mog => mog.Meal)
			.HasForeignKey(mog => mog.MealId);

		builder.Property(m => m.Name)
			.HasMaxLength(30);

		builder.Property(m => m.Description)
			.HasMaxLength(100);

		builder.Property(m => m.Price)
			.HasPrecision(5, 2);
	}
}