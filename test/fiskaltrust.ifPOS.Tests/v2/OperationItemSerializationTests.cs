using System;
using System.Collections.Generic;
using System.Globalization;
using fiskaltrust.Middleware.ifPOS.v2.Models;
using NUnit.Framework;
using Newtonsoft.Json;

#if NETSTANDARD2_1_TESTS
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Encodings.Web;
#endif

namespace fiskaltrust.Middleware.Interface.Tests.v2
{
    [TestFixture]
    public class OperationItemSerializationTests
    {
        private OperationItem CreateTestOperationItem()
        {
            return new OperationItem
            {
                cbOperationItemID = Guid.Parse("12345678-1234-1234-1234-123456789012"),
                ftQueueID = Guid.Parse("aaaaaaaa-bbbb-cccc-dddd-eeeeeeeeeeee"),
                ftPosSystemID = Guid.Parse("11111111-2222-3333-4444-555555555555"),
                cbTerminalID = "TERM001",
                Method = "POST",
                Path = "/api/receipt",
                RequestHeaders = new Dictionary<string, string[]>
                {
                    { "Content-Type", new[] { "application/json" } },
                    { "Accept", new[] { "application/json" } }
                },
                Request = "{\"test\":\"data\"}",
                Response = "{\"status\":\"success\"}",
                ResponseCode = 200,
                LastState = "Completed",
                TimeStamp = new DateTimeOffset(2024, 1, 15, 10, 30, 0, TimeSpan.Zero),
                ftOperationItemMoment = new DateTimeOffset(2024, 1, 15, 10, 30, 0, TimeSpan.Zero)
            };
        }

        [Test]
        public void Newtonsoft_SerializeDeserialize_PreservesAllProperties()
        {
            var original = CreateTestOperationItem();
            var json = JsonConvert.SerializeObject(original, Formatting.Indented);
            var deserialized = JsonConvert.DeserializeObject<OperationItem>(json);
            AssertOperationItemsEqual(original, deserialized);
        }

#if NETSTANDARD2_1_TESTS
        [Test]
        public void SystemTextJson_SerializeDeserialize_PreservesAllProperties()
        {
            var original = CreateTestOperationItem();
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                PropertyNamingPolicy = null,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                Converters = { new JsonStringEnumConverter() }
            };
            var json = System.Text.Json.JsonSerializer.Serialize(original, options);
            var deserialized = System.Text.Json.JsonSerializer.Deserialize<OperationItem>(json, options);
            AssertOperationItemsEqual(original, deserialized);
        }

        [Test]
        public void BothSerializers_ProduceSameOutput()
        {
            // Arrange
            var item = CreateTestOperationItem();
            
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
                    new NewtonsoftDateTimeOffsetConverter(),
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
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping, // to allow special characters
                Converters = {
                    new JsonStringEnumConverter(namingPolicy: null), 
                    new SystemTextJsonConverters.DateTimeConverter(), 
                    new SystemTextJsonConverters.DateTimeOffsetConverter(),
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

        public class NewtonsoftDateTimeOffsetConverter : Newtonsoft.Json.JsonConverter<DateTimeOffset>
        {
            public override void WriteJson(JsonWriter writer, DateTimeOffset value, Newtonsoft.Json.JsonSerializer serializer)
            {
                writer.WriteValue(value.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ssZ", CultureInfo.InvariantCulture));
            }

            public override DateTimeOffset ReadJson(JsonReader reader, Type objectType, DateTimeOffset existingValue, bool hasExistingValue, Newtonsoft.Json.JsonSerializer serializer)
            {
                if (reader.Value == null) return default;
                return DateTimeOffset.Parse(reader.Value.ToString(), CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal);
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

            public class DateTimeOffsetConverter : System.Text.Json.Serialization.JsonConverter<DateTimeOffset>
            {
                public override DateTimeOffset Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
                {
                    string dateStr = reader.GetString();
                    return DateTimeOffset.Parse(dateStr, CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal);
                }

                public override void Write(Utf8JsonWriter writer, DateTimeOffset value, JsonSerializerOptions options)
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
        public void Clone_CreatesIdenticalCopy()
        {
            var original = CreateTestOperationItem();
            var cloned = (OperationItem)original.Clone();

            Assert.AreEqual(original.cbOperationItemID, cloned.cbOperationItemID);
            Assert.AreEqual(original.ftQueueID, cloned.ftQueueID);
            Assert.AreEqual(original.Method, cloned.Method);
            Assert.AreEqual(original.Path, cloned.Path);
            Assert.AreEqual(original.Request, cloned.Request);
            Assert.AreEqual(original.Response, cloned.Response);
        }

        [Test]
        public void DefaultValues_SerializeCorrectly()
        {
            var item = new OperationItem();
            var json = JsonConvert.SerializeObject(item);
            var deserialized = JsonConvert.DeserializeObject<OperationItem>(json);

            Assert.AreEqual(string.Empty, deserialized.Method);
            Assert.AreEqual(string.Empty, deserialized.Path);
            Assert.AreEqual(string.Empty, deserialized.Request);
            Assert.AreEqual(string.Empty, deserialized.LastState);
            Assert.IsNotNull(deserialized.RequestHeaders);
            Assert.AreEqual(0, deserialized.RequestHeaders.Count);
        }

        private void AssertOperationItemsEqual(OperationItem expected, OperationItem actual)
        {
            Assert.AreEqual(expected.cbOperationItemID, actual.cbOperationItemID);
            Assert.AreEqual(expected.ftQueueID, actual.ftQueueID);
            Assert.AreEqual(expected.ftPosSystemID, actual.ftPosSystemID);
            Assert.AreEqual(expected.cbTerminalID, actual.cbTerminalID);
            Assert.AreEqual(expected.Method, actual.Method);
            Assert.AreEqual(expected.Path, actual.Path);
            Assert.AreEqual(expected.RequestHeaders.Count, actual.RequestHeaders.Count);
            Assert.AreEqual(expected.Request, actual.Request);
            Assert.AreEqual(expected.Response, actual.Response);
            Assert.AreEqual(expected.ResponseCode, actual.ResponseCode);
            Assert.AreEqual(expected.LastState, actual.LastState);
            Assert.AreEqual(expected.TimeStamp, actual.TimeStamp);
            Assert.AreEqual(expected.ftOperationItemMoment, actual.ftOperationItemMoment);
        }
    }
}