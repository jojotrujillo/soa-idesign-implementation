using System.Net;
using Pss.Reference.Common.Extensions;
using Pss.Reference.Contracts.Logic.Exceptions;
using Pss.Reference.Contracts.Logic.Validations;
using Radzen;

namespace Pss.Reference.Web.Extensions;

internal static class HttpResponseMessageExtensions
{
	public static async Task DisplayErrorNotification(this HttpResponseMessage response, NotificationService notificationService)
	{
		if (response == null || notificationService == null)
			return;

		var content = await response.Content.ReadAsStringAsync();

		switch (response.StatusCode)
		{
			case HttpStatusCode.NotFound:
				notificationService.Notify(NotificationSeverity.Error, "Not Found", "The requested product(s) could not be found.", 5000);
				break;

			case HttpStatusCode.BadRequest:
				var validationResults = content.FromJson<ValidationResult[]>();
				foreach (var result in validationResults)
					notificationService.Notify(NotificationSeverity.Error, result.Field, result.ErrorMessage, 5000);
				break;

			case HttpStatusCode.InternalServerError:
				var error = content.FromJson<ErrorResponse>();
				notificationService.Notify(NotificationSeverity.Error, error.Message, error.Body, 5000);
				break;

			default:
				notificationService.Notify(NotificationSeverity.Error, $"Status Code: {response.StatusCode}", "Unknown error occurred.", 5000);
				break;
		}
	}
}
