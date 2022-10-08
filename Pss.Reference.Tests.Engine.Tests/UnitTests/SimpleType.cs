using System.ComponentModel.DataAnnotations;

namespace Pss.Reference.Engine.Tests.UnitTests;

internal class SimpleType
{
	[Required]
	public string Name { get; set; }

	[Required]
	public int? Value { get; set; }
}