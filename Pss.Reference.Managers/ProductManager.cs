using Pss.Reference.Accessors.Contracts;
using Pss.Reference.Contracts.Logic.Exceptions;
using Pss.Reference.Contracts.Logic.Notifications;
using Pss.Reference.Contracts.Logic.Validations;
using Pss.Reference.Engines.Contracts;
using Pss.Reference.Managers.Contracts;
using Pss.Reference.Managers.Extensions;
using Pss.Reference.Utilities.Contracts;
using Client = Pss.Reference.Contracts.Client.Products;

namespace Pss.Reference.Managers;

internal class ProductManager : ManagerBase, IProductManager
{
	private IValidationEngine ValidationEngine => EngineFactory.Create<IValidationEngine>();
	private IProductAccessor ProductAccessor => AccessorFactory.Create<IProductAccessor>();
	private IQueueUtility QueueUtility => UtilityFactory.Create<IQueueUtility>();

	public async Task<Client.ProductBase[]> Find(Client.FindRequest request)
	{
		// Orchestration Sequence
		// 1. Transform Client-layer request contract to Logic-layer contract.
		// 2. Validate the request.
		// 3. Leverage the Resource Accessor to satisfy the request.
		// 4. Transform Logic-layer product contract to Client-layer contract.

		var logicRequest = request.ToLogic();
		ValidateRequest(logicRequest);

		var response = await ProductAccessor.Find(logicRequest);
		return response.ToClient();
	}

	public async Task Remove(int productId)
	{
		// Orchestration Sequence
		// 1. Leverage the Resource Accessor to satisfy the request.
		// 2. Queue notification about the change in products.

		await ProductAccessor.Remove(productId);

		var notification = new RemoveProductNotification { Message = $"Product Removed", ProductId = productId };
		QueueUtility.Send(notification);
	}

	public async Task<Client.ProductBase> Store(Client.ProductBase product)
	{
		// Orchestration Sequence
		// 1. Transform Client-layer contract to Logic-layer contract.
		// 2. Validate the request.
		// 3. Leverage the Resource Accessor to satisfy the request.
		// 4. Queue notification about the change in products.
		// 5. Transform Logic-layer product contract to Client-layer contract.

		var logicProduct = product.ToLogic();
		ValidateRequest(logicProduct);

		var response = await ProductAccessor.Store(logicProduct);

		var notification = new StoreProductNotification
		{
			Message = $"{product.GetType().Name} Stored",
			ProductId = product.ProductId.GetValueOrDefault()
		};
		QueueUtility.Send(notification);

		return response.ToClient();
	}

	#region TestMe
	public override string TestMe(string input)
	{
		var validationEngine = EngineFactory.Create<IValidationEngine>();
		var result = validationEngine.TestMe(base.TestMe(input));
		return result;
	}
	#endregion

	private void ValidateRequest(object request)
	{
		ValidationResult[] validationResults = ValidationEngine.Validate(request);

		if (validationResults.Any(r => !r.IsValid))
			throw new ValidationException(validationResults);
	}
}
