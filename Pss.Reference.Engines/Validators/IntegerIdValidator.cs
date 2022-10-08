using Pss.Reference.Contracts.Logic.Validations;

namespace Pss.Reference.Engines.Validators;

internal class IntegerIdValidator : ValidatorBase, IValidator
{
	private readonly bool _isOptional;

	public IntegerIdValidator(bool isOptional)
	{
		_isOptional = isOptional;
	}

	public ValidationResult Validate(KeyValuePair<string, object> input)
	{
		// Allow all non-zero numbers.
		int? inputValue = (int?)input.Value;

		if (_isOptional && (!inputValue.HasValue || inputValue == default(int)))
			return Ok();

		if (!inputValue.HasValue || inputValue <= 0)
		{
			return BadRequest(input.Key, $"Invalid '{input.Key}' value.  Must be greater than zero.");
		}

		return Ok();
	}
}

