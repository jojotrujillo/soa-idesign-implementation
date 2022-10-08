using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pss.Reference.Common.Extensions;
using Pss.Reference.Common.Tests.SupportingTypes;

namespace Pss.Reference.Common.Tests.UnitTests;

[TestClass]
[TestCategory(Constants.Testing.UnitTest)]
public class JsonExtensionsTests
{
	private static readonly string Name = Guid.NewGuid().ToString();
	private static readonly int Value = new Random(DateTime.UtcNow.Millisecond).Next(int.MinValue, int.MaxValue);

	[TestMethod]
	public void ToJson_WithNullType_ReturnsNullResponse()
	{
		// ARRANGE
		SimpleType entity = null;

		// ACT
		var result = entity.ToJson();

		// ASSERT
		Assert.IsNull(result);
	}

	[TestMethod]
	public void ToJson_WithEmptyTypeAndDefaults_ReturnsNullResponse()
	{
		// ARRANGE
		var entity = new SimpleType();

		// ACT
		var result = entity.ToJson();

		// ASSERT
		Assert.IsNull(result);
	}

	[TestMethod]
	public void ToJson_WithEmptyTypeIgnoreNullsIsFalse_ReturnsJsonResponseWithNulls()
	{
		// ARRANGE
		var entity = new SimpleType();

		// ACT
		var result = entity.ToJson(areNullsOmitted: false, isCamelCasingPreferred: true, useIndentedFormatting: false, outputEnumsUsingName: false);

		// ASSERT
		Assert.AreEqual("{\"name\":null,\"value\":null}", result);
	}

	[TestMethod]
	public void ToJson_WithInitializedTypeAndDefaults_ReturnsExpectedJsonResponse()
	{
		// ARRANGE
		var entity = new SimpleType { Name = Name, Value = Value };

		// ACT
		var result = entity.ToJson();

		// ASSERT
		Assert.AreEqual("{" + $"\"name\":\"{Name}\",\"value\":{Value}" + "}", result);
	}

	[TestMethod]
	public void ToJson_WithInitializedTypeAndCamelCasingFalse_ReturnsExpectedJsonResponse()
	{
		// ARRANGE
		var entity = new SimpleType { Name = Name, Value = Value };

		// ACT
		var result = entity.ToJson(isCamelCasingPreferred: false, areNullsOmitted: true, useIndentedFormatting: false, outputEnumsUsingName: false);

		// ASSERT
		Assert.AreEqual("{" + $"\"Name\":\"{Name}\",\"Value\":{Value}" + "}", result);
	}

	[TestMethod]
	public void ToJson_WithInitializedTypeWithNullAndCamelCasingFalseAndIgnoreNullsFalse_ReturnsExpectedJsonResponse()
	{
		// ARRANGE
		var entity = new SimpleType { Name = Name, Value = null };

		// ACT
		var result = entity.ToJson(false, false, false, false);

		// ASSERT
		Assert.AreEqual("{" + $"\"Name\":\"{Name}\",\"Value\":null" + "}", result);
	}

	[TestMethod]
	public void FromJson_WithInitializedJsonString_ReturnsExpectedTypeValues()
	{
		// ARRANGE
		const string json = "{\"name\":\"65ef8642-5357-4655-a358-cf11d5dc471a\",\"value\":921174499}";

		// ACT
		var result = json.FromJson<SimpleType>();

		// ASSERT
		Assert.IsNotNull(result);
		Assert.AreEqual(typeof(SimpleType), result.GetType());
		Assert.AreEqual("65ef8642-5357-4655-a358-cf11d5dc471a", result.Name);
		Assert.AreEqual(921174499, result.Value.Value);
	}

	[TestMethod]
	public void ToJson_WithTimeFormat_ReturnsExpectedJsonResponse()
	{
		// ARRANGE
		const string json = "{\"Date\":\"20201003\",\"value\":921174499}";

		// ACT
		var result = json.FromJson<SimpleTypeDate>("yyyyMMdd");

		// ASSERT
		Assert.AreEqual(new DateTime(2020, 10, 3), result.Date);
	}

