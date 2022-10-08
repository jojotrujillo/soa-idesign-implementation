using System.ComponentModel.DataAnnotations;

namespace Pss.Reference.Engines.Extensions;

internal static class ObjectExtensions
{
	public static Dictionary<string, object> DataContractToDictionary(this object dataContract)
	{
		if (dataContract == null)
			return new Dictionary<string, object>(0);

		return dataContract
			.GetType()
			.GetProperties()
			.Where(info => info.IsDefined(typeof(RequiredAttribute), true) || info.GetValue(dataContract, null) != null)
			.ToDictionary(info => info.Name, info => info.GetValue(dataContract, null), StringComparer.OrdinalIgnoreCase);
	}
}
