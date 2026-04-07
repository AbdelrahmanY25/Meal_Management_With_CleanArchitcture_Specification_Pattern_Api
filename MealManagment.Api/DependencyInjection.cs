namespace MealManagment.Api;

public static class DependencyInjection
{
	extension(IServiceCollection services)
	{
		public IServiceCollection AddDependancies(IConfiguration configuration)
		{
			services.AddControllers();

			services.AddOpenApi();

			services.AddExceptionHandler<GlobalExcceptionHandler>();

			services.AddProblemDetails();

			services
				.AddApplication()
				.AddInfrastructure(configuration);

			return services;
		}
	}
}