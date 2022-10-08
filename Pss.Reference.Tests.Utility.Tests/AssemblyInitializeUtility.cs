using Pss.Reference.Shared.Tests;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Pss.Reference.Utility.Tests;

[TestClass]
public sealed class AssemblyInitializeUtility : AssemblyInitialize
{
	[AssemblyInitialize]
	public static void Initialize(TestContext context)
	{
		_ = context;

		InitializeAssembly();
	}
}
