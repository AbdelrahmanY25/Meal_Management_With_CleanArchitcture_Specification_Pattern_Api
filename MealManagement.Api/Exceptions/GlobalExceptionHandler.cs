namespace MealManagement.Api.Exceptions;

internal class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger, IProblemDetailsService problemDetailsService) : IExceptionHandler
{
	public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
	{
		logger.LogError(exception, "Something went wrong: {Message}", exception.Message);

		return await problemDetailsService.TryWriteAsync(new ProblemDetailsContext
		{

			HttpContext = httpContext,
			Exception = exception,
			ProblemDetails = {
				Status = StatusCodes.Status500InternalServerError,
				Title = "Internal Server Eerror.",
				Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.6.1"
			}

		});
	}
}