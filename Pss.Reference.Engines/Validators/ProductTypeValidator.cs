using Pss.Reference.Contracts.Logic.Products;
using Pss.Reference.Contracts.Logic.Validations;

namespace Pss.Reference.Engines.Validators;

internal class ProductTypeValidator : ValidatorBase, IValidator
{
	public ValidationResult Validate(KeyValuePair<string, object> input)
	{
		// Allow null or a valid product type (except Unknown).
		ProductType? inputValue = (ProductType?)input.Value;

		var isDefined = Enum.IsDefined(typeof(ProductType), inputValue.GetValueOrDefault(ProductType.Unknown));

		if (inputValue.HasValue && isDefined && inputValue.Value != ProductType.Unknown)
			return Ok();

		var validTypes = Enum.GetNames(typeof(ProductType)).Where(e => !e.Equals(ProductType.Unknown.ToString()));
		return BadRequest(input.Key, $"Invalid '{input.Key}' value.  Must be one of the known Product Types: ({string.Join(", ", validTypes)}).");
	}
}
