using Pss.Reference.Contracts.Logic.Validations;

namespace Pss.Reference.Engines.Validators;

internal class StringValidator : ValidatorBase, IValidator
{
	private readonly int _maxLength;
	private readonly bool _isOptional;

	public StringValidator(int maxLength, bool isOptional)
	{
		_maxLength = maxLength;
		_isOptional = isOptional;
	}

	public ValidationResult Validate(KeyValuePair<string, object> input)
	{
		var inputValue = (string)input.Value;

		if (!_isOptional && string.IsNullOrWhiteSpace(inputValue))
			return BadRequest(input.Key, $"'{input.Key}' is required.");

		if (_isOptional && string.IsNullOrWhiteSpace(inputValue))
			return Ok();

		if (inputValue.Length > _maxLength)
			return BadRequest(input.Key, $"Invalid '{input.Key}' value.  Must be less than or equal to {_maxLength} characters.");

		return Ok();
	}
}

