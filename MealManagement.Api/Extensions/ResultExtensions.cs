namespace MealManagement.Api.Extensions;

internal static class ResultExtensions
{
	extension(Result result)
	{
		internal ObjectResult ToProblem()
		{
			if (result.IsSuccess)
				throw new InvalidOperationException("Can't convert a successful result to a problem.");

			var problem = Results.Problem(statusCode: result.Error.StatusCode);
			var problemDetails = problem.GetType().GetProperty(nameof(ProblemDetails))!.GetValue(problem) as ProblemDetails;

			problemDetails!.Extensions = new Dictionary<string, object?>
			{
				{
					"errors", new[] { result.Error }
				}
			};

			return new ObjectResult(problemDetails);
		}
	}
}