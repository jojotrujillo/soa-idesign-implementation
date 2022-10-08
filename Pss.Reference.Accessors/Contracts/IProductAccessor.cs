using Pss.Reference.Common.Contracts;
using Logic = Pss.Reference.Contracts.Logic;

namespace Pss.Reference.Accessors.Contracts;

public interface IProductAccessor : IServiceComponent
{
	Task<Logic.Products.ProductBase[]> Find(Logic.Products.FindRequest request);

	Task Remove(int productId);

	Task<Logic.Products.ProductBase> Store(Logic.Products.ProductBase product);
}
