using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

#if NETCOREAPP3_0_OR_GREATER
using System.Text.Json;
using System.Text.Json.Serialization;
#endif

namespace fiskaltrust.ifPOS.v2.Converters
{
  public class StringOrStringArrayNewtonsoftConverter : Newtonsoft.Json.JsonConverter<string[]>
  {
    public override string[] ReadJson(JsonReader reader, Type objectType, string[] existingValue, bool hasExistingValue, Newtonsoft.Json.JsonSerializer serializer)
    {
      if (reader.TokenType == JsonToken.Null)
        return null;

      if (reader.TokenType == JsonToken.StartArray)
      {
        var array = JArray.Load(reader);
        return array.ToObject<string[]>();
      }

      if (reader.TokenType == JsonToken.String)
      {
        string value = (string)reader.Value;
        return new string[] { value };
      }

      throw new JsonSerializationException($"Unexpected token {reader.TokenType} when parsing string array");
    }

    public override void WriteJson(JsonWriter writer, string[] value, Newtonsoft.Json.JsonSerializer serializer)
    {
      if (value == null)
      {
        writer.WriteNull();
        return;
      }

      if (value.Length == 1)
      {
        serializer.Serialize(writer, value[0]);
      }
      else
      {
        serializer.Serialize(writer, value);
      }
    }
  }

#if NETCOREAPP3_0_OR_GREATER
  public class StringOrStringArraySystemTextJsonConverter : System.Text.Json.Serialization.JsonConverter<string[]>
  {
    public override string[] Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
      if (reader.TokenType == JsonTokenType.Null)
        return null;

      if (reader.TokenType == JsonTokenType.StartArray)
      {
        return System.Text.Json.JsonSerializer.Deserialize<string[]>(ref reader, options);
      }

      if (reader.TokenType == JsonTokenType.String)
      {
        string value = reader.GetString();
        return new string[] { value };
      }

      throw new System.Text.Json.JsonException($"Unexpected token {reader.TokenType} when parsing string array");
    }

    public override void Write(Utf8JsonWriter writer, string[] value, JsonSerializerOptions options)
    {
      if (value == null)
      {
        writer.WriteNullValue();
        return;
      }

      if (value.Length == 1)
      {
        System.Text.Json.JsonSerializer.Serialize(writer, value[0], options);
      }
      else
      {
        System.Text.Json.JsonSerializer.Serialize(writer, value, options);
      }
    }
  }
#endif
}
