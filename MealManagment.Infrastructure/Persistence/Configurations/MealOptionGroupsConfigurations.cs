namespace MealManagment.Infrastructure.Persistence.Configurations;

internal class MealOptionGroupsConfigurations : IEntityTypeConfiguration<MealOptionGroup>
{
	public void Configure(EntityTypeBuilder<MealOptionGroup> builder)
	{
		builder.ToTable("MealOptionGroups", "Menu");
		
		builder.HasMany(o => o.Items)
			.WithOne(i => i.OptionGroup)
			.HasForeignKey(i => i.OptionGroupId);

		builder.Property(e => e.Name)
			.HasMaxLength(30);

		 builder.HasIndex(e => new { e.MealId, e.Name })
			.IsUnique();
	}
}