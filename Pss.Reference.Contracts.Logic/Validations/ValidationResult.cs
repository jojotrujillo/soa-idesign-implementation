using Newtonsoft.Json;

namespace Pss.Reference.Contracts.Logic.Validations;

public class ValidationResult
{
	public string Field { get; set; }

	public string ErrorMessage { get; set; }

	[JsonIgnore]
	public bool IsValid { get; set; }
}

