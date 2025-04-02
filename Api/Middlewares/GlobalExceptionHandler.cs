using System.Net;
using System.Text.Json;
using Domain.Common;
using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;

namespace Api.Middlewares;

public class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
{
	public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception,  CancellationToken cancellationToken)
	{
		httpContext.Response.Clear();
		httpContext.Response.ContentType = "application/json";
		ErrorResult errorResult;
		if (exception is ValidationException validationException)
		{
			httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
			errorResult = new ErrorResult
			{
				Title = "Validation error occurred",
				Errors =
					validationException
						.Errors
						.Select(x => new Error
						{
							
							Title = x.PropertyName,
							Description = x.ErrorMessage
						})
						.ToList()
			};
			logger.LogInformation("Request returned 400. Validation error occured: {Error}", errorResult);
		}
		else
		{
			httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
			errorResult = new ErrorResult
			{
				StatusCode = HttpStatusCode.InternalServerError,
				Title = "Unknown error occurred"
			};
			logger.LogError("Request returned 500. An error occurred: {Error}", exception.Message);
		}
		
		var result = JsonSerializer.Serialize(errorResult);

		await httpContext.Response.WriteAsync(result, cancellationToken: cancellationToken);
		
		return true;
	}
}