namespace MealManagement.Infrastructure.Persistence.Configurations;

internal class OptionGroupItemsConfigurations : IEntityTypeConfiguration<MealOptionsItem>
{
	public void Configure(EntityTypeBuilder<MealOptionsItem> builder)
	{
		builder.ToTable("MealOptionsItems", "Menu");

		builder.HasOne(e => e.Option)
			.WithMany(e => e.Items)
			.HasForeignKey(e => e.OptionGroupId);

		builder.Property(e => e.Name)
			.HasMaxLength(30);	

		builder.Property(e => e.Price)
			.HasPrecision(5,2);

		builder.HasIndex(e => new { e.OptionGroupId, e.Name })
			.IsUnique();
	}
}