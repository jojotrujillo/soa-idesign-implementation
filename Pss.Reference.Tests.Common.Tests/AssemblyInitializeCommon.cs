using Pss.Reference.Shared.Tests;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Pss.Reference.Common.Tests;

[TestClass]
public sealed class AssemblyInitializeCommon : AssemblyInitialize
{
	[AssemblyInitialize]
	public static void Initialize(TestContext context)
	{
		_ = context;

		InitializeAssembly();
	}
}
