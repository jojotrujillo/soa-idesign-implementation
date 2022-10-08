using Pss.Reference.Common;
using Pss.Reference.Common.Contracts;
using Pss.Reference.Shared.Tests;
using Pss.Reference.Utilities;
using Pss.Reference.Utilities.Contracts;
using Pss.Reference.Utility.Tests.SupportingTypes;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Pss.Reference.Utility.Tests.IntegrationTests;

[TestClass]
[TestCategory(Constants.Testing.IntegrationTest)]
public class CacheUtilityTests
{
	#region Fields
	private static readonly string _cacheKey = Guid.NewGuid().ToString();
	const string _testString = "This is a test of the emergency broadcast system. This is only a test.";
	#endregion

	#region Properties
	private UtilityFactory UtilityFactory => new UtilityFactory(new AmbientContext(), AssemblyInitialize.ServiceProvider);
	#endregion

	[TestMethod]
	public void SetTypeT_WithDataContract_MatchesGetTypeT()
	{
		// ARRANGE
		string name = Guid.NewGuid().ToString();
		int value = new Random(DateTime.Now.Millisecond).Next(1, int.MaxValue);
		MyDataContract dataContract = new MyDataContract { Name = name, Value = value };

		// Prove the resolution of the cache utility from the factory interacts with the same underlying cache instance.
		ICacheUtility cacheUtilitySet = UtilityFactory.Create<ICacheUtility>();
		ICacheUtility cacheUtilityGet = UtilityFactory.Create<ICacheUtility>();

		// ACT
		cacheUtilitySet.Set(_cacheKey, dataContract);
		MyDataContract result = cacheUtilityGet.Get<MyDataContract>(_cacheKey);

		// ASSERT
		Assert.IsNotNull(result);
		Assert.AreEqual(name, result.Name);
		Assert.AreEqual(value, result.Value);
	}

	[TestMethod]
	public void SetString_StringValue_MatchesGet()
	{
		// ARRANGE
		ICacheUtility cacheUtility = UtilityFactory.Create<ICacheUtility>();

		// ACT
		cacheUtility.SetString(_cacheKey, _testString);

		// ASSERT
		Assert.AreEqual(_testString, cacheUtility.GetString(_cacheKey));
	}

	[TestMethod]
	public void SetString_Remove_ResultsInCacheMiss()
	{
		// ARRANGE
		ICacheUtility cacheUtility = UtilityFactory.Create<ICacheUtility>();
		cacheUtility.SetString(_cacheKey, _testString);

		// ACT
		cacheUtility.Remove(_cacheKey);

		// ASSERT
		Assert.IsNull(cacheUtility.GetString(_cacheKey));
	}

	[TestMethod]
	public void SetTypeTWithOptions_ExpiredCacheEntry_ResultsInCacheMiss()
	{
		// ARRANGE
		string name = Guid.NewGuid().ToString();
		int value = new Random(DateTime.Now.Millisecond).Next(1, int.MaxValue);
		MyDataContract dataContract = new MyDataContract { Name = name, Value = value };

		ICacheUtility cacheUtility = UtilityFactory.Create<ICacheUtility>();
		DistributedCacheEntryOptions options = new DistributedCacheEntryOptions { AbsoluteExpirationRelativeToNow = TimeSpan.FromMilliseconds(100) };

		// ACT
		cacheUtility.Set(_cacheKey, dataContract, options);
		MyDataContract cacheHit = cacheUtility.Get<MyDataContract>(_cacheKey);
		System.Threading.Thread.Sleep(250);
		MyDataContract cacheMiss = cacheUtility.Get<MyDataContract>(_cacheKey);

		// ASSERT
		Assert.IsNotNull(cacheHit);
		Assert.IsNull(cacheMiss);
	}

