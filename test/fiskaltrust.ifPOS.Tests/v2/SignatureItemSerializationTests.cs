using System;
using System.Globalization;
using fiskaltrust.Middleware.ifPOS.v2.Models;
using fiskaltrust.Middleware.Localization.v2.Models.ifPOS.v2.Cases;
using NUnit.Framework;
using Newtonsoft.Json;

#if NETSTANDARD2_1_TESTS
using System.Text.Json;
using System.Text.Json.Serialization;
#endif

namespace fiskaltrust.Middleware.Interface.Tests.v2
{
    [TestFixture]
    public class SignatureItemSerializationTests
    {
        private SignatureItem CreateTestSignatureItem()
        {
            return new SignatureItem
            {
                ftSignatureItemId = Guid.Parse("12345678-1234-1234-1234-123456789012"),
                ftSignatureFormat = SignatureFormat.QRCode,
                ftSignatureType = SignatureType.Unknown,
                Caption = "Test Signature",
                Data = "BASE64ENCODEDDATA=="
            };
        }

        [Test]
        public void Newtonsoft_SerializeDeserialize_PreservesAllProperties()
        {
            var original = CreateTestSignatureItem();
            var json = JsonConvert.SerializeObject(original, Formatting.Indented);
            var deserialized = JsonConvert.DeserializeObject<SignatureItem>(json);
            AssertSignatureItemsEqual(original, deserialized);
        }

#if NETSTANDARD2_1_TESTS
        [Test]
        public void SystemTextJson_SerializeDeserialize_PreservesAllProperties()
        {
            var original = CreateTestSignatureItem();
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                PropertyNamingPolicy = null,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                Converters = { new JsonStringEnumConverter() }
            };
            var json = System.Text.Json.JsonSerializer.Serialize(original, options);
            var deserialized = System.Text.Json.JsonSerializer.Deserialize<SignatureItem>(json, options);
            AssertSignatureItemsEqual(original, deserialized);
        }

        [Test]
        public void BothSerializers_ProduceSameOutput()
        {
            // Arrange
            var item = CreateTestSignatureItem();
            
            // JSON settings aligned for consistent output between Newtonsoft.Json and System.Text.Json,
            // matching formatting, dates, numbers, enums, null handling, and reference behavior,
            // based on Microsoft's migration guide.

            var newtonsoftSettings = new JsonSerializerSettings
            {
                Formatting = Formatting.None, 
                DateFormatHandling = DateFormatHandling.IsoDateFormat, 
                DateTimeZoneHandling = DateTimeZoneHandling.Utc, 
                NullValueHandling = NullValueHandling.Ignore, 
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore, 
                PreserveReferencesHandling = PreserveReferencesHandling.None, 
                FloatFormatHandling = FloatFormatHandling.DefaultValue, 
                Culture = CultureInfo.InvariantCulture, 
                Converters = { 
                    new Newtonsoft.Json.Converters.StringEnumConverter(namingStrategy: null), 
                    new NewtonsoftDateTimeConverter(), 
                    new GlobalNumberConverter() 
                }
            };

            var systemTextJsonOptions = new JsonSerializerOptions
            {
                WriteIndented = false, 
                PropertyNamingPolicy = null, 
                PropertyNameCaseInsensitive = true, 
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull, 
                NumberHandling = JsonNumberHandling.AllowReadingFromString, 
                ReferenceHandler = ReferenceHandler.IgnoreCycles, 
                ReadCommentHandling = JsonCommentHandling.Skip, 
                AllowTrailingCommas = true, 
                MaxDepth = 64, 
                Converters = {
                    new JsonStringEnumConverter(namingPolicy: null), 
                    new SystemTextJsonConverters.DateTimeConverter(), 
                    new SystemTextJsonConverters.DecimalConverter() 
                }
            };

            // Act
            var newtonsoftJson = JsonConvert.SerializeObject(item, newtonsoftSettings);
            var systemTextJson = System.Text.Json.JsonSerializer.Serialize(item, systemTextJsonOptions);

            // Assert
            Assert.AreEqual(newtonsoftJson, systemTextJson);
        }

        public class GlobalNumberConverter : Newtonsoft.Json.JsonConverter
        {
            public override bool CanConvert(Type objectType)
            {
                return objectType == typeof(int) || objectType == typeof(int?) ||
                       objectType == typeof(long) || objectType == typeof(long?) ||
                       objectType == typeof(decimal) || objectType == typeof(decimal?) ||
                       objectType == typeof(double) || objectType == typeof(double?) ||
                       objectType == typeof(float) || objectType == typeof(float?);
            }

