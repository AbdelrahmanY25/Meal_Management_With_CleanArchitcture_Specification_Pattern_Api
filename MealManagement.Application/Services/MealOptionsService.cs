namespace MealManagement.Application.Services;

internal class MealOptionsService(IRepository<MealOption> mealOptionsRepository, IMealOptionItemsService itemsService) : IMealOptionsService
{
	private readonly IMealOptionItemsService _itemsService = itemsService;
	private readonly IRepository<MealOption> _mealOptionsRepository = mealOptionsRepository;

	public async Task<Result> UpdateAsync(string mealId,
		IReadOnlyList<MealOptionRequest> mealOptionsReq,
		CancellationToken cancellationToken)
	{
		var spec = new GetOptionsByMealIdSpec(mealId);
		
		var mealOptionsDb = await _mealOptionsRepository.GetAllAsync(spec, cancellationToken);

		var dbNames = mealOptionsDb.ToDictionary(r => r.Name);

		var reqNames = mealOptionsReq.ToDictionary(r => r.Name);

		var deletedOptions = mealOptionsDb.Where(db => !reqNames.ContainsKey(db.Name)).ToList();

		if (deletedOptions.Count > 0)
			DeleteMany(deletedOptions);

		var updatedOptionsDb = mealOptionsDb.Where(db => reqNames.ContainsKey(db.Name)).ToList();
		
		foreach (var optionDb in updatedOptionsDb)
		{
			var matchingReq = reqNames[optionDb.Name];

			await _itemsService
				.UpdateAsync(optionDb.Id, (IReadOnlyList<MealOptionsItem>)optionDb.Items, matchingReq.Items, cancellationToken);
		}
		
		var newOptions = mealOptionsReq.Where(req => !dbNames.ContainsKey(req.Name)).ToList();

		if (newOptions.Count > 0)
			AddMany(mealId, newOptions);

		return Result.Success();
	}

	private void AddMany(string mealId, IReadOnlyList<MealOptionRequest> mealOptionsReq)
	{
		var newOptions = mealOptionsReq.Adapt<List<MealOption>>();

		foreach (var option in newOptions)
		{
			option.AssignToMeal(mealId);

			foreach (var item in option.Items)
				item.AssignToOption(option.Id);
		}

		_mealOptionsRepository.AddRange(newOptions);
	}

	private void DeleteMany(IEnumerable<MealOption> mealOptionsDb)
	{
		_itemsService.DeleteMany(mealOptionsDb.SelectMany(x => x.Items));
		_mealOptionsRepository.DeleteRange(mealOptionsDb);
	}
}