namespace MealManagement.Application.Mapping;

internal class MappingConfig : IRegister
{
	public void Register(TypeAdapterConfig config)
	{
		config.NewConfig<CreateMealRequest, Meal>()
			.Map(dest => dest.MealOptionGroups, src => src.Options);

		config.NewConfig<UpdateMealRequest, Meal>()
			.Ignore(dest => dest.MealOptionGroups);

		config.NewConfig<Meal, MealResponse>()
			.Map(dest => dest.Options, src => src.MealOptionGroups);
	}
}