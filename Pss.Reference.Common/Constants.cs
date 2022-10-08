namespace Pss.Reference.Common;

public static class Constants
{
	public const string AppName = "Pss.Reference";
	public const string ApplicationInsightsInstrumentationKey = "ApplicationInsights:InstrumentationKey";

	public const string AppJson = "application/json";
	public const string RedisConfiguration = "RedisConfiguration";
	public const string RedisInstanceName = "RedisInstanceName";
	public const string TypeName = "AssemblyQualifiedName";
	public const string ValidationResults = "ValidationResults";
	public const string StatusCode = "StatusCode";
	public const string Body = "Body";
	public const string Json = "application/json";

	public static class AppConfig
	{
		public const string AppPrefix = "{Reference}:";
		public const string GlobalPrefix = "Global:";
		public const string Sentinel = "{Reference}:Sentinel";
	}

	public static class AzureAd
	{
		public const string ClientId = "AzureAd:ClientId";
		public const string ClientSecret = "AzureAd:ClientSecret";
		public const string TenantId = "AzureAd:TenantId";
	}

	public static class ConnectionStrings
	{
		public const string AppConfig = "ConnectionStrings:AppConfig";
		public const string ServiceBus = "ConnectionStrings:ServiceBus";
		public const string AzureSqlServer = "ConnectionStrings:SqlConnectionString";
		public const string LocalDbSqlServer = @"Server=(localdb)\mssqllocaldb;Database=Products;Trusted_Connection=True;MultipleActiveResultSets=true";
	}

	public static class SignalR
	{
		public const string SendMethod = "SendNotification";
		public const string ReceiveMethod = "ReceiveNotification";
		public const string HubPath = "/notificationhub";
	}

	public static class Environments
	{
		public const string AspNetCoreEnvironment = "ASPNETCORE_ENVIRONMENT";
		public const string Local = "Local";
		public const string Development = "Development";
		public const string QA = "QA";
		public const string Staging = "Staging";
		public const string Production = "Production";
	}

	public static class Messages
	{
		public const string UnhandledExceptionMessage = "The server was unable to complete your request.";
		public const string BadRequestSubmittedMessage = "A bad request was submitted.";
	}

	public static class Testing
	{
		public const string UnitTest = "Unit-Test";
		public const string IntegrationTest = "Integration-Test";
	}
}
