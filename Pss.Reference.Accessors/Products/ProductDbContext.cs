using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Pss.Reference.Accessors.Products.Configuration;
using Pss.Reference.Common;

namespace Pss.Reference.Accessors.Products;

internal class ProductDbContext : DbContext
{
	private readonly IConfiguration _configuration;

	public DbSet<Models.Product> Products { get; set; }

	public ProductDbContext(IConfiguration configuration)
	{
		_configuration = configuration;
	}

	public ProductDbContext(DbContextOptions options) : base(options)
	{
	}

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		if (optionsBuilder == null)
			throw new ArgumentNullException(nameof(optionsBuilder));

		if (!optionsBuilder.IsConfigured)
		{
			string connectionString = _configuration[Constants.ConnectionStrings.AzureSqlServer] ?? Constants.ConnectionStrings.LocalDbSqlServer;
			optionsBuilder.UseSqlServer(connectionString);
		}
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		if (modelBuilder == null)
			throw new ArgumentNullException(nameof(modelBuilder));

		modelBuilder.ApplyConfiguration(new ProductConfiguration());
	}
}
