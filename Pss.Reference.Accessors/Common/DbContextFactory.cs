using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Pss.Reference.Accessors.Common;

internal class DbContextFactory<TContext> : IDbContextFactory<TContext> where TContext : DbContext
{
	public TContext CreateDbContext(IConfiguration configuration, ILoggerFactory loggerFactory)
	{
		return (TContext)Activator.CreateInstance(typeof(TContext), configuration, loggerFactory);
	}

	public TContext CreateDbContext(IConfiguration configuration)
	{
		return (TContext)Activator.CreateInstance(typeof(TContext), configuration);
	}
}