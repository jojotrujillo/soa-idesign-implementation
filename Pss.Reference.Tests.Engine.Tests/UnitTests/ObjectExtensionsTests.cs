using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pss.Reference.Common;
using Pss.Reference.Engines.Extensions;

namespace Pss.Reference.Engine.Tests.UnitTests;

[TestClass]
[TestCategory(Constants.Testing.UnitTest)]
public class ObjectExtensionsTests
{
	[TestMethod]
	public void DataContractToDictionary_WithNull_ReturnEmptyDictionary()
	{
		// ARRANGE
		SimpleType myType = null;

		// ACT
		var result = myType.DataContractToDictionary();

		// ASSERT
		Assert.IsNotNull(result);
		Assert.IsTrue(result.Keys.Any() == false);
		Assert.IsTrue(result.Values.Any() == false);
	}

	[TestMethod]
	public void DataContractToDictionary_WithValues_ReturnsExpectedDictionary()
	{
		// ARRANGE
		SimpleType myType = new SimpleType { Name = Guid.NewGuid().ToString(), Value = int.MaxValue };

		// ACT
		var result = myType.DataContractToDictionary();

		// ASSERT
		Assert.IsNotNull(result);
		Assert.AreEqual(myType.Name, result["Name"]);
		Assert.AreEqual(myType.Value, result["Value"]);
	}
}
