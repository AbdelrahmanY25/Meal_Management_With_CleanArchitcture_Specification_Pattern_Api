namespace MealManagment.Infrastructure;

public static class DependencyInjection
{
	extension(IServiceCollection services)
	{
		public IServiceCollection AddInfrastructure(IConfiguration configuration)
		{
			services.AddDbContext(configuration);

			services.AddRepository();

			return services;
		}

		private IServiceCollection AddDbContext(IConfiguration configuration)
		{
			services.AddDbContext<ApplicationDbContext>(options =>
			{
				options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
			});

			return services;
		}

		private IServiceCollection AddRepository()
		{
			services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
			return services;
		}
	}
}