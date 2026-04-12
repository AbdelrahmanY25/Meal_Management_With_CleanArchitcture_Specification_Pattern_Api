namespace MealManagement.Application.Tests.ServicesTests;

public class MealServiceTest
{
	[Fact]
	public async Task AddAsync_WhenMealNameIsExistsInDatabase_ShouldReturnResultFailureWithDuplicatedMealNameError()
	{
		// Arrange
		var mealRepository = A.Fake<IRepository<Meal>>();
		var mealOptionsService = A.Fake<IMealOptionsService>();

		var sut = new MealService(mealRepository, mealOptionsService);

		var request = new CreateMealRequest("Existing Meal", "Description", 10.0m, false, []);
		
		A.CallTo(() => mealRepository
			.IsExistsAsync(A<Expression<Func<Meal, bool>>>.Ignored, A<bool>.Ignored, A<CancellationToken>.Ignored))
			.Returns(true);

		// Act
		var result = await sut.AddAsync(request, CancellationToken.None);

		// Assert
		A.CallTo(() => mealRepository.Add(A<Meal>.Ignored))
			.MustNotHaveHappened();
		
		A.CallTo(() => mealRepository.SaveChangesAsync(A<CancellationToken>.Ignored))
			.MustNotHaveHappened();
	
		Assert.False(result.IsSuccess);
		Assert.Equal(MealErrors.DuplicatedMealName, result.Error);
	}

	[Fact]
	public async Task AddAsync_WhenMealNameIsUnique_ShouldReturnSuccessAndPersistMeal() 
	{
		// Arrange
		var mealRepository = A.Fake<IRepository<Meal>>();
		var mealOptionsService = A.Fake<IMealOptionsService>();

		var sut = new MealService(mealRepository, mealOptionsService);

		var request = new CreateMealRequest("Unique Meal", "Description", 10.0m, false, []);

		A.CallTo(() => mealRepository
			.IsExistsAsync(A<Expression<Func<Meal, bool>>>.Ignored, A<bool>.Ignored, A<CancellationToken>.Ignored))
			.Returns(false);

		A.CallTo(() => mealRepository.Add(A<Meal>.Ignored))
			.ReturnsLazily((Meal m) => m);

		A.CallTo(() => mealRepository.SaveChangesAsync(A<CancellationToken>.Ignored));

		// Act
		var result = await sut.AddAsync(request, CancellationToken.None);

		// Assert
		Assert.True(result.IsSuccess);

		Assert.Equal(request.Name, result.Value.Name);
		Assert.Equal(request.Description, result.Value.Description);
		Assert.Equal(request.Price, result.Value.Price);
		Assert.Equal(request.HasOptions, result.Value.HasOptionGroup);
		
		A.CallTo(() => mealRepository.Add(A<Meal>.Ignored))
			.MustHaveHappenedOnceExactly()
			.Then(A.CallTo(() => mealRepository.SaveChangesAsync(A<CancellationToken>.Ignored))
				.MustHaveHappenedOnceExactly());
	}
}