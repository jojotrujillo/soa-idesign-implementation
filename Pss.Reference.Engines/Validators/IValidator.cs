using Pss.Reference.Contracts.Logic.Validations;

namespace Pss.Reference.Engines.Validators;

public interface IValidator
{
	ValidationResult Validate(KeyValuePair<string, object> input);
}
