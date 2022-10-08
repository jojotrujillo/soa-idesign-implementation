using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Pss.Reference.Accessors.Common;

internal interface IDbContextFactory<TContext> where TContext : DbContext
{
	TContext CreateDbContext(IConfiguration configuration, ILoggerFactory loggerFactory);
	TContext CreateDbContext(IConfiguration configuration);
}
