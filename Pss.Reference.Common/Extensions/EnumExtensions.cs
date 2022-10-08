namespace Pss.Reference.Common.Extensions;

public static class EnumExtensions
{
	public static T ToEnumType<T>(this char? value, T defaultValue) where T : struct
	{
		var stringValue = value?.ToString();
		var enumValue = stringValue.ToNullableEnumType<T>(defaultValue);
		return enumValue ?? defaultValue;
	}

	public static T ToEnumType<T>(this string value, T defaultValue) where T : struct
	{
		var enumValue = value.ToNullableEnumType<T>(defaultValue);
		return enumValue ?? defaultValue;
	}

	public static T? ToNullableEnumType<T>(this char? value, T? defaultValue) where T : struct
	{
		var stringValue = value?.ToString();
		return stringValue.ToNullableEnumType(defaultValue);
	}

	public static T? ToNullableEnumType<T>(this string value, T? defaultValue) where T : struct
	{
		if (string.IsNullOrEmpty(value))
			return defaultValue;

		if (Enum.TryParse<T>(value, out var tryParseValue))
			return tryParseValue;

		var val = (T)Enum.ToObject(typeof(T), value[0]);

		if ((!string.IsNullOrWhiteSpace(value) && value.Length > 1) || !Enum.IsDefined(typeof(T), val))
			throw new InvalidOperationException($"Invalid {typeof(T).Name} value: [{value}].");

		return val;
	}
}
