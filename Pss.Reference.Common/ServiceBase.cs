using System.Diagnostics;
using Pss.Reference.Common.Contracts;

namespace Pss.Reference.Common;

public abstract class ServiceBase : IServiceComponent
{
	public AmbientContext Context { get; set; }
	public FactoryBase Factory { get; set; }

	public virtual string TestMe(string input)
	{
		var result = $"{input} : {GetType().Name}[{Context.CorrelationId}]";
		Trace.WriteLine(result);
		return result;
	}
}
