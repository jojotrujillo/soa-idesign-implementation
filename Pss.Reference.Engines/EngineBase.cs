using Pss.Reference.Accessors;
using Pss.Reference.Common;
using Pss.Reference.Utilities;

[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("Pss.Reference.Engine.Tests")]
[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("Pss.Reference.Manager.Tests")]
[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("DynamicProxyGenAssembly2")]

namespace Pss.Reference.Engines;

internal abstract class EngineBase : ServiceBase
{
	internal AccessorFactory AccessorFactory { get; set; }
	internal UtilityFactory UtilityFactory { get; set; }

	protected EngineBase()
	{
	}
}
