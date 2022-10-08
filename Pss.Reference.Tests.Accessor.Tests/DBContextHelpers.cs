using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Moq;

namespace Pss.Reference.Accessor.Tests;

internal static class DbContextHelpers
{
	public static DbContextOptions ConfigureInMemoryDbContext<TDbContext>(string databaseName = null) where TDbContext : DbContext
	{
		if (databaseName == null)
			databaseName = Guid.NewGuid().ToString();

		var builder = new DbContextOptionsBuilder<TDbContext>();
		builder.UseInMemoryDatabase(databaseName);
		builder.ConfigureWarnings(a => a.Ignore(InMemoryEventId.TransactionIgnoredWarning));
		return builder.Options;
	}

	public static Mock<Accessors.Common.IDbContextFactory<TDbContext>> GetDbContextFactoryMock<TDbContext>(TDbContext context) where TDbContext : DbContext
	{
		var mockDbContextFactory = new Mock<Accessors.Common.IDbContextFactory<TDbContext>>();
		mockDbContextFactory
			.Setup(mock => mock.CreateDbContext(It.IsAny<IConfiguration>()))
			.Returns(context);
		return mockDbContextFactory;
	}
}

