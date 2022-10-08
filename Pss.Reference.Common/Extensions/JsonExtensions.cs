using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Pss.Reference.Common.Extensions;

public static class JsonExtensions
{
	public static string ToJson<TType>(this TType entity)
	{
		return entity.ToJson(true, true, false, false, false);
	}

	public static string ToJson<TType>(this TType entity, bool isCamelCasingPreferred, bool areNullsOmitted, bool useIndentedFormatting, bool outputEnumsUsingName)
	{
		return entity.ToJson(isCamelCasingPreferred, areNullsOmitted, useIndentedFormatting, outputEnumsUsingName, false);
	}

	public static string ToJson<TType>(this TType entity, bool isCamelCasingPreferred, bool areNullsOmitted, bool useIndentedFormatting, bool outputEnumsUsingName, bool isIgnoreDefault)
	{
		if (entity == null)
			return null;

		var serializationSettings = new JsonSerializerSettings();

		if (isCamelCasingPreferred)
			serializationSettings.ContractResolver = new DefaultContractResolver { NamingStrategy = new CamelCaseNamingStrategy() };

		if (areNullsOmitted)
			serializationSettings.NullValueHandling = NullValueHandling.Ignore;

		if (useIndentedFormatting)
			serializationSettings.Formatting = Formatting.Indented;

		if (outputEnumsUsingName)
			serializationSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());

		if (isIgnoreDefault)
			serializationSettings.DefaultValueHandling = DefaultValueHandling.Ignore;

		var json = JsonConvert.SerializeObject(entity, serializationSettings);
		if (json == "{}" && areNullsOmitted)
			return null;

		return json;
	}

	public static TType FromJson<TType>(this string json)
	{
		var result = JsonConvert.DeserializeObject<TType>(json);
		return result;
	}

	public static TType FromJson<TType>(this string json, string dateTimeFormatter)
	{
		var result = JsonConvert.DeserializeObject<TType>(json, new JsonSerializerSettings { DateFormatString = dateTimeFormatter });
		return result;
	}

	public static object FromJson(this string json, string typeName)
	{
		var result = JsonConvert.DeserializeObject(json, Type.GetType(typeName));
		return result;
	}

	public static TType ToObjectFromJsonFile<TType>(this string filename)
	{
		using var file = File.OpenText(filename);
		using var reader = new JsonTextReader(file);
		var serializer = new JsonSerializer();
		return serializer.Deserialize<TType>(reader);
	}

	public static async Task<TType> FromJson<TType>(this HttpResponseMessage response) where TType : class
	{
		if (response == null)
			return null;

		return (await response.Content.ReadAsStringAsync()).FromJson<TType>();
	}
}
