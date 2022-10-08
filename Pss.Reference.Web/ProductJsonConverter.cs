using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using Client = Pss.Reference.Contracts.Client.Products;

namespace Pss.Reference.Web;

public class ProductJsonConverter : JsonConverter<Client.ProductBase[]>
{
	private static readonly PropertyInfo[] _commodityProperties = typeof(Client.Commodity).GetProperties(BindingFlags.Instance | BindingFlags.Public);
	private static readonly PropertyInfo[] _salonProductProperties = typeof(Client.SalonProduct).GetProperties(BindingFlags.Instance | BindingFlags.Public);
	private static readonly PropertyInfo[] _vehicleProperties = typeof(Client.Vehicle).GetProperties(BindingFlags.Instance | BindingFlags.Public);
	private static readonly PropertyInfo[] _allProperties = _commodityProperties.Union(_salonProductProperties).Union(_vehicleProperties).Distinct().ToArray();

	public override bool CanConvert(Type typeToConvert) => typeof(Client.ProductBase[]).IsAssignableFrom(typeToConvert);

	public override Client.ProductBase[] Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		Dictionary<string, object> entity = null;
		string currentProperty = string.Empty;
		List<Client.ProductBase> products = new List<Client.ProductBase>();

		while (reader.Read())
		{
			switch (reader.TokenType)
			{
				case JsonTokenType.StartObject:
					entity = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
					break;
				case JsonTokenType.EndObject:
					Client.ProductType productType = (Client.ProductType)entity["ProductType"];
					products.Add(DeserializeProductType(entity, productType));
					break;
				case JsonTokenType.StartArray:
					break;
				case JsonTokenType.EndArray:
					break;
				case JsonTokenType.String:
					entity.Add(currentProperty, reader.GetString());
					break;
				case JsonTokenType.True:
				case JsonTokenType.False:
					entity.Add(currentProperty, reader.GetBoolean());
					break;
				case JsonTokenType.Number:
					entity.Add(currentProperty, GetTypeValue(reader, currentProperty));
					break;
				case JsonTokenType.PropertyName:
					currentProperty = reader.GetString();
					break;
			}
		}

		return products.ToArray();
	}

	private static object GetTypeValue(Utf8JsonReader reader, string currentProperty)
	{
		Type propertyType = _allProperties.First(p => p.Name.Equals(currentProperty, StringComparison.OrdinalIgnoreCase)).PropertyType;
		switch (propertyType)
		{
			case Type intValue when intValue == typeof(int):
				return reader.GetInt32();
			case Type nullableInt when nullableInt == typeof(int?):
				return reader.GetInt32();
			case Type boolValue when boolValue == typeof(bool):
				return reader.GetBoolean();
			case Type decimalValue when decimalValue == typeof(decimal?):
				return reader.GetDecimal();
			case Type decimalValue when decimalValue == typeof(decimal):
				return reader.GetDecimal();
			case Type stringValue when stringValue == typeof(string):
				return reader.GetString();
			case Type enumType when enumType == typeof(Client.ProductType):
				return (Client.ProductType)reader.GetInt32();
			case Type shortValue when shortValue == typeof(short):
				return reader.GetInt16();
			default:
				return reader.GetString();
		}
	}

	private Client.ProductBase DeserializeProductType(Dictionary<string, object> entity, Client.ProductType productType)
	{
		Client.ProductBase product = null;

		if (productType == Client.ProductType.Commodity)
			product = new Client.Commodity();
		else if (productType == Client.ProductType.SalonProduct)
			product = new Client.SalonProduct();
		else if (productType == Client.ProductType.Vehicle)
			product = new Client.Vehicle();

		foreach (var property in _allProperties)
		{
			if (entity.ContainsKey(property.Name))
				property.SetValue(product, entity[property.Name]);
		}

		return product;
	}

	public override void Write(Utf8JsonWriter writer, Client.ProductBase[] product, JsonSerializerOptions options)
	{
		writer.WriteStartArray();

		foreach (var item in product)
		{
			writer.WriteStartObject();
			WriteProductProperties(writer, item);
			writer.WriteEndObject();
		}

		writer.WriteEndArray();
	}

	private static void SetTypeValue(Utf8JsonWriter writer, PropertyInfo property, Client.ProductBase product)
	{
		switch (property.PropertyType)
		{
			case Type intValue when intValue == typeof(int):
				writer.WriteNumberValue((int)property.GetValue(product));
				break;
			case Type nullableInt when nullableInt == typeof(int?):
				writer.WriteNumberValue((int)property.GetValue(product));
				break;
			case Type decimalNullableValue when decimalNullableValue == typeof(decimal?):
				writer.WriteNumberValue((decimal)property.GetValue(product));
				break;
			case Type decimalValue when decimalValue == typeof(decimal):
				writer.WriteNumberValue((decimal)property.GetValue(product));
				break;
			case Type enumType when enumType == typeof(Client.ProductType):
				writer.WriteNumberValue((int)property.GetValue(product));
				break;
			case Type shortValue when shortValue == typeof(short):
				writer.WriteNumberValue((short)property.GetValue(product));
				break;
			case Type boolValue when boolValue == typeof(bool):
				writer.WriteBooleanValue((bool)property.GetValue(product));
				break;
			case Type stringValue when stringValue == typeof(string):
				writer.WriteStringValue((string)property.GetValue(product));
				break;
			default:
				writer.WriteStringValue((string)property.GetValue(product));
				break;
			}
	}

	private static void WriteProductProperties(Utf8JsonWriter writer, Client.ProductBase product)
	{
		PropertyInfo[] propertyInfos = null;
		if (product.GetType() == typeof(Client.Commodity))
			propertyInfos = _commodityProperties;
		else if (product.GetType() == typeof(Client.SalonProduct))
			propertyInfos = _salonProductProperties;
		else if (product.GetType() == typeof(Client.Vehicle))
			propertyInfos = _vehicleProperties;

		foreach (var propertyInfo in propertyInfos)
		{
			writer.WritePropertyName(propertyInfo.Name);
			SetTypeValue(writer, propertyInfo, product);
		}
	}
}
