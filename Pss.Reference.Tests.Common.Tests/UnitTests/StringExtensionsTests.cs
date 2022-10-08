using Pss.Reference.Common.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Globalization;

namespace Pss.Reference.Common.Tests.UnitTests;

[TestClass]
[TestCategory(Constants.Testing.UnitTest)]
public class StringExtensionsTests
{
	[TestMethod]
	public void ToInt_WithNullType_ReturnsNullResponse()
	{
		// ARRANGE
		string val = null;

		// ACT
		var result = val.ToInt();

		// ASSERT
		Assert.IsNull(result);
	}

	[TestMethod]
	public void ToInt_WithNumber_ReturnsIntNumberResponse()
	{
		// ARRANGE
		var val = "12";

		// ACT
		var result = val.ToInt();

		// ASSERT
		Assert.AreEqual(int.Parse(val), result);
	}

	[TestMethod]
	public void ToInt_WithText_ReturnsNullResponse()
	{
		// ARRANGE
		var val = "abc";

		// ACT
		var result = val.ToInt();

		// ASSERT
		Assert.IsNull(result);
	}

	[TestMethod]
	public void ToDateTime_WithDate_ReturnsDateTimeResponse()
	{
		// ARRANGE
		var val = "7/27/2020 3:31:00 PM";

		// ACT
		var result = val.ToDateTime();

		// ASSERT
		Assert.AreEqual(DateTime.ParseExact(val, "M/dd/yyyy h:mm:ss tt", CultureInfo.InvariantCulture, DateTimeStyles.None), result);
	}

	[TestMethod]
	public void ToDateTime_WithNull_ReturnsNullResponse()
	{
		// ARRANGE
		string val = null;

		// ACT
		var result = val.ToDateTime();

		// ASSERT
		Assert.IsNull(result);
	}

	[TestMethod]
	public void ToDateTime_WithText_ReturnsNullResponse()
	{
		// ARRANGE
		var val = "abc";

		// ACT
		var result = val.ToDateTime();

		// ASSERT
		Assert.IsNull(result);
	}

	[TestMethod]
	public void ToBytes_FromBytes_WithText_ReturnsMatchingString()
	{
		// ARRANGE
		var val = "abc";

		// ACT
		var bytes = val.ToBytes();
		var actual = bytes.FromBytes();

		// ASSERT
		Assert.AreEqual(val, actual);
	}

	[DataRow("true", true)]
	[DataRow("false", false)]
	[DataRow("True", true)]
	[DataRow("False", false)]
	[DataRow("abcd", null)]
	[DataRow("12345", null)]
	[DataRow("", null)]
	[DataRow(" ", null)]
	[DataRow(null, null)]
	[TestMethod]
	public void ToBool_WithVaryingData_ReturnsExectedValue(string input, bool? expected)
	{
		// ARRANGE

		// ACT
		var actual = input.ToBool();

		// ASSERT
		Assert.AreEqual(expected, actual);
	}

	[TestMethod]
	public void Encode_WithGuidString_MatchesDecodedValue()
	{
		// ARRANGE
		string decoded = Guid.NewGuid().ToString();

		// ACT
		string encoded = decoded.Encode();

		// ASSERT
		Assert.IsNotNull(encoded);
		Assert.AreNotEqual(decoded, encoded);
		Assert.AreEqual(decoded, encoded.Decode());
	}
}
