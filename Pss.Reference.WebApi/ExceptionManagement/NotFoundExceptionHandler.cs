using System.Net;
using Pss.Reference.Common.Extensions;
using Pss.Reference.Contracts.Logic.Exceptions;

namespace Pss.Reference.WebApi.ExceptionManagement;

internal class NotFoundExceptionHandler : IExceptionHandler
{
	public (HttpStatusCode, string) HandleException(Exception exception)
	{
		var errorResponse = new ErrorResponse
		{
			Message = exception.Message,
			Body = null
		};

		return (HttpStatusCode.NotFound, errorResponse.ToJson());
	}
}
