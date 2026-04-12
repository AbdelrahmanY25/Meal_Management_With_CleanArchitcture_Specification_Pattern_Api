namespace MealManagement.Infrastructure.Persistence.Configurations;

internal class MealOptionGroupsConfigurations : IEntityTypeConfiguration<MealOption>
{
	public void Configure(EntityTypeBuilder<MealOption> builder)
	{
		builder.ToTable("MealOptions", "Menu");
		
		builder.HasOne(o => o.Meal)
			.WithMany(m => m.Options)
			.HasForeignKey(o => o.MealId);

		builder.Property(e => e.Name)
			.HasMaxLength(30);

		 builder.HasIndex(e => new { e.MealId, e.Name })
			.IsUnique();
	}
}