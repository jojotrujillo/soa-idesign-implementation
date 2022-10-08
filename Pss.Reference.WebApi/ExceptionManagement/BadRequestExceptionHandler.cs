using System.Net;
using Pss.Reference.Common;
using Pss.Reference.Common.Extensions;

namespace Pss.Reference.WebApi.ExceptionManagement;

internal class BadRequestExceptionHandler : IExceptionHandler
{
	public (HttpStatusCode, string) HandleException(Exception exception)
	{
		return (HttpStatusCode.BadRequest, exception.Data[Constants.ValidationResults].ToJson());
	}
}
