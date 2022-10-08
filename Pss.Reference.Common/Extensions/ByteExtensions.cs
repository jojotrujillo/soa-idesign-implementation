using Newtonsoft.Json;

namespace Pss.Reference.Common.Extensions;

public static class ByteExtensions
{
	public static byte[] ToBytes<TContract>(this TContract dataContract) where TContract : class
	{
		return dataContract?.ToJson().ToBytes();
	}

	public static TContract FromBytes<TContract>(this byte[] dataContract) where TContract : class
	{
		return dataContract?.FromBytes()?.FromJson<TContract>();
	}

	public static TContract FromBytes<TContract>(this byte[] dataContract, Type type) where TContract : class
	{
		System.Diagnostics.Debug.Assert(typeof(TContract) == type);
		return JsonConvert.DeserializeObject(dataContract.FromBytes(), type) as TContract;
	}
}
