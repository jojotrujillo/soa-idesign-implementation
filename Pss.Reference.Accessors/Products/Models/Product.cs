using Pss.Reference.Contracts.Logic.Products;

namespace Pss.Reference.Accessors.Products.Models;

internal class Product
{
	public int ProductId { get; set; }
	public ProductType ProductType { get; set; }
	public string Name { get; set; }
	public string Description { get; set; }
	public string Manufacturer { get; set; }
	public string StockKeepingUnit { get; set; }
	public decimal SellPrice { get; set; }
	public int CurrentQuantity { get; set; }
	public int ReorderQuantity { get; set; }
	public bool IsDeleted { get; set; }
	public string ProductJson { get; set; }
}
