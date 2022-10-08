using System.Diagnostics;
using System.Net;
using Microsoft.AspNetCore.Diagnostics;
using Pss.Reference.Common;
using Pss.Reference.Common.Extensions;
using Pss.Reference.Contracts.Logic.Exceptions;

namespace Pss.Reference.WebApi.ExceptionManagement;

internal static class ExceptionHandler
{
	private static readonly Dictionary<Type, IExceptionHandler> _strategy = new()
	{
		{ typeof(ValidationException), new BadRequestExceptionHandler() },
		{ typeof(NotFoundException), new NotFoundExceptionHandler() },
	};

	public static void AddExceptionHandler(this IApplicationBuilder app, ILoggerFactory loggerFactory)
	{
		app.UseExceptionHandler(appError =>
		{
			appError.Run(async context => await OnHandleError(context, loggerFactory).ConfigureAwait(false));
		});
	}

	private static async Task OnHandleError(HttpContext context, ILoggerFactory loggerFactory)
	{
		var logger = loggerFactory.CreateLogger("Pss.Reference.WebApi");
		var exceptionFeature = context.Features.Get<IExceptionHandlerFeature>();
		if (exceptionFeature != null && exceptionFeature.Error != null)
		{
			if (_strategy.ContainsKey(exceptionFeature.Error.GetType()))
			{
				logger.LogError(exceptionFeature.Error, "Unhandled exception.");
				(HttpStatusCode statusCode, string json) = _strategy[exceptionFeature.Error.GetType()].HandleException(exceptionFeature.Error);
				context.Response.StatusCode = (int)statusCode;
				context.Response.ContentType = Constants.Json;
				await context.Response.WriteAsync(json).ConfigureAwait(false);
				return;
			}
		}

		// If no handler matches this exception, return the default, Internal Server Error and fixed message text.
		context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
		context.Response.ContentType = Constants.Json;
		var errorResponse = new ErrorResponse
		{
			Body = string.Empty,
			Message = Constants.Messages.UnhandledExceptionMessage
		};

		if (Debugger.IsAttached && exceptionFeature?.Error != null)
			errorResponse = new ErrorResponse
			{
				Body = exceptionFeature.Error.StackTrace,
				Message = exceptionFeature.Error.Message
			};

		await context.Response.WriteAsync(errorResponse.ToJson()).ConfigureAwait(false);
	}
}
