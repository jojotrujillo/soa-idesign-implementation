using Microsoft.Extensions.Caching.Distributed;
using Pss.Reference.Common.Extensions;
using Pss.Reference.Utilities.Contracts;

namespace Pss.Reference.Utilities;

internal class DistributedCacheUtility : UtilityBase, ICacheUtility
{
	#region Fields
	private static IDistributedCache _cacheInternal;
	private static readonly object _syncObject = new();
	#endregion

	#region Properties
	private IDistributedCache CacheInternal
	{
		get
		{
			// NOTE: The underlying cache must be a singleton.
			if (_cacheInternal == null)
			{
				lock (_syncObject)
				{
					if (_cacheInternal == null)
						_cacheInternal = Factory.ResolveRequiredService<IDistributedCache>();
				}
			}

			return _cacheInternal;
		}
	}
	#endregion

	public T Get<T>(string key) where T : class
	{
		return CacheInternal.Get(key)?.FromBytes<T>();
	}

	public string GetString(string key)
	{
		return CacheInternal.GetString(key);
	}

	public void Refresh(string key)
	{
		CacheInternal.Refresh(key);
	}

	public void Remove(string key)
	{
		CacheInternal.Remove(key);
	}

	public void Set<T>(string key, T dataContract) where T : class
	{
		CacheInternal.Set(key, dataContract.ToBytes());
	}

	public void Set<T>(string key, T dataContract, DistributedCacheEntryOptions options) where T : class
	{
		CacheInternal.Set(key, dataContract.ToBytes(), options);
	}

	public void SetString(string key, string value)
	{
		CacheInternal.SetString(key, value);
	}

	public void SetString(string key, string value, DistributedCacheEntryOptions options)
	{
		CacheInternal.SetString(key, value, options);
	}
}
