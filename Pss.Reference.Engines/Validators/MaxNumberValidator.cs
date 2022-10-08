using Pss.Reference.Contracts.Logic.Validations;

namespace Pss.Reference.Engines.Validators;

internal class MaxNumberValidator<T> : ValidatorBase, IValidator where T : IComparable
{
	private readonly T _maxValue;

	public MaxNumberValidator(T maxValue)
	{
		_maxValue = maxValue;
	}

	public ValidationResult Validate(KeyValuePair<string, object> input)
	{
		if (input.Value is null)
			return BadRequest(input.Key, $"Invalid '{input.Key}' value. A value is required.");

		var actualValue = (T)input.Value;

		if (actualValue.CompareTo(_maxValue) > 0)
			return BadRequest(input.Key, $"Invalid '{input.Key}' value. The value must be less than or equal to {_maxValue}.");

		return Ok();
	}
}
