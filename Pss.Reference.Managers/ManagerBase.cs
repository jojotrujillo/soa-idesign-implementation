using Pss.Reference.Accessors;
using Pss.Reference.Common;
using Pss.Reference.Engines;
using Pss.Reference.Utilities;

[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("Pss.Reference.Manager.Tests")]

namespace Pss.Reference.Managers;

internal abstract class ManagerBase : ServiceBase
{
	internal EngineFactory EngineFactory { get; set; }
	internal AccessorFactory AccessorFactory { get; set; }
	internal UtilityFactory UtilityFactory { get; set; }

	protected ManagerBase()
	{
	}
}
