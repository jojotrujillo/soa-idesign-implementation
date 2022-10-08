using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pss.Reference.Shared.Tests;

namespace Pss.Reference.WebApi.Tests;

[TestClass]
public sealed class AssenblyInitializeWebApi : AssemblyInitialize
{
	[AssemblyInitialize]
	public static void Initialize(TestContext context)
	{
		_ = context;

		InitializeAssembly();
	}
}
