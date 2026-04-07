namespace MealManagment.Application;

public static class DependencyInjection
{
	extension(IServiceCollection services)
	{
		public IServiceCollection AddApplication()
		{
			services.AddFluentValidationConfig();

			services.AddServices();

			services.AddMapsterConfig();

			return services;
		}

		private IServiceCollection AddFluentValidationConfig()
		{
			services
				.AddFluentValidationAutoValidation()
				.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);

			return services;
		}

		private IServiceCollection AddServices()
		{
			services.AddScoped<IMealService, MealService>();
			services.AddScoped<IMealOptionsService, MealOptionsService>();
			services.AddScoped<IMealOptionItemsService, MealOptionItemsService>();

			return services;
		}

		private IServiceCollection AddMapsterConfig()
		{
			var mappingConfig = TypeAdapterConfig.GlobalSettings;
			mappingConfig.Scan(typeof(MappingConfig).Assembly);

			services.AddSingleton<IMapper>(new Mapper(mappingConfig));

			return services;
		}
	}
}