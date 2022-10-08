using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace Pss.Reference.Shared.Tests;

public static class TestHelper
{
	private static readonly Random _random = new();

	public static void ValidatePropertyCount<TExpected, TResult>(TExpected expected, TResult result, int difference = 0)
	{
		ValidatePropertyCount(result, expected.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public).Length - difference);
	}

	public static void ValidatePropertyCount<TObject>(TObject instance, int numProperties)
	{
		var count = instance.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public).Length;
		Assert.AreEqual(numProperties, count,
			$"The number of properties in type {instance.GetType().Name} does not match the indicated number covered in the Unit Test!");
	}

	public static void ValidatePropertyValues<TExpected, TResult>(TExpected expected, TResult result, params string[] ignoreProperties)
	{
		if (expected == null && result != null)
			Assert.Fail("Expected object is null while result object is not!");
		else if (expected != null && result == null)
			Assert.Fail("Expected object is not null while result object is!");
		if (expected == null && result == null)
			return;

		var expectedProperties = expected.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);
		var resultProperties = result.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);

		foreach (var property in expectedProperties)
		{
			if (ignoreProperties.Any(p => property.Name == p))
				continue;

			var resultProp = resultProperties.SingleOrDefault(p => p.Name == property.Name);
			Assert.IsNotNull(resultProp, $"There is no property in the result object that matches {property.Name} from the expected object!");
			Assert.AreEqual(property.PropertyType, resultProp.PropertyType,
				$"The expected property type '{property.PropertyType}' and result property '{resultProp.PropertyType}' types do not match!");

			var expectedValue = property.GetValue(expected);
			var resultValue = resultProp.GetValue(result);

			if (ValidateClass(property.PropertyType, expectedValue, resultValue))
				continue;

			Assert.AreEqual(expectedValue, resultValue, $"The expected and result property values do not match for \"{property.Name}\"! (Expected: {expectedValue} != Actual: {resultValue})");
		}
	}

	public static void ValidateCollection<TExpected, TResult>(TExpected[] expected, TResult[] result, Func<TExpected, TResult, bool> comparisonPredicate,
															  Action<TExpected, TResult> validateItem = null, params string[] ignoreProperties)
	{
		if (expected == null && result != null)
			Assert.Fail("Expected array is null while result array is not!");
		else if (expected != null && result == null)
			Assert.Fail("Expected array is not null while result array is!");
		else if (expected.Length != result.Length)
			Assert.Fail("The length of the expected array does not match the length of the result array!");

		foreach (var item in expected)
		{
			bool found = false;
			foreach (var actual in result)
			{
				if (comparisonPredicate(item, actual))
				{
					found = true;
					ValidatePropertyValues(item, actual, ignoreProperties);
					validateItem?.Invoke(item, actual);
					break;
				}
			}

			if (!found)
			{
				Assert.Fail("Item in expected array doesnt exist in the result array!");
			}
		}
	}

	public static T GetRandomElement<T>(this IEnumerable<T> list)
	{
		if (list == null)
			throw new ArgumentNullException(nameof(list));

		int count = list.Count();

		if (count == 0)
			return default;

		int index = _random.Next(list.Count());

		if (count <= 100)
		{
			using (IEnumerator<T> enumerator = list.GetEnumerator())
			{
				while (index >= 0 && enumerator.MoveNext())
					index--;

				return enumerator.Current;
			}
		}

		return list.ElementAt(index);
	}

	public static IEnumerable<T> GetRandomElements<T>(this IEnumerable<T> list, int count)
	{
		if (list.Count() <= count)
			return list;

		var hashSet = new HashSet<T>();
		for (int i = 0; i < count; i++)
		{
			int index = _random.Next(list.Count());
			if (!hashSet.Contains(list.ElementAt(index)))
				hashSet.Add(list.ElementAt(index));
			else
			{
				index = _random.Next(list.Count());
				while (hashSet.Contains(list.ElementAt(index)))
				{
					index = _random.Next(list.Count());
				}

				hashSet.Add(list.ElementAt(index));
			}
		}

		return hashSet;
	}

	private static bool ValidateClass<TExpected, TResult>(Type type, TExpected expected, TResult result)
	{
		if (!type.IsClass || ReferenceEquals(expected, result))
			return false;

		if (type.IsArray)
		{
			Assert.AreEqual(JsonConvert.SerializeObject(expected), JsonConvert.SerializeObject(result));

			return true;
		}

		if (type != typeof(string))
		{
			ValidatePropertyValues(expected, result);
			return true;
		}

		return false;
	}
}
