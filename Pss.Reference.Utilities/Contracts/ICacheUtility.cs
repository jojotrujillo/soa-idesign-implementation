using Microsoft.Extensions.Caching.Distributed;
using Pss.Reference.Common.Contracts;

namespace Pss.Reference.Utilities.Contracts;

public interface ICacheUtility : IServiceComponent
{
	T Get<T>(string key) where T : class;

	string GetString(string key);

	void Refresh(string key);

	void Remove(string key);

	void Set<T>(string key, T dataContract) where T : class;

	void Set<T>(string key, T dataContract, DistributedCacheEntryOptions options) where T : class;

	void SetString(string key, string value);

	void SetString(string key, string value, DistributedCacheEntryOptions options);
}
