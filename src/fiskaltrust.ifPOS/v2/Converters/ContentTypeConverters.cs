using System;
using System.Net.Mime;
using Newtonsoft.Json;

#if !WCF
using System.Text.Json;
using System.Text.Json.Serialization;
#endif

namespace fiskaltrust.ifPOS.v2.Converters
{
  /// <summary>
  /// Newtonsoft.Json converter for ContentType that serializes/deserializes as a string.
  /// </summary>
  public class ContentTypeNewtonsoftConverter : Newtonsoft.Json.JsonConverter<ContentType>
  {
    public override ContentType ReadJson(JsonReader reader, Type objectType, ContentType existingValue, bool hasExistingValue, Newtonsoft.Json.JsonSerializer serializer)
    {
      if (reader.TokenType == JsonToken.Null)
        return null;

      if (reader.TokenType == JsonToken.String)
      {
        var contentTypeString = (string)reader.Value;
        if (string.IsNullOrEmpty(contentTypeString))
          return null;

        return new ContentType(contentTypeString);
      }

      throw new JsonSerializationException($"Unexpected token {reader.TokenType} when parsing ContentType");
    }

    public override void WriteJson(JsonWriter writer, ContentType value, Newtonsoft.Json.JsonSerializer serializer)
    {
      if (value == null)
      {
        writer.WriteNull();
        return;
      }

      writer.WriteValue(value.ToString());
    }
  }

#if !WCF
    /// <summary>
    /// System.Text.Json converter for ContentType that serializes/deserializes as a string.
    /// </summary>
    public class ContentTypeSystemTextJsonConverter : System.Text.Json.Serialization.JsonConverter<ContentType>
    {
        public override ContentType Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.Null)
                return null;

            if (reader.TokenType == JsonTokenType.String)
            {
                var contentTypeString = reader.GetString();
                if (string.IsNullOrEmpty(contentTypeString))
                    return null;
                
                return new ContentType(contentTypeString);
            }

            throw new System.Text.Json.JsonException($"Unexpected token {reader.TokenType} when parsing ContentType");
        }

        public override void Write(Utf8JsonWriter writer, ContentType value, JsonSerializerOptions options)
        {
            if (value == null)
            {
                writer.WriteNullValue();
                return;
            }

            writer.WriteStringValue(value.ToString());
        }
    }
#endif
}
