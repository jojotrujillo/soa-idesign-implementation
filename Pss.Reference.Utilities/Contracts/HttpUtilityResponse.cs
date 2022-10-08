using System.Net;

namespace Pss.Reference.Utilities.Contracts;

public class HttpUtilityResponse
{
	public HttpStatusCode StatusCode { get; set; }

	public string Body { get; set; }
}
