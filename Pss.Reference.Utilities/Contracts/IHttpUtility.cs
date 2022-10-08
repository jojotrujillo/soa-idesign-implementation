using Pss.Reference.Common.Contracts;

namespace Pss.Reference.Utilities.Contracts;

public interface IHttpUtility : IServiceComponent
{
	Task<HttpUtilityResponse> Send(HttpRequestMessage requestMessage);
}
