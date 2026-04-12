namespace MealManagement.Application.Services;

internal class MealOptionsService(IRepository<MealOption> mealOptionsRepository, IMealOptionItemsService itemsService) : IMealOptionsService
{
	public async Task<Result> UpdateAsync(string mealId,
		IReadOnlyList<MealOptionRequest> mealOptionsReq,
		CancellationToken cancellationToken)
	{
		var validationResult = ValidateMealOptionsRequestOnUpdate(mealOptionsReq);

		if (validationResult.IsFailure)
			return validationResult;

		var spec = new GetOptionsByMealIdSpec(mealId);
		
		var mealOptionsDb = await mealOptionsRepository.GetAllAsync(spec, cancellationToken);

		var dbNames = mealOptionsDb.ToDictionary(r => r.Name);

		var reqNames = mealOptionsReq.ToDictionary(r => r.Name);

		var deletedOptions = mealOptionsDb.Where(db => !reqNames.ContainsKey(db.Name)).ToList();

		if (deletedOptions.Count > 0)
			DeleteMany(deletedOptions);

		var updatedOptionsDb = mealOptionsDb.Where(db => reqNames.ContainsKey(db.Name)).ToList();
		
		foreach (var optionDb in updatedOptionsDb)
		{
			var matchingReq = reqNames[optionDb.Name];

			await itemsService
				.UpdateAsync(optionDb.Id, (IReadOnlyList<MealOptionsItem>)optionDb.Items, matchingReq.Items, cancellationToken);
		}
		
		var newOptions = mealOptionsReq.Where(req => !dbNames.ContainsKey(req.Name)).ToList();

		if (newOptions.Count > 0)
			AddManyAsync(mealId, newOptions, cancellationToken);

		return Result.Success();
	}

	private void AddManyAsync(string mealId, IReadOnlyList<MealOptionRequest> mealOptionsReq, CancellationToken cancellationToken)
	{
		IEnumerable<MealOption> newOptions = [.. mealOptionsReq.Select(x => new MealOption
		{
			MealId = mealId,
			Name = x.Name,
			Items = [.. x.Items.Select(i => new MealOptionsItem
			{
				Name = i.Name,
				IsPopular = i.IsPopular,
				Price = i.Price
			})]
		})];

		mealOptionsRepository.AddRange(newOptions);
	}

	private void DeleteMany(IEnumerable<MealOption> mealOptionsDb)
	{
		itemsService.DeleteMany(mealOptionsDb.SelectMany(x => x.Items));
		mealOptionsRepository.DeleteRange(mealOptionsDb);
	}

	private static Result ValidateMealOptionsRequestOnUpdate(IReadOnlyList<MealOptionRequest> mealOptionsReq) 
	{
		if (mealOptionsReq.Count > 20)
			return Result.Failure(MealOptionsErrors.InvalidMealOptionsCount);

		if (mealOptionsReq.Count != mealOptionsReq.DistinctBy(opt => opt.Name).Count())
			return Result.Failure(MealOptionsErrors.DuplicatedOptionName);

		foreach (var option in mealOptionsReq)
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
}