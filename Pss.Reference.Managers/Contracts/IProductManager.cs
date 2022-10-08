using Pss.Reference.Common.Contracts;
using Client = Pss.Reference.Contracts.Client;

namespace Pss.Reference.Managers.Contracts;

public interface IProductManager : IServiceComponent
{
	Task<Client.Products.ProductBase[]> Find(Client.Products.FindRequest request);

	Task Remove(int productId);

	Task<Client.Products.ProductBase> Store(Client.Products.ProductBase product);
}
