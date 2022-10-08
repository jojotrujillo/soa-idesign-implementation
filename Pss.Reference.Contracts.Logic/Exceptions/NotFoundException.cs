using System.Runtime.Serialization;

namespace Pss.Reference.Contracts.Logic.Exceptions;

public sealed class NotFoundException : Exception
{
	public NotFoundException()
	{
	}

	public NotFoundException(string message) : base(message)
	{
	}

	public NotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
	{
	}

	public NotFoundException(string message, Exception innerException) : base(message, innerException)
	{
	}
}
