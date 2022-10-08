using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pss.Reference.Accessors.Products.Models;
using Pss.Reference.Contracts.Logic.Products;

namespace Pss.Reference.Accessors.Products.Configuration;

internal class ProductConfiguration : IEntityTypeConfiguration<Product>
{
	public void Configure(EntityTypeBuilder<Product> builder)
	{
		builder.ToTable("Product");

		builder.HasKey(e => e.ProductId);

		builder.Property(e => e.ProductType)
			.HasConversion(
				e => e.ToString(),
				e => (ProductType)Enum.Parse(typeof(ProductType), e))
			.HasMaxLength(50);

		builder.Property(e => e.Name)
			.IsRequired()
			.HasMaxLength(50);

		builder.Property(e => e.Description)
			.IsRequired()
			.HasMaxLength(150);

		builder.Property(e => e.Manufacturer)
			.IsRequired()
			.HasMaxLength(50);

		builder.Property(e => e.StockKeepingUnit)
			.IsRequired()
			.HasMaxLength(50);

		builder.Property(e => e.SellPrice)
			.HasPrecision(18, 2)
			.HasDefaultValue(0.0M)
			.IsRequired();

		builder.Property(e => e.CurrentQuantity)
			.HasDefaultValue(0)
			.IsRequired();

		builder.Property(e => e.ReorderQuantity)
			.HasDefaultValue(0)
			.IsRequired();

		builder.Property(e => e.IsDeleted)
			.HasDefaultValue(false)
			.IsRequired();

		builder.Property(e => e.ProductJson)
			.IsRequired()
			.HasMaxLength(8192);

		builder.HasData(DataSeeder.GetProducts());
	}
}
