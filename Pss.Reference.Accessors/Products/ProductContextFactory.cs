using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Pss.Reference.Common;

namespace Pss.Reference.Accessors.Products;

internal class ProductContextFactory : IDesignTimeDbContextFactory<ProductDbContext>
{
	public ProductDbContext CreateDbContext(string[] args)
	{
		var optionsBuilder = new DbContextOptionsBuilder<ProductDbContext>();
		optionsBuilder.UseSqlServer(Constants.ConnectionStrings.LocalDbSqlServer);

		return new ProductDbContext(optionsBuilder.Options);
	}
}
