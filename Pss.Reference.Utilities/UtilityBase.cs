using Pss.Reference.Common;

[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("Pss.Reference.Accessor.Tests")]
[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("Pss.Reference.Utility.Tests")]
[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("Pss.Reference.Manager.Tests")]
[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("DynamicProxyGenAssembly2")]

namespace Pss.Reference.Utilities;

internal abstract class UtilityBase : ServiceBase
{
	protected UtilityBase()
	{
	}
}