            public override void WriteJson(JsonWriter writer, object value, Newtonsoft.Json.JsonSerializer serializer)
            {
                if (value == null)
                {
                    writer.WriteNull();
                    return;
                }

                if (value is int || value is long)
                {
                    writer.WriteValue(value);
                }
                else if (value is decimal decimalValue)
                {
                    if (decimalValue == Math.Floor(decimalValue))
                    {
                        writer.WriteValue((long)decimalValue);
                    }
                    else
                    {
                        writer.WriteValue(decimalValue);
                    }
                }
                else if (value is double doubleValue)
                {
                    if (doubleValue == Math.Floor(doubleValue))
                    {
                        writer.WriteValue((long)doubleValue);
                    }
                    else
                    {
                        writer.WriteValue(doubleValue);
                    }
                }
                else if (value is float floatValue)
                {
                    if (floatValue == Math.Floor(floatValue))
                    {
                        writer.WriteValue((long)floatValue);
                    }
                    else
                    {
                        writer.WriteValue(floatValue);
                    }
                }
                else
                {
                    writer.WriteValue(value);
                }
            }

            public override object ReadJson(JsonReader reader, Type objectType, object existingValue, Newtonsoft.Json.JsonSerializer serializer)
            {
                if (reader.Value == null) return null;
                
                if (objectType == typeof(int) || objectType == typeof(int?))
                    return Convert.ToInt32(reader.Value);
                if (objectType == typeof(long) || objectType == typeof(long?))
                    return Convert.ToInt64(reader.Value);
                if (objectType == typeof(decimal) || objectType == typeof(decimal?))
                    return Convert.ToDecimal(reader.Value);
                if (objectType == typeof(double) || objectType == typeof(double?))
                    return Convert.ToDouble(reader.Value);
                if (objectType == typeof(float) || objectType == typeof(float?))
                    return Convert.ToSingle(reader.Value);
                    
                return reader.Value;
            }
        }

        public class NewtonsoftDateTimeConverter : Newtonsoft.Json.JsonConverter<DateTime>
        {
            public override void WriteJson(JsonWriter writer, DateTime value, Newtonsoft.Json.JsonSerializer serializer)
            {
                writer.WriteValue(value.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ssZ", CultureInfo.InvariantCulture));
            }

            public override DateTime ReadJson(JsonReader reader, Type objectType, DateTime existingValue, bool hasExistingValue, Newtonsoft.Json.JsonSerializer serializer)
            {
                if (reader.Value == null) return default;
                return DateTime.Parse(reader.Value.ToString(), CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal);
            }
        }

        private static class SystemTextJsonConverters
        {
            public class DateTimeConverter : System.Text.Json.Serialization.JsonConverter<DateTime>
            {
                public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
                {
                    string dateStr = reader.GetString();
                    return DateTime.Parse(dateStr, CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal);
                }

                public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
                {
                    writer.WriteStringValue(value.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ssZ", CultureInfo.InvariantCulture));
                }
            }

            public class DecimalConverter : System.Text.Json.Serialization.JsonConverter<decimal>
            {
                public override decimal Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
                {
                    if (reader.TokenType == JsonTokenType.String)
                    {
                        return decimal.Parse(reader.GetString(), CultureInfo.InvariantCulture);
                    }
                    return reader.GetDecimal();
                }

                public override void Write(Utf8JsonWriter writer, decimal value, JsonSerializerOptions options)
                {
                    if (value == Math.Floor(value))
                    {
                        writer.WriteNumberValue((long)value);
                    }
                    else
                    {
                        writer.WriteNumberValue(value);
                    }
                }
            }
        }
#endif

        [Test]
        public void NullableCaption_SerializesCorrectly()
        {
            var item = new SignatureItem
            {
                ftSignatureFormat = 0,
                ftSignatureType = 0,
                Caption = null,
                Data = "TestData"
            };

            var json = JsonConvert.SerializeObject(item);
            var deserialized = JsonConvert.DeserializeObject<SignatureItem>(json);

            Assert.IsNull(deserialized.Caption);
            Assert.AreEqual(item.Data, deserialized.Data);
        }

        [Test]
        public void NullableProperties_SerializeCorrectly()
        {
            var item = new SignatureItem
            {
                ftSignatureFormat = SignatureFormat.QRCode,
                ftSignatureType = SignatureType.Unknown,
                Data = "TestData"
            };

            var json = JsonConvert.SerializeObject(item);
            var deserialized = JsonConvert.DeserializeObject<SignatureItem>(json);

            Assert.IsNull(deserialized.ftSignatureItemId);
            Assert.IsNull(deserialized.Caption);
            Assert.AreEqual(item.Data, deserialized.Data);
        }

        private void AssertSignatureItemsEqual(SignatureItem expected, SignatureItem actual)
        {
            Assert.AreEqual(expected.ftSignatureItemId, actual.ftSignatureItemId);
            Assert.AreEqual(expected.ftSignatureFormat, actual.ftSignatureFormat);
            Assert.AreEqual(expected.ftSignatureType, actual.ftSignatureType);
            Assert.AreEqual(expected.Caption, actual.Caption);
            Assert.AreEqual(expected.Data, actual.Data);
        }
    }
}