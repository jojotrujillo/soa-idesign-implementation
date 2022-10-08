using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pss.Reference.Common;
using Pss.Reference.Engines.Validators;
using Logic = Pss.Reference.Contracts.Logic.Products;

namespace Pss.Reference.Engine.Tests.UnitTests;

[TestClass]
[TestCategory(Constants.Testing.UnitTest)]
public class ValidatorTests
{
	[TestMethod]
	[DataRow("ProductId", null, false, false)]
	[DataRow("ProductId", null, true, true)]
	[DataRow("ProductId", -1, false, false)]
	[DataRow("ProductId", 0, false, false)]
	[DataRow("ProductId", 1, true, false)]
	[DataRow("ProductId", int.MaxValue, true, false)]
	public void Validate_IntegerIdValidator_ReturnsExpectedResult(string key, int? value, bool isValid, bool isOptional)
	{
		// ARRANGE
		IValidator validator = new IntegerIdValidator(isOptional);

		// ACT & ASSERT
		ValidateAndAssert(validator, key, value, isValid);
	}

	[TestMethod]
	[DataRow("ProductType", null, false)]
	[DataRow("ProductType", Logic.ProductType.Unknown, false)]
	[DataRow("ProductType", Logic.ProductType.Commodity, true)]
	[DataRow("ProductType", Logic.ProductType.SalonProduct, true)]
	[DataRow("ProductType", Logic.ProductType.Vehicle, true)]
	[DataRow("ProductType", (Logic.ProductType)10, false)]
	public void Validate_ProductTypeValidator_ReturnsExpectedResult(string key, Logic.ProductType? value, bool isValid)
	{
		// ARRANGE
		IValidator validator = new ProductTypeValidator();

		// ACT & ASSERT
		ValidateAndAssert(validator, key, value, isValid);
	}

	[TestMethod]
	[DataRow("VehicleIdentificationNumber", null, false, 50, false)]
	[DataRow("VehicleIdentificationNumber", null, true, 50, true)]
	[DataRow("VehicleIdentificationNumber", "", false, 50, false)]
	[DataRow("VehicleIdentificationNumber", "", true, 50, true)]
	[DataRow("VehicleIdentificationNumber", " ", false, 50, false)]
	[DataRow("VehicleIdentificationNumber", " ", true, 50, true)]
	[DataRow("VehicleIdentificationNumber", "XYZ123", true, 50, false)]
	[DataRow("VehicleIdentificationNumber", "XYZ123", true, 50, true)]
	[DataRow("VehicleIdentificationNumber", "12345678901234567890123456789012345678901234567890", true, 50, false)]
	[DataRow("VehicleIdentificationNumber", "123456789012345678901234567890123456789012345678901", false, 50, false)]
	public void Validate_StringValidator_ReturnsExpectedResult(string key, string value, bool isValid, int maxLength, bool isOptional)
	{
		// ARRANGE
		IValidator validator = new StringValidator(maxLength, isOptional);

		// ACT & ASSERT
		ValidateAndAssert(validator, key, value, isValid);
	}

	[TestMethod]
	[DataRow("ManufactureYear", null, false, (short)0)]
	[DataRow("ManufactureYear", (short)0, true, (short)0)]
	[DataRow("ManufactureYear", (short)1, true, (short)1)]
	[DataRow("ManufactureYear", (short)1999, true, (short)9999)]
	[DataRow("ManufactureYear", (short)1999, false, (short)2)]
	public void Validate_MaxNumberValidator_ReturnsExpectedResult(string key, short? value, bool isValid, short maxValue)
	{
		// ARRANGE
		IValidator validator = new MaxNumberValidator<short>(maxValue);

		// ACT & ASSERT
		ValidateAndAssert(validator, key, value, isValid);
	}

	private static void ValidateAndAssert<TValue>(IValidator validator, string key, TValue value, bool isValid)
	{
		// ACT
		var result = validator.Validate(new KeyValuePair<string, object>(key, value));

		// ASSERT
		Assert.AreEqual(isValid, result.IsValid);
	}
}
