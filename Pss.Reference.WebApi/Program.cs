using Microsoft.AspNetCore.ResponseCompression;
using Newtonsoft.Json.Converters;
using Pss.Reference.Common;
using Pss.Reference.Common.Contracts;
using Pss.Reference.Managers;
using Pss.Reference.WebApi.ExceptionManagement;
using Pss.Reference.WebApi.HostedServices;
using Pss.Reference.WebApi.Hubs;

AsyncLocal<AmbientContext> ambientContext = new AsyncLocal<AmbientContext>();
var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddDebug();
builder.Logging.AddConsole();

// Add services to the container.
builder.Services.AddCors(options =>
{
	options.AddDefaultPolicy(
		build =>
		{
			build.WithOrigins("http://localhost:3000");
		});
});

builder.Services
	.AddControllers()
	// Enables polymorphic serialization of the derived type properties in the JSON response.
	.AddNewtonsoftJson(options =>
	{
		options.SerializerSettings.Converters.Add(new StringEnumConverter());
	}); 

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen().AddSwaggerGenNewtonsoftSupport();
builder.Services.AddSignalR();
builder.Services.AddResponseCompression(options =>
{
	options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new[] { "application/octet-stream" });
});

ManagerFactory.RegisterTypes(builder.Services, builder.Configuration);
builder.Services.AddTransient(serviceProvider => new ManagerFactory(ambientContext.Value, serviceProvider));
builder.Services.AddHostedService(provider => new QueueMessageProcessorServiceHost(provider));

var app = builder.Build();

app.Use(async (context, next) =>
{
	ambientContext.Value = new AmbientContext { CorrelationId = Guid.NewGuid().ToString() };
	await next.Invoke();
});

using var loggerFactory = LoggerFactory.Create(loggingBuilder => loggingBuilder.AddConsole().AddDebug());
app.AddExceptionHandler(loggerFactory);

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
app.UseSwagger();
app.UseSwaggerUI();
//}

app.UseCors();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapHub<NotificationHub>(Constants.SignalR.HubPath);
app.Run();
