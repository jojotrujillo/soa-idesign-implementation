using Pss.Reference.Shared.Tests;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Pss.Reference.Accessor.Tests;

[TestClass]
public sealed class AssemblyInitializeAccessor : AssemblyInitialize
{
	[AssemblyInitialize]
	public static void Initialize(TestContext context)
	{
		_ = context;

		InitializeAssembly();
	}
}
