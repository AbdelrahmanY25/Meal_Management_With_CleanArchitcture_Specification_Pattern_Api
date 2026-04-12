namespace MealManagement.Application.Services;

internal class MealService(IRepository<Meal> mealRepository, IMealOptionsService mealOptionsService) : IMealService
{
	public async Task<Result<MealResponse>> AddAsync(CreateMealRequest request, CancellationToken cancellationToken)
	{
		var validationResult = await ValidateMealRequestOnAdd(request, cancellationToken);

		if (validationResult.IsFailure)
			return Result.Failure<MealResponse>(validationResult.Error);

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

		if (await mealRepository.GetOneAsync(spec, cancellationToken) is not { } oldMeal)
			return Result.Failure(MealErrors.MealNotFound);

		if (request.HasOptionGroup || oldMeal.HasOptionGroup)
		{
			var mealOptionsResult = await mealOptionsService.UpdateAsync(oldMeal.Id, request.Options, cancellationToken);
			
			if (mealOptionsResult.IsFailure)
				return mealOptionsResult;
		}

		request.Adapt(oldMeal);

		await mealRepository.SaveChangesAsync(cancellationToken);

		return Result.Success();
	}	

	public async Task<Result<MealResponse>> GetMealAsync(string mealId, CancellationToken cancellationToken)
	{
		var spec = new MealResponseByIdWithOptionsAndItemsSpec(mealId);

		if(await mealRepository.GetOneWithSelectAsync(spec, cancellationToken) is not { } response)
			return Result.Failure<MealResponse>(MealErrors.MealNotFound);

		return Result.Success(response!);
	}

	public async Task<IReadOnlyCollection<MealResponse>> GetAllAsync(CancellationToken cancellationToken)
	{
		var spec = new AllMealsWithOptionsAndOptionItemsSpec();

		var response = await mealRepository.GetAllWithSelectAsync(spec, cancellationToken);

		return response;
	}

	private async Task<Result> ValidateMealRequestOnAdd(CreateMealRequest request, CancellationToken cancellationToken)
	{
		if (request.HasOptionGroup != (request.Options is not null && request.Options.Any()))
			return Result.Failure(MealErrors.InvalidOptionGroupState);

		if (request.HasOptionGroup)
		{
			var optionsValidationResult = ValidateMealOptionRequstOnAdd(request.Options!);

			if (optionsValidationResult.IsFailure)
				return optionsValidationResult;
		}

		bool isMealExists = await mealRepository
				.IsExistsAsync(m => m.Name == request.Name, cancellationToken: cancellationToken);

		if (isMealExists)
			return Result.Failure(MealErrors.DuplicatedMealName);

		return Result.Success();
	}

	private static Result ValidateMealOptionRequstOnAdd(IReadOnlyList<MealOptionRequest> options) 
	{
		if (options.Count > 20)
			return Result.Failure(MealOptionsErrors.InvalidMealOptionsCount);

		if (options.Count != options.DistinctBy(opt => opt.Name).Count())
			return Result.Failure(MealOptionsErrors.DuplicatedOptionName);

		foreach (var option in options) 
		{
			var itemsValidationResult = ValidateMealOptionItemsRequestOnAdd(option.Items);

			if (itemsValidationResult.IsFailure)
				return itemsValidationResult;	
		}

		return Result.Success();
	}

	private static Result ValidateMealOptionItemsRequestOnAdd(IReadOnlyList<OptionItemRequest> items)
	{
		if (items is null || items.Count == 0 || items.Count > 10)
			return Result.Failure(MealOptionsItemsErrors.InvalidMealOptionsItemsCount);

		if ((items.Select(i => i.Name).Count() != items.DistinctBy(i => i.Name).Count()))
			return Result.Failure(MealOptionsItemsErrors.DuplicatedItemName);

		return Result.Success();
	}

	private async Task<Result> ValidateMealOnUpdate(string mealId, UpdateMealRequest request, CancellationToken cancellationToken)
	{
		if (request.HasOptionGroup != (request.Options is not null && request.Options.Any()))
			return Result.Failure(MealErrors.InvalidOptionGroupState);

		bool isMealNameDuplicated = await mealRepository
			.IsExistsAsync(m => m.Name == request.Name && m.Id != mealId, cancellationToken: cancellationToken);

		if (isMealNameDuplicated)
			return Result.Failure(MealErrors.DuplicatedMealName);

		return Result.Success();
	}
}