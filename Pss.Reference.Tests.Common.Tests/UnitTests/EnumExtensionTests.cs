using Pss.Reference.Common.Extensions;
using Pss.Reference.Common.Tests.SupportingTypes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Pss.Reference.Common.Tests.UnitTests;

[TestClass]
[TestCategory(Constants.Testing.UnitTest)]
public class EnumExtensionTests
{
	#region To Enum Type From Nullable Char
	[DataTestMethod]
	[DataRow(null, EnumWithDefaultValuesType.UnknownDefaultValue)]
	[DataRow('0', EnumWithDefaultValuesType.UnknownDefaultValue)]
	[DataRow('1', EnumWithDefaultValuesType.ThisDefaultValue)]
	[DataRow('2', EnumWithDefaultValuesType.ThatDefaultValue)]
	public void ToEnumType_FromNullableChar_ForDefaultValueEnumType_WithDataValue_ReturnsExpectedValue(char? value, EnumWithDefaultValuesType expected)
	{
		// ARRANGE

		// ACT 
		var actual = value.ToEnumType(EnumWithDefaultValuesType.UnknownDefaultValue);

		// ASSERT
		Assert.AreEqual(expected, actual);
	}

	[TestMethod]
	[ExpectedException(typeof(InvalidOperationException))]
	public void ToEnumType_FromNullableChar_ForDefaultValueEnumType_WithInvalidValue_ThrowsException()
	{
		// ARRANGE
		char? value = '~';

		// ACT 
		value.ToEnumType(EnumWithDefaultValuesType.UnknownDefaultValue);

		// ASSERT
	}

	[DataTestMethod]
	[DataRow(null, EnumWithCharValuesType.UnknownCharValue)]
	[DataRow(' ', EnumWithCharValuesType.UnknownCharValue)]
	[DataRow('A', EnumWithCharValuesType.ThisCharValue)]
	[DataRow('Z', EnumWithCharValuesType.ThatCharValue)]
	public void ToEnumType_FromNullableChar_ForCharValueEnumType_WithDataValue_ReturnsExpectedValue(char? value, EnumWithCharValuesType expected)
	{
		// ARRANGE

		// ACT 
		var actual = value.ToEnumType(EnumWithCharValuesType.UnknownCharValue);

		// ASSERT
		Assert.AreEqual(expected, actual);
	}

	[TestMethod]
	[ExpectedException(typeof(InvalidOperationException))]
	public void ToEnumType_FromNullableChar_ForCharValueEnumType_WithInvalidValue_ThrowsException()
	{
		// ARRANGE
		char? value = '~';

		// ACT 
		value.ToEnumType(EnumWithCharValuesType.UnknownCharValue);

		// ASSERT
	}
	#endregion

	#region To Nullable Enum Type From Nullable Char
	[DataTestMethod]
	[DataRow(null, null, null)]
	[DataRow(null, EnumWithDefaultValuesType.UnknownDefaultValue, EnumWithDefaultValuesType.UnknownDefaultValue)]
	[DataRow('0', EnumWithDefaultValuesType.UnknownDefaultValue, null)]
	[DataRow('1', EnumWithDefaultValuesType.ThisDefaultValue, null)]
	[DataRow('2', EnumWithDefaultValuesType.ThatDefaultValue, null)]
	public void ToNullableEnumType_FromNullableChar_ForDefaultValueEnumType_WithDataValue_ReturnsExpectedValue(char? value, EnumWithDefaultValuesType? expected, EnumWithDefaultValuesType? defaultValue)
	{
		// ARRANGE

		// ACT 
		var actual = value.ToNullableEnumType(defaultValue);

		// ASSERT
		Assert.AreEqual(expected, actual);
	}

	[TestMethod]
	[ExpectedException(typeof(InvalidOperationException))]
	public void ToNullableEnumType_FromNullableChar_ForDefaultValueEnumType_WithInvalidValue_ThrowsException()
	{
		// ARRANGE
		char? value = '~';

		// ACT 
		value.ToNullableEnumType<EnumWithDefaultValuesType>(null);

		// ASSERT
	}

	[DataTestMethod]
	[DataRow(null, null, null)]
	[DataRow(null, EnumWithCharValuesType.UnknownCharValue, EnumWithCharValuesType.UnknownCharValue)]
	[DataRow(' ', EnumWithCharValuesType.UnknownCharValue, null)]
	[DataRow('A', EnumWithCharValuesType.ThisCharValue, null)]
	[DataRow('Z', EnumWithCharValuesType.ThatCharValue, null)]
	public void ToNullableEnumType_FromNullableChar_ForCharValueEnumType_WithDataValue_ReturnsExpectedValue(char? value, EnumWithCharValuesType? expected, EnumWithCharValuesType? defaultValue)
	{
		// ARRANGE

		// ACT 
		var actual = value.ToNullableEnumType(defaultValue);

		// ASSERT
		Assert.AreEqual(expected, actual);
	}

	[TestMethod]
	[ExpectedException(typeof(InvalidOperationException))]
	public void ToNullableEnumType_FromNullableChar_ForCharValueEnumType_WithInvalidValue_ThrowsException()
	{
		// ARRANGE
		char? value = '~';

		// ACT 
		value.ToNullableEnumType<EnumWithCharValuesType>(null);

		// ASSERT
	}
	#endregion

