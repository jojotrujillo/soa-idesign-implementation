using System.Runtime.Serialization;
using Pss.Reference.Common;
using Pss.Reference.Contracts.Logic.Validations;

namespace Pss.Reference.Contracts.Logic.Exceptions;

public sealed class ValidationException : Exception
{
	public ValidationException()
	{
	}

	public ValidationException(string message) : base(message)
	{
	}

	public ValidationException(string message, Exception innerException) : base(message, innerException)
	{
	}

	public ValidationException(SerializationInfo info, StreamingContext context) : base(info, context)
	{
	}

	public ValidationException(ValidationResult[] validationResults)
	{
		Data.Add(Constants.ValidationResults, validationResults);
	}
}
