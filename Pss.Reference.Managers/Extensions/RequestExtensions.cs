using Client = Pss.Reference.Contracts.Client.Products;
using Logic = Pss.Reference.Contracts.Logic.Products;

namespace Pss.Reference.Managers.Extensions;

internal static class RequestExtensions
{
	public static Logic.FindRequest ToLogic(this Client.FindRequest request)
	{
		if (request == null)
			return null;

		return new Logic.FindRequest
		{
			CurrentQuantity = request.CurrentQuantity,
			Description = request.Description,
			IsDeleted = request.IsDeleted,
			Manufacturer = request.Manufacturer,
			Name = request.Name,
			ProductId = request.ProductId,
			ProductType = (Logic.ProductType?)request.ProductType,
			ReorderQuantity = request.ReorderQuantity,
			SellPrice = request.SellPrice,
			StockKeepingUnit = request.StockKeepingUnit
		};
	}
}