	#region To Enum Type From String
	[DataTestMethod]
	[DataRow(null, EnumWithDefaultValuesType.UnknownDefaultValue)]
	[DataRow("", EnumWithDefaultValuesType.UnknownDefaultValue)]
	[DataRow("UnknownDefaultValue", EnumWithDefaultValuesType.UnknownDefaultValue)]
	[DataRow("ThisDefaultValue", EnumWithDefaultValuesType.ThisDefaultValue)]
	[DataRow("ThatDefaultValue", EnumWithDefaultValuesType.ThatDefaultValue)]
	public void ToEnumType_FromString_ForDefaultValueEnumType_WithDataValue_ReturnsExpectedValue(string value, EnumWithDefaultValuesType expected)
	{
		// ARRANGE

		// ACT 
		var actual = value.ToEnumType(EnumWithDefaultValuesType.UnknownDefaultValue);

		// ASSERT
		Assert.AreEqual(expected, actual);
	}

	[TestMethod]
	[ExpectedException(typeof(InvalidOperationException))]
	public void ToEnumType_FromString_ForDefaultValueEnumType_WithInvalidValue_ThrowsException()
	{
		// ARRANGE
		string value = "AnotherThing";

		// ACT 
		value.ToEnumType(EnumWithDefaultValuesType.UnknownDefaultValue);

		// ASSERT
	}

	[DataTestMethod]
	[DataRow(null, EnumWithCharValuesType.UnknownCharValue)]
	[DataRow("", EnumWithCharValuesType.UnknownCharValue)]
	[DataRow("UnknownCharValue", EnumWithCharValuesType.UnknownCharValue)]
	[DataRow("ThisCharValue", EnumWithCharValuesType.ThisCharValue)]
	[DataRow("ThatCharValue", EnumWithCharValuesType.ThatCharValue)]
	public void ToEnumType_FromString_ForCharValueEnumType_WithDataValue_ReturnsExpectedValue(string value, EnumWithCharValuesType expected)
	{
		// ARRANGE

		// ACT 
		var actual = value.ToEnumType(EnumWithCharValuesType.UnknownCharValue);

		// ASSERT
		Assert.AreEqual(expected, actual);
	}

	[TestMethod]
	[ExpectedException(typeof(InvalidOperationException))]
	public void ToEnumType_FromString_ForCharValueEnumType_WithInvalidValue_ThrowsException()
	{
		// ARRANGE
		string value = "AnotherThing";

		// ACT 
		value.ToEnumType(EnumWithCharValuesType.UnknownCharValue);

		// ASSERT
	}
	#endregion

	#region To Nullable Enum Type From String
	[DataTestMethod]
	[DataRow(null, null, null)]
	[DataRow(null, EnumWithDefaultValuesType.UnknownDefaultValue, EnumWithDefaultValuesType.UnknownDefaultValue)]
	[DataRow("", EnumWithDefaultValuesType.UnknownDefaultValue, EnumWithDefaultValuesType.UnknownDefaultValue)]
	[DataRow("UnknownDefaultValue", EnumWithDefaultValuesType.UnknownDefaultValue, null)]
	[DataRow("ThisDefaultValue", EnumWithDefaultValuesType.ThisDefaultValue, null)]
	[DataRow("ThatDefaultValue", EnumWithDefaultValuesType.ThatDefaultValue, null)]
	public void ToNullableEnumType_FromString_ForDefaultValueEnumType_WithDataValue_ReturnsExpectedValue(string value, EnumWithDefaultValuesType? expected, EnumWithDefaultValuesType? defaultValue)
	{
		// ARRANGE

		// ACT 
		var actual = value.ToNullableEnumType(defaultValue);

		// ASSERT
		Assert.AreEqual(expected, actual);
	}

	[TestMethod]
	[ExpectedException(typeof(InvalidOperationException))]
	public void ToNullableEnumType_FromString_ForDefaultValueEnumType_WithInvalidValue_ThrowsException()
	{
		// ARRANGE
		string value = "AnotherThing";

		// ACT 
		value.ToNullableEnumType<EnumWithDefaultValuesType>(EnumWithDefaultValuesType.UnknownDefaultValue);

		// ASSERT
	}

	[DataTestMethod]
	[DataRow(null, null, null)]
	[DataRow(null, EnumWithCharValuesType.UnknownCharValue, EnumWithCharValuesType.UnknownCharValue)]
	[DataRow("", EnumWithCharValuesType.UnknownCharValue, EnumWithCharValuesType.UnknownCharValue)]
	[DataRow("UnknownCharValue", EnumWithCharValuesType.UnknownCharValue, null)]
	[DataRow("ThisCharValue", EnumWithCharValuesType.ThisCharValue, null)]
	[DataRow("ThatCharValue", EnumWithCharValuesType.ThatCharValue, null)]
	public void ToNullableEnumType_FromString_ForCharValueEnumType_WithDataValue_ReturnsExpectedValue(string value, EnumWithCharValuesType? expected, EnumWithCharValuesType? defaultValue)
	{
		// ARRANGE

		// ACT 
		var actual = value.ToNullableEnumType(defaultValue);

		// ASSERT
		Assert.AreEqual(expected, actual);
	}

	[TestMethod]
	[ExpectedException(typeof(InvalidOperationException))]
	public void ToNullableEnumType_FromString_ForCharValueEnumType_WithInvalidValue_ThrowsException()
	{
		// ARRANGE
		string value = "AnotherThing";

		// ACT 
		value.ToNullableEnumType<EnumWithCharValuesType>(EnumWithCharValuesType.UnknownCharValue);

		// ASSERT
	}
	#endregion
}
