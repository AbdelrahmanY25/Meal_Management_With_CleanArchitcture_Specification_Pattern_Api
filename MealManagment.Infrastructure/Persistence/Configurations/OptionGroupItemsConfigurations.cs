namespace MealManagment.Infrastructure.Persistence.Configurations;

internal class OptionGroupItemsConfigurations : IEntityTypeConfiguration<OptionGroupItems>
{
	public void Configure(EntityTypeBuilder<OptionGroupItems> builder)
	{
		builder.ToTable("OptionGroupItems", "Menu");

		builder.Property(e => e.Name)
			.HasMaxLength(30);	

		builder.Property(e => e.Price)
			.HasPrecision(5,2);

		builder.HasIndex(e => new { e.OptionGroupId, e.Name })
			.IsUnique();
	}
}