	[TestMethod]
	public void ToJson_WithInitializedTypeWithNullAndCamelCasingFalseAndIgnoreNullsFalseAndUseIndentedFormattingTrueAndOutputEnumsUsingNameFalse_ReturnsExpectedJsonResponse()
	{
		// ARRANGE
		var entity = new SimpleType { Name = Name, Value = null };

		// ACT
		var result = entity.ToJson(false, false, true, false);

		// ASSERT
		Assert.AreEqual("{" + Environment.NewLine + GetJsonFormatterSpaces(2) + $"\"Name\": \"{Name}\"," + Environment.NewLine + GetJsonFormatterSpaces(2) + "\"Value\": null" + Environment.NewLine + "}", result);
	}

	[TestMethod]
	public void ToJson_WithInitializedTypeWithNullAndCamelCasingFalseAndIgnoreNullsFalseAndUseIndentedFormattingFalseAndOutputEnumsUsingNameFalse_ReturnsExpectedJsonResponse()
	{
		// ARRANGE
		var entity = new SimpleTypeWithEnum { Name = Name, Value = null, EnumValue = TestEnum.Value0 };

		// ACT
		var result = entity.ToJson(false, false, false, false);

		// ASSERT
		Assert.AreEqual("{" + $"\"Name\":\"{Name}\",\"Value\":null,\"EnumValue\":0" + "}", result);
	}

	[TestMethod]
	public void ToJson_WithInitializedTypeWithNullAndCamelCasingFalseAndIgnoreNullsFalseAndUseIndentedFormattingFalseAndOutputEnumsUsingNameTrue_ReturnsExpectedJsonResponse()
	{
		// ARRANGE
		var entity = new SimpleTypeWithEnum { Name = Name, Value = null, EnumValue = TestEnum.Value0 };

		// ACT
		var result = entity.ToJson(false, false, false, true);

		// ASSERT
		Assert.AreEqual("{" + $"\"Name\":\"{Name}\",\"Value\":null,\"EnumValue\":\"Value0\"" + "}", result);
	}

	[TestMethod]
	public void FromJson_WithInitializedJsonStringAndType_ReturnsExpectedTypeValues()
	{
		// ARRANGE
		const string json = "{\"name\":\"65ef8642-5357-4655-a358-cf11d5dc471a\",\"value\":921174499}";
		string typeName = typeof(SimpleType).AssemblyQualifiedName;

		// ACT
		var result = (SimpleType)json.FromJson(typeName);

		// ASSERT
		Assert.IsNotNull(result);
		Assert.AreEqual(typeof(SimpleType), result.GetType());
		Assert.AreEqual("65ef8642-5357-4655-a358-cf11d5dc471a", result.Name);
		Assert.AreEqual(921174499, result.Value.Value);
	}

	[TestMethod]
	public void ToObjectFromJsonFile_WithValidFile_ReturnsValidObject()
	{
		// ARRANGE
		var filename = @".\Data\SimpleData.json";
		var expected = new SimpleType
		{
			Name = "value",
			Value = 1
		};

		// ACT
		var result = filename.ToObjectFromJsonFile<SimpleType>();

		// ASSERT
		Assert.AreEqual(expected.Name, result.Name);
		Assert.AreEqual(expected.Value, result.Value);
	}

	[TestMethod]
	public void ToJson_IgnoreDefaults_MatchingString()
	{
		// ARRANGE
		var obj = new SimpleTypeWithEnum
		{
			Name = "value"
		};

		// ACT
		var result = obj.ToJson(true, true, false, false, true);

		// ASSERT
		Assert.AreEqual($"{{\"name\":\"{obj.Name}\"}}", result);
	}

	private static string GetJsonFormatterSpaces(int level)
	{
		return new string(' ', level);
	}
}
