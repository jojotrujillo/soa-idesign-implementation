using Pss.Reference.Shared.Tests;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Pss.Reference.Engine.Tests;

[TestClass]
public sealed class AssemblyInitializeEngine : AssemblyInitialize
{
	[AssemblyInitialize]
	public static void Initialize(TestContext context)
	{
		_ = context;

		InitializeAssembly();
	}
}
