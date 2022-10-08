using Pss.Reference.Common.Contracts;
using Pss.Reference.Contracts.Logic.Validations;

namespace Pss.Reference.Engines.Contracts;

public interface IValidationEngine : IServiceComponent
{
	ValidationResult[] Validate(object request);
}
