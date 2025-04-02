using System.Net;
using Domain.Common;
using Microsoft.AspNetCore.Mvc;

namespace Api.Extensions;

public static class ResultExtensions
{
	public static IActionResult ToActionResult<T>(this Result<T> result, int statusCode = 200)
	{
		if (result.IsSuccess)
		{
			if (statusCode == 204)
			{
				return new NoContentResult();
			}
			return new ObjectResult(result.Value)
			{
				StatusCode = (int)statusCode
			};
		}

		if (result.Error is null)
		{
			return new ObjectResult(result.Error)
			{
				StatusCode = (int?)result.Error?.StatusCode ?? 500
			};
		}

		return new ObjectResult(result.Error)
		{
			StatusCode = (int)result.Error.StatusCode
		};
	}
}