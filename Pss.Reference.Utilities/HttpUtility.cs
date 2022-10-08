using Microsoft.Extensions.DependencyInjection;
using Polly;
using Pss.Reference.Utilities.Contracts;

namespace Pss.Reference.Utilities;

internal class HttpUtility : UtilityBase, IHttpUtility
{
	private const string _clientName = "GenericHttp";

	internal static void AddHttpClient(IServiceCollection serviceCollection)
	{
		var builder = serviceCollection.AddHttpClient(_clientName);

		builder.AddTransientHttpErrorPolicy(b => b.WaitAndRetryAsync(new[]
		{
				TimeSpan.FromSeconds(1),
				TimeSpan.FromSeconds(5),
				TimeSpan.FromSeconds(10)
			}));
	}

	public async Task<HttpUtilityResponse> Send(HttpRequestMessage requestMessage)
	{
		var httpClientFactory = Factory.ResolveRequiredService<IHttpClientFactory>();
		var client = httpClientFactory.CreateClient(_clientName);

		using (var response = await client.SendAsync(requestMessage).ConfigureAwait(false))
		using (var responseContent = response.Content)
		{
			var utilityResponse = new HttpUtilityResponse
			{
				StatusCode = response.StatusCode,
				Body = await responseContent.ReadAsStringAsync().ConfigureAwait(false)
			};

			return utilityResponse;
		}
	}
}
