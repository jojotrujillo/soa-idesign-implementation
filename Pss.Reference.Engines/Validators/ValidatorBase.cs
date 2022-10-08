using Pss.Reference.Contracts.Logic.Validations;

namespace Pss.Reference.Engines.Validators;

internal abstract class ValidatorBase
{
	protected static ValidationResult Ok()
	{
		return new ValidationResult { IsValid = true };
	}

	protected static ValidationResult BadRequest(string field, string error)
	{
		return new ValidationResult
		{
			Field = field,
			ErrorMessage = error,
			IsValid = false
		};
	}
}
