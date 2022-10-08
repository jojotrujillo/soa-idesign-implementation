using System.Globalization;
using System.Text;

namespace Pss.Reference.Common.Extensions;

public static class StringExtensions
{
	public static byte[] ToBytes(this string value)
	{
		return Encoding.UTF8.GetBytes(value);
	}

	public static string FromBytes(this byte[] value)
	{
		return Encoding.UTF8.GetString(value);
	}

	public static int? ToInt(this string value)
	{
		if (value == null)
			return null;

		if (!int.TryParse(value, out var result))
			return null;

		return result;
	}

	public static bool? ToBool(this string value)
	{
		if (string.IsNullOrWhiteSpace(value))
			return null;

		if (!bool.TryParse(value, out var result))
			return null;

		return result;
	}

	public static DateTime? ToDateTime(this string value)
	{
		if (value == null)
			return null;

		if (!DateTime.TryParseExact(value, "M/dd/yyyy h:mm:ss tt", CultureInfo.InvariantCulture, DateTimeStyles.None, out var result))
			return null;

		return result;
	}

	public static string Encode(this string value)
	{
		var bytes = Encoding.UTF8.GetBytes(value);
		var ticks = BitConverter.GetBytes(DateTime.Now.Ticks);
		var data = new byte[bytes.Length + ticks.Length];
		ticks.CopyTo(data, 0);
		bytes.CopyTo(data, ticks.Length);
		return Convert.ToBase64String(data);
	}

	public static string Decode(this string value)
	{
		var fromBase64 = Convert.FromBase64String(value);
		byte[] b = new byte[fromBase64.Length - sizeof(long)];
		Array.Copy(fromBase64, sizeof(long), b, 0, b.Length);
		return Encoding.UTF8.GetString(b);
	}
}
