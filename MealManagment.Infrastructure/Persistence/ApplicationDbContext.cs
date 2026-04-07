namespace MealManagment.Infrastructure.Persistence;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
	public DbSet<Meal> Meals { get; set; }
	public DbSet<MealOptionGroup> MealOptionGroups { get; set; }
	public DbSet<OptionGroupItems> OptionGroupItems { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);

		var fks = modelBuilder.Model
			.GetEntityTypes()
			.SelectMany(e => e.GetForeignKeys())
			.Where(e => !e.IsOwnership);

		foreach (var fk in fks)
			fk.DeleteBehavior = DeleteBehavior.Restrict;

		modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
	}
}