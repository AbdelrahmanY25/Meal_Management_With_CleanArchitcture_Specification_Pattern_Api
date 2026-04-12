namespace MealManagement.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MealsController(IMealService mealService) : ControllerBase
{
	[HttpPost("")]
	public async Task<IActionResult> Add([FromBody] CreateMealRequest request, CancellationToken cancellationToken = default)
	{
		var result = await mealService.AddAsync(request, cancellationToken);

		return result.IsSuccess ?
			CreatedAtAction(nameof(GetMeal), new { mealId = result.Value.Id }, result.Value) : result.ToProblem();
	}

	[HttpPut("{mealId}")]
	public async Task<IActionResult> Update([FromRoute] string mealId, [FromBody] UpdateMealRequest request, CancellationToken cancellationToken = default)
	{
		var result = await mealService.UpdateAsync(mealId, request, cancellationToken);

		return result.IsSuccess ? Ok() : result.ToProblem();
	}

	[HttpGet("{mealId}")]
	public async Task<IActionResult> GetMeal([FromRoute] string mealId, CancellationToken cancellationToken = default)
	{
		var result = await mealService.GetMealAsync(mealId, cancellationToken);

		return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
	}

	[HttpGet]
	public async Task<IActionResult> GetAll(CancellationToken cancellationToken = default)
	{
		var result = await mealService.GetAllAsync(cancellationToken);

		return Ok(result);
	}
}