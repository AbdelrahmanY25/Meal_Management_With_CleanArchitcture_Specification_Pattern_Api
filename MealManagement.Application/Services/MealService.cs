namespace MealManagement.Application.Services;

internal class MealService(IRepository<Meal> mealRepository, IMealOptionsService mealOptionsService) : IMealService
{
	public async Task<Result<MealResponse>> AddAsync(CreateMealRequest request, CancellationToken cancellationToken)
	{
		bool isMealExist = await mealRepository
			.IsExistsAsync(m => m.Name == request.Name, cancellationToken: cancellationToken);

		if (isMealExist)
			return Result.Failure<MealResponse>(MealErrors.DuplicatedMealName);

		var meal = request.Adapt<Meal>();

		Meal newMeal = await mealRepository.AddAsync(meal, cancellationToken);
		await mealRepository.SaveChangesAsync(cancellationToken);

		return Result.Success(newMeal.Adapt<MealResponse>());
	}

	public async Task<Result> UpdateAsync(string mealId, UpdateMealRequest request, CancellationToken cancellationToken)
	{
		Result validationResult = await ValidateMealOnUpdate(mealId, request, cancellationToken);

		if (validationResult.IsFailure)
			return validationResult;

		var spec = new MealByIdSpec(mealId);

		if (await mealRepository.GetOneWithSelectAsync(spec, cancellationToken) is not { } oldMeal)
			return Result.Failure(MealErrors.MealNotFound);

		if (request.HasOptionGroup || oldMeal.HasOptionGroup)
		{
			await mealOptionsService
				.UpdateAsync(oldMeal.Id, (IReadOnlyList<MealOptionGroup>)oldMeal.MealOptionGroups, request.Options?.ToList() ?? [], cancellationToken);
		}

		if (ThereAnyNewMealDataToUpdate(oldMeal, request))
			request.Adapt(oldMeal);

		mealRepository.Update(oldMeal);
		await mealRepository.SaveChangesAsync(cancellationToken);

		return Result.Success();
	}	

	public async Task<Result<MealResponse>> GetMeal(string mealId, CancellationToken cancellationToken)
	{
		var spec = new MealResponseByIdWithOptionsAndItemsSpec(mealId);

		if(await mealRepository.GetOneWithSelectAsync(spec, cancellationToken) is not { } response)
			return Result.Failure<MealResponse>(MealErrors.MealNotFound);

		return Result.Success(response!);
	}

	public async Task<IReadOnlyCollection<MealResponse>> GetAll(CancellationToken cancellationToken)
	{
		var spec = new AllMealsWithOptionsAdnOptionItemsSpec();

		var response = await mealRepository.GetAllWithSelectAsync(spec, cancellationToken);

		return response;
	}


	private async Task<Result> ValidateMealOnUpdate(string mealId, UpdateMealRequest request, CancellationToken cancellationToken)
	{
		if (request.HasOptionGroup != (request.Options is not null && request.Options.Any()))
			return Result.Failure(MealErrors.InvalidMealOptions);

		bool isMealNameDuplicated = await mealRepository
			.IsExistsAsync(m => m.Name == request.Name && m.Id != mealId, cancellationToken: cancellationToken);

		if (isMealNameDuplicated)
			return Result.Failure(MealErrors.DuplicatedMealName);

		return Result.Success();
	}

	private static bool ThereAnyNewMealDataToUpdate(Meal oldMeal, UpdateMealRequest request)
	{
		return oldMeal.Name != request.Name ||
			   oldMeal.Description != request.Description ||
			   oldMeal.Price != request.Price ||
			   oldMeal.HasOptionGroup != request.HasOptionGroup;
	}
}