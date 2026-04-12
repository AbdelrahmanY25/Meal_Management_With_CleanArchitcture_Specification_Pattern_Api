namespace MealManagement.Application.Mapping;

internal class MappingConfig : IRegister
{
	public void Register(TypeAdapterConfig config)
	{
		config.NewConfig<CreateMealRequest, Meal>();

		config.NewConfig<UpdateMealRequest, Meal>()
			.Ignore(dest => dest.Options);

		config.NewConfig<Meal, MealResponse>();
	}
}