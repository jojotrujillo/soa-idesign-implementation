namespace Pss.Reference.Contracts.Logic.Products;

public class FindRequest
{
	public int? ProductId { get; set; }
	public ProductType? ProductType { get; set; }
	public string Manufacturer { get; set; }
	public string StockKeepingUnit { get; set; }
	public string Name { get; set; }
	public string Description { get; set; }
	public decimal? SellPrice { get; set; }
	public int? CurrentQuantity { get; set; }
	public int? ReorderQuantity { get; set; }
	public bool? IsDeleted { get; set; }
}
