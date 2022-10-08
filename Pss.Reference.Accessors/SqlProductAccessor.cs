using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Pss.Reference.Accessors.Common;
using Pss.Reference.Accessors.Contracts;
using Pss.Reference.Accessors.Products;
using Pss.Reference.Accessors.Products.Extensions;
using Pss.Reference.Contracts.Logic.Exceptions;
using Pss.Reference.Contracts.Logic.Products;
using Models = Pss.Reference.Accessors.Products.Models;

namespace Pss.Reference.Accessors;

internal class SqlProductAccessor : AccessorBase, IProductAccessor
{
	private readonly Common.IDbContextFactory<ProductDbContext> _dbContextFactory = null;

	private IConfiguration Configuration => Factory.ResolveRequiredService<IConfiguration>();

	public SqlProductAccessor() : this(null)
	{
	}

	internal SqlProductAccessor(Common.IDbContextFactory<ProductDbContext> dbContextFactory)
	{
		_dbContextFactory = dbContextFactory ?? new DbContextFactory<ProductDbContext>();
	}

	public async Task<ProductBase[]> Find(FindRequest request)
	{
		if (request == null)
			throw new ArgumentNullException(nameof(request));

		using var context = _dbContextFactory.CreateDbContext(Configuration);
		var query = CreateQuery(request, context);

		var results = await query.Select(c => c.ToLogic()).ToArrayAsync();

		if (!results.Any() && request.ProductId.HasValue)
			throw new NotFoundException($"Product with Id={request.ProductId} was not found in the database.");

		return results;
	}

	public async Task Remove(int productId)
	{
		using var context = _dbContextFactory.CreateDbContext(Configuration);
		var product = await context.Products.FindAsync(productId);

		if (product == null)
			throw new NotFoundException($"Product with Id={productId} was not found in the database.");

		context.Products.Remove(product);
		await context.SaveChangesAsync();
	}

	public async Task<ProductBase> Store(ProductBase product)
	{
		using var context = _dbContextFactory.CreateDbContext(Configuration);
		Models.Product model = null;

		if (product.ProductId.HasValue)
		{
			// Update the tracked model.
			model = await context.Products.FindAsync(product.ProductId.Value);
			if (model != null)
				model = product.ToModel(model);
		}

		if (model == null)
		{
			// Add new model.
			model = product.ToModel();
			model.ProductId = 0;
			context.Products.Add(model);
		}

		await context.SaveChangesAsync();
		return model.ToLogic();
	}

	private static IQueryable<Models.Product> CreateQuery(FindRequest request, ProductDbContext context)
	{
		IQueryable<Models.Product> query = context.Products;

		if (request.ProductId.HasValue)
			query = query.Where(c => c.ProductId == request.ProductId);

		if (request.ProductType.HasValue)
			query = query.Where(c => c.ProductType == request.ProductType);

		if (!string.IsNullOrWhiteSpace(request.Manufacturer))
			query = query.Where(c => c.Manufacturer == request.Manufacturer);

		if (!string.IsNullOrWhiteSpace(request.StockKeepingUnit))
			query = query.Where(c => c.StockKeepingUnit == request.StockKeepingUnit);

		if (!string.IsNullOrWhiteSpace(request.Name))
			query = query.Where(c => c.Name == request.Name);

		if (!string.IsNullOrWhiteSpace(request.Description))
			query = query.Where(c => c.Description == request.Description);

		if (request.SellPrice.HasValue)
			query = query.Where(c => c.SellPrice == request.SellPrice.Value);

		if (request.CurrentQuantity.HasValue)
			query = query.Where(c => c.CurrentQuantity == request.CurrentQuantity.Value);

		if (request.ReorderQuantity.HasValue)
			query = query.Where(c => c.ReorderQuantity == request.ReorderQuantity.Value);

		if (request.IsDeleted.HasValue)
			query = query.Where(c => c.IsDeleted == request.IsDeleted.Value);

		return query;
	}
}
