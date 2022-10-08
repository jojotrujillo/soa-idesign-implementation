using System.Net;

namespace Pss.Reference.WebApi.ExceptionManagement;

internal interface IExceptionHandler
{
	(HttpStatusCode, string) HandleException(Exception exception);
}
