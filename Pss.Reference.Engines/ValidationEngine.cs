using System.Diagnostics;
using Pss.Reference.Accessors.Contracts;
using Pss.Reference.Contracts.Logic.Validations;
using Pss.Reference.Engines.Contracts;
using Pss.Reference.Engines.Extensions;
using Pss.Reference.Engines.Validators;

namespace Pss.Reference.Engines;

internal class ValidationEngine : EngineBase, IValidationEngine
{
	private static readonly Dictionary<string, IValidator> _validators = new(StringComparer.OrdinalIgnoreCase)
		{
			{ "ProductId", new IntegerIdValidator(true) },
			{ "ProductType", new ProductTypeValidator() },
			{ "VehicleIdentificationNumber", new StringValidator(50, true) },
			{ "Make", new StringValidator(50, true) },
			{ "ManufactureYear", new MaxNumberValidator<short>(9999) },
			{ "LicenseNumber", new StringValidator(10, true) },
			{ "StockKeepingUnit", new StringValidator(50, false) },
			{ "Name", new StringValidator(50, false) },
			{ "Description", new StringValidator(150, false) },
			{ "Manufacturer", new StringValidator(50, false) },
		};

	public ValidationResult[] Validate(object request)
	{
		var requestDictionary = request.DataContractToDictionary();
		List<ValidationResult> validationResults = new List<ValidationResult>();
		validationResults.AddRange(MultiFieldValidation(requestDictionary));

		foreach (var kvp in requestDictionary)
		{
			if (_validators.ContainsKey(kvp.Key))
			{
				var validation = _validators[kvp.Key].Validate(kvp);

				if (!validation.IsValid)
					validationResults.Add(validation);
			}
		}

		return validationResults.ToArray();
	}

	#region TestMe
	public override string TestMe(string input)
	{
		var productAccessor = AccessorFactory.Create<IProductAccessor>();
		var result = productAccessor.TestMe(base.TestMe(input));
		return result;
	}
	#endregion

	private static IEnumerable<ValidationResult> MultiFieldValidation(Dictionary<string, object> requestDictionary)
	{
		var validationResults = new List<ValidationResult>();

		if (!requestDictionary.ContainsKey("StockKeepingUnit") || !requestDictionary.ContainsKey("Manufacturer"))
			return validationResults;

		string sku = requestDictionary["StockKeepingUnit"] as string;
		string manufacturer = requestDictionary["Manufacturer"] as string;
		Debug.Assert(!string.IsNullOrEmpty(sku));
		Debug.Assert(!string.IsNullOrEmpty(manufacturer));

		if (!sku.StartsWith(manufacturer[0]))
		{
			validationResults.Add(new ValidationResult
			{
				Field = "StockKeepingUnit",
				ErrorMessage = "Stock Keeping Unit must start with the first letter in the Manufacturer's name",
				IsValid = false
			});
		}

		return validationResults;
	}
}
