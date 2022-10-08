using Pss.Reference.Accessors;
using Pss.Reference.Common;
using Pss.Reference.Common.Contracts;
using Pss.Reference.Shared.Tests;
using Pss.Reference.Shared.Tests.Extensions;
using Pss.Reference.Utilities;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Pss.Reference.Accessor.Tests.UnitTests;

[TestClass]
[TestCategory(Constants.Testing.UnitTest)]
[TestCategory("Accessor")]
public class AccessorFactoryTests
{
	#region Mocks
	public interface IMockUtility : IServiceComponent
	{
	}

	public interface IMockAccessorUsesUtility : IServiceComponent
	{

	}

	internal class MockUtility : UtilityBase, IMockUtility
	{

	}

	internal class MockAccessorUsesUtility : AccessorBase, IMockAccessorUsesUtility
	{
		public override string TestMe(string input)
		{
			IServiceComponent utility = UtilityFactory.Create<IMockUtility>();
			return utility.TestMe(base.TestMe(input));
		}
	}

	internal class MockAccessor : AccessorBase, IServiceComponent
	{
	}
	#endregion

	[TestMethod]
	public void MockAccessor_TestMe_ReturnsExpectedText()
	{
		// ARRANGE
		var serviceCollection = AssemblyInitialize.CopyServiceCollection();
		serviceCollection.RegisterOverride<IServiceComponent>(new MockAccessor());
		var serviceProvider = serviceCollection.BuildServiceProvider();

		var accessorFactory = new AccessorFactory(null, serviceProvider);
		var service = accessorFactory.Create<IServiceComponent>();

		// ACT
		var result = service.TestMe("Test");

		// ASSERT
		Assert.AreEqual($"Test : MockAccessor[]", result);
		Assert.IsTrue(accessorFactory.IsTypeRegistered<IServiceComponent>());
	}

	[TestMethod]
	public void MockAccessorWithUtility_TestMe_ReturnsExpectedText()
	{
		// ARRANGE
		var serviceCollection = AssemblyInitialize.CopyServiceCollection();
		serviceCollection.RegisterOverride<IServiceComponent>(new MockAccessorUsesUtility());
		serviceCollection.RegisterOverride<IMockUtility>(new MockUtility());

		var serviceProvider = serviceCollection.BuildServiceProvider();
		var context = new AmbientContext { CorrelationId = Guid.NewGuid().ToString() };

		var accessorFactory = new AccessorFactory(context, serviceProvider);
		var utilityFactory = new UtilityFactory(context, serviceProvider);
		var service = accessorFactory.Create<IServiceComponent>(utilityFactory);

		// ACT
		var result = service.TestMe("Test");

		// ASSERT
		Assert.AreEqual($"Test : MockAccessorUsesUtility[{context.CorrelationId}] : MockUtility[{context.CorrelationId}]", result);
	}

	[TestMethod]
	public void MockAccessorWithAmbientContext_TestMe_ReturnsExpectedText()
	{
		// ARRANGE
		var serviceCollection = AssemblyInitialize.CopyServiceCollection();
		serviceCollection.RegisterOverride<IServiceComponent>(new MockAccessor());
		var serviceProvider = serviceCollection.BuildServiceProvider();
		var context = new AmbientContext { CorrelationId = Guid.NewGuid().ToString() };

		var accessorFactory = new AccessorFactory(context, serviceProvider);
		var service = accessorFactory.Create<IServiceComponent>();

		// ACT
		var result = service.TestMe("Test");

		// ASSERT
		Assert.AreEqual($"Test : MockAccessor[{context.CorrelationId}]", result);
	}

	[TestMethod]
	public void MockAccessorWithUtilityWithAmbientContext_TestMe_ReturnsExpectedText()
	{
		// ARRANGE
		var serviceCollection = AssemblyInitialize.CopyServiceCollection();
		serviceCollection.RegisterOverride<IMockAccessorUsesUtility>(new MockAccessorUsesUtility());
		serviceCollection.RegisterOverride<IMockUtility>(new MockUtility());
		var serviceProvider = serviceCollection.BuildServiceProvider();
		var context = new AmbientContext { CorrelationId = Guid.NewGuid().ToString() };

		var accessorFactory = new AccessorFactory(context, serviceProvider);
		var utilityFactory = new UtilityFactory(context, serviceProvider);
		var service = accessorFactory.Create<IMockAccessorUsesUtility>(utilityFactory);

		// ACT
		var result = service.TestMe("Test");

		// ASSERT
		Assert.AreEqual($"Test : MockAccessorUsesUtility[{context.CorrelationId}] : MockUtility[{context.CorrelationId}]", result);
	}
}
