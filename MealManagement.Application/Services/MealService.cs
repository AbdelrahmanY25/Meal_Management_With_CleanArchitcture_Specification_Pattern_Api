namespace MealManagement.Application.Services;

internal class MealService(IRepository<Meal> mealRepository, IMealOptionsService mealOptionsService) : IMealService
{
	private readonly IRepository<Meal> _mealRepository = mealRepository;
	private readonly IMealOptionsService _mealOptionsService = mealOptionsService;

	public async Task<Result<MealResponse>> AddAsync(CreateMealRequest request, CancellationToken cancellationToken)
	{
		bool isMealExist = await _mealRepository
			.IsExistsAsync(m => m.Name == request.Name, cancellationToken: cancellationToken);

		if (isMealExist)
			return Result.Failure<MealResponse>(MealErrors.DuplicatedMealName);

		var meal = request.Adapt<Meal>();

		Meal newMeal = await _mealRepository.AddAsync(meal, cancellationToken);
		await _mealRepository.SaveChangesAsync(cancellationToken);

		return Result.Success(newMeal.Adapt<MealResponse>());
	}

	public async Task<Result> UpdateAsync(string mealId, UpdateMealRequest request, CancellationToken cancellationToken)
	{
		Result validationResult = await ValidateMealOnUpdate(mealId, request, cancellationToken);

		if (validationResult.IsFailure)
			return validationResult;

		var spec = new MealByIdSpec(mealId);
		var oldMeal = await _mealRepository.GetOneAsync(spec, cancellationToken);

		if ((!request.HasOptionGroup && oldMeal!.HasOptionGroup) ||
			(request.HasOptionGroup && !oldMeal!.HasOptionGroup) ||
			(request.HasOptionGroup && oldMeal!.HasOptionGroup))
		{
			await _mealOptionsService
				.UpdateAsync(oldMeal.HasOptionGroup, oldMeal.Id, oldMeal.MealOptionGroups, request.Options!, cancellationToken);
		}

		request.Adapt(oldMeal!);

		await _mealRepository.SaveChangesAsync(cancellationToken);

		return Result.Success();
	}	

	public async Task<Result<MealResponse>> GetMeal(string mealId, CancellationToken cancellationToken)
	{
		bool isMealExist = await _mealRepository
			.IsExistsAsync(m => m.Id == mealId, cancellationToken: cancellationToken);

		if (!isMealExist)
			return Result.Failure<MealResponse>(MealErrors.MealNotFound);

		var spec = new MealResponseByIdWithOptionsAndItemsSpec(mealId);

		var response = await _mealRepository.GetOneWithSelectAsync(spec, cancellationToken);

		return Result.Success(response!);
	}

	public async Task<IReadOnlyCollection<MealResponse>> GetAll(CancellationToken cancellationToken)
	{
		var spec = new AllMealsWithOptionsAdnOptionItemsSpec();

		var response = await _mealRepository.GetAllWithSelectAsync(spec, cancellationToken);

		return response;
	}


	private async Task<Result> ValidateMealOnUpdate(string mealId, UpdateMealRequest request, CancellationToken cancellationToken)
	{
		if (request.HasOptionGroup != (request.Options is not null && request.Options.Any()))
			return Result.Failure(MealErrors.InvalidMealOptions);

		bool isMealExist = await _mealRepository
			.IsExistsAsync(m => m.Id == mealId, cancellationToken: cancellationToken);

		if (!isMealExist)
			return Result.Failure(MealErrors.MealNotFound);

		bool isMealNameDuplicated = await _mealRepository
			.IsExistsAsync(m => m.Name == request.Name && m.Id != mealId, cancellationToken: cancellationToken);

		if (isMealNameDuplicated)
			return Result.Failure(MealErrors.DuplicatedMealName);

		return Result.Success();
	}
}