using AutoFixture;
using Pss.Reference.Common.Extensions;
using Pss.Reference.Common.Tests.SupportingTypes;
using Pss.Reference.Shared.Tests;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Pss.Reference.Common.Tests.UnitTests;

[TestClass]
[TestCategory(Constants.Testing.UnitTest)]
public class ByteExtensionsTests
{
	[TestMethod]
	public void ToBytes_WithNullType_ReturnsNullResponse()
	{
		// ARRANGE
		SimpleType entity = null;

		// ACT
		var actual = entity.ToBytes();

		// ASSERT
		Assert.IsNull(actual);
	}

	[TestMethod]
	public void ToBytes_WithType_ReturnsByteArrayResponse()
	{
		// ARRANGE
		var fixuture = new Fixture();
		var expected = fixuture.Create<SimpleType>();

		// ACT
		var actual = expected.ToBytes();

		// ASSERT
		Assert.IsNotNull(actual);
		var actutalObj = actual.FromBytes<SimpleType>();
		TestHelper.ValidatePropertyValues(expected, actutalObj);
	}

	[TestMethod]
	public void FromBytes_WithType_ReturnsSimpleTypeResponse()
	{
		// ARRANGE
		var fixuture = new Fixture();
		var expected = fixuture.Create<SimpleType>();
		var bytes = expected.ToBytes();

		// ACT
		var actual = bytes.FromBytes<SimpleType>();

		// ASSERT
		Assert.IsNotNull(actual);
		TestHelper.ValidatePropertyValues(expected, actual);
	}

	[TestMethod]
	public void FromBytes_WithTypeParameter_ReturnsSimpleTypeResponse()
	{
		// ARRANGE
		var fixuture = new Fixture();
		var expected = fixuture.Create<SimpleType>();
		var bytes = expected.ToBytes();

		// ACT
		var actual = bytes.FromBytes<SimpleType>(typeof(SimpleType));

		// ASSERT
		Assert.IsNotNull(actual);
		TestHelper.ValidatePropertyValues(expected, actual);
	}
}
