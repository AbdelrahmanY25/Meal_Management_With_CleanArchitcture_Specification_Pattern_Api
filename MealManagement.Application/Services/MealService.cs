namespace MealManagement.Application.Services;

internal class MealService(IRepository<Meal> mealRepository, IMealOptionsService mealOptionsService) : IMealService
{
	private readonly IRepository<Meal> _mealRepository = mealRepository;
	private readonly IMealOptionsService _mealOptionsService = mealOptionsService;

	public async Task<Result<MealResponse>> AddAsync(MealRequest request, CancellationToken cancellationToken)
	{
		var validationResult = await ValidateMealRequestOnAdd(request, cancellationToken);

		if (validationResult.IsFailure)
			return Result.Failure<MealResponse>(validationResult.Error);

		var meal = request.Adapt<Meal>();

		Meal newMeal = _mealRepository.Add(meal);
		await _mealRepository.SaveChangesAsync(cancellationToken);

		return Result.Success(newMeal.Adapt<MealResponse>());
	}

	public async Task<Result> UpdateAsync(string mealId, MealRequest request, CancellationToken cancellationToken)
	{
		Result validationResult = await ValidateMealOnUpdate(mealId, request, cancellationToken);

		if (validationResult.IsFailure)
			return validationResult;

		var spec = new MealByIdSpec(mealId);

		if (await _mealRepository.GetOneAsync(spec, cancellationToken) is not { } oldMeal)
			return Result.Failure(MealErrors.MealNotFound);

		if (request.HasOptions || oldMeal.HasOptions)
		{
			var mealOptionsResult = await _mealOptionsService.UpdateAsync(oldMeal.Id, request.Options!, cancellationToken);
			
			if (mealOptionsResult.IsFailure)
				return mealOptionsResult;
		}

		oldMeal.Update(request.Name, request.Description, request.Price, request.HasOptions);

		await _mealRepository.SaveChangesAsync(cancellationToken);

		return Result.Success();
	}	

	public async Task<Result<MealResponse>> GetMealAsync(string mealId, CancellationToken cancellationToken)
	{
		var spec = new MealResponseByIdWithOptionsAndItemsSpec(mealId);

		if(await _mealRepository.GetOneWithSelectAsync(spec, cancellationToken) is not { } response)
			return Result.Failure<MealResponse>(MealErrors.MealNotFound);

		return Result.Success(response!);
	}

	public async Task<IReadOnlyCollection<MealResponse>> GetAllAsync(CancellationToken cancellationToken)
	{
		var spec = new AllMealsWithOptionsAndOptionItemsSpec();

		var response = await _mealRepository.GetAllWithSelectAsync(spec, cancellationToken);

		return response;
	}

	

	private async Task<Result> ValidateMealRequestOnAdd(MealRequest request, CancellationToken cancellationToken)
	{
		bool isMealExists = await _mealRepository
				.IsExistsAsync(m => m.Name == request.Name, cancellationToken: cancellationToken);

		if (isMealExists)
			return Result.Failure(MealErrors.DuplicatedMealName);

		return Result.Success();
	}	

	private async Task<Result> ValidateMealOnUpdate(string mealId, MealRequest request, CancellationToken cancellationToken)
	{
		bool isMealNameDuplicated = await _mealRepository
			.IsExistsAsync(m => m.Name == request.Name && m.Id != mealId, cancellationToken: cancellationToken);

		if (isMealNameDuplicated)
			return Result.Failure(MealErrors.DuplicatedMealName);

		return Result.Success();
	}
}