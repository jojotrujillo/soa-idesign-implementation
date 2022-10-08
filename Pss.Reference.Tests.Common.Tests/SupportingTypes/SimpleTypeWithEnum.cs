namespace Pss.Reference.Common.Tests.SupportingTypes;

internal enum TestEnum
{
	Value0,
	Value1
}

internal class SimpleTypeWithEnum
{
	public string Name { get; set; }
	public int? Value { get; set; }
	public TestEnum EnumValue { get; set; }
}