	[TestMethod]
	public void SetTypeTWithOptions_UnExpiredCacheEntry_ResultsInCacheHit()
	{
		// ARRANGE
		string name = Guid.NewGuid().ToString();
		int value = new Random(DateTime.Now.Millisecond).Next(1, int.MaxValue);
		MyDataContract dataContract = new MyDataContract { Name = name, Value = value };

		ICacheUtility cacheUtility = UtilityFactory.Create<ICacheUtility>();
		DistributedCacheEntryOptions options = new DistributedCacheEntryOptions { AbsoluteExpirationRelativeToNow = TimeSpan.FromMilliseconds(1000) };

		// ACT
		cacheUtility.Set(_cacheKey, dataContract, options);
		System.Threading.Thread.Sleep(250);
		MyDataContract cacheHit = cacheUtility.Get<MyDataContract>(_cacheKey);

		// ASSERT
		Assert.IsNotNull(cacheHit);
		Assert.AreEqual(name, cacheHit.Name);
		Assert.AreEqual(value, cacheHit.Value);
	}

	[TestMethod]
	public void SetStringWithOptions_ExpiredCacheEntry_ResultsInCacheMiss()
	{
		// ARRANGE
		ICacheUtility cacheUtility = UtilityFactory.Create<ICacheUtility>();
		DistributedCacheEntryOptions options = new DistributedCacheEntryOptions { AbsoluteExpirationRelativeToNow = TimeSpan.FromMilliseconds(100) };

		// ACT
		cacheUtility.SetString(_cacheKey, _testString, options);
		string cacheHit = cacheUtility.GetString(_cacheKey);
		System.Threading.Thread.Sleep(250);
		string cacheMiss = cacheUtility.GetString(_cacheKey);

		// ASSERT
		Assert.IsNotNull(cacheHit);
		Assert.IsNull(cacheMiss);
	}

	[TestMethod]
	public void SetStringWithOptions_UnExpiredCacheEntry_ResultsInCacheHit()
	{
		// ARRANGE
		ICacheUtility cacheUtility = UtilityFactory.Create<ICacheUtility>();
		DistributedCacheEntryOptions options = new DistributedCacheEntryOptions { AbsoluteExpirationRelativeToNow = TimeSpan.FromMilliseconds(1000) };

		// ACT
		cacheUtility.SetString(_cacheKey, _testString, options);
		System.Threading.Thread.Sleep(250);
		string cacheHit = cacheUtility.GetString(_cacheKey);

		// ASSERT
		Assert.IsNotNull(cacheHit);
		Assert.AreEqual(_testString, cacheHit);
	}

	[TestMethod]
	public void SetStringWithOptions_RefreshBeforeExpiration_ResultsInCacheHit()
	{
		// ARRANGE
		ICacheUtility cacheUtility = UtilityFactory.Create<ICacheUtility>();
		DistributedCacheEntryOptions options = new DistributedCacheEntryOptions { SlidingExpiration = TimeSpan.FromMilliseconds(250) };

		// ACT
		cacheUtility.SetString(_cacheKey, _testString, options);
		string cacheHit1 = cacheUtility.GetString(_cacheKey);
		System.Threading.Thread.Sleep(100);
		cacheUtility.Refresh(_cacheKey);
		string cacheHit2 = cacheUtility.GetString(_cacheKey);
		System.Threading.Thread.Sleep(100);
		cacheUtility.Refresh(_cacheKey);
		string cacheHit3 = cacheUtility.GetString(_cacheKey);
		System.Threading.Thread.Sleep(100);
		cacheUtility.Refresh(_cacheKey);
		string cacheHit4 = cacheUtility.GetString(_cacheKey);
		System.Threading.Thread.Sleep(100);
		cacheUtility.Refresh(_cacheKey);
		string cacheHit5 = cacheUtility.GetString(_cacheKey);

		// ASSERT
		Assert.IsNotNull(cacheHit1);
		Assert.IsNotNull(cacheHit2);
		Assert.IsNotNull(cacheHit3);
		Assert.IsNotNull(cacheHit4);
		Assert.IsNotNull(cacheHit5);
	}
}
