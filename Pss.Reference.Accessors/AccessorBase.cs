using Pss.Reference.Common;
using Pss.Reference.Utilities;

[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("Pss.Reference.Accessor.Tests")]
[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("Pss.Reference.Manager.Tests")]
[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("DynamicProxyGenAssembly2")]

namespace Pss.Reference.Accessors;

internal abstract class AccessorBase : ServiceBase
{
	internal UtilityFactory UtilityFactory { get; set; }

	protected AccessorBase()
	{
	}
}
