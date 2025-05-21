using System;
using System.Collections.Generic;
using System.Globalization;
using fiskaltrust.Middleware.ifPOS.v2.Models;
using NUnit.Framework;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

#if NETSTANDARD2_1_TESTS
using System.Text.Json;
using System.Text.Json.Serialization;
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
            // Arrange
            var original = CreateTestOperationItem();

            // Act
            var json = JsonConvert.SerializeObject(original, Formatting.Indented);
            var deserialized = JsonConvert.DeserializeObject<OperationItem>(json);

            // Assert
            AssertOperationItemsEqual(original, deserialized);
        }

#if NETSTANDARD2_1_TESTS
        [Test]
        public void SystemTextJson_SerializeDeserialize_PreservesAllProperties()
        {
            // Arrange
            var original = CreateTestOperationItem();
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                PropertyNamingPolicy = null,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                Converters = { new JsonStringEnumConverter() }
            };

            // Act
            var json = System.Text.Json.JsonSerializer.Serialize(original, options);
            var deserialized = System.Text.Json.JsonSerializer.Deserialize<OperationItem>(json, options);

            // Assert
            AssertOperationItemsEqual(original, deserialized);
        }

        [Test]
        public void BothSerializers_ProduceSameOutput()
        {
            // Arrange
            var item = CreateTestOperationItem();

            var newtonsoftSettings = new JsonSerializerSettings
            {
                Formatting = Formatting.None,
                DateFormatHandling = DateFormatHandling.IsoDateFormat,
                DateTimeZoneHandling = DateTimeZoneHandling.Utc,
                NullValueHandling = NullValueHandling.Ignore,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                PreserveReferencesHandling = PreserveReferencesHandling.None,
                Converters = { 
                    new Newtonsoft.Json.Converters.StringEnumConverter(namingStrategy: null)
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
                    new SystemTextJsonConverters.DateTimeOffsetConverter()
                }
            };

            // Act
            var newtonsoftJson = JsonConvert.SerializeObject(item, newtonsoftSettings);
            var systemTextJson = System.Text.Json.JsonSerializer.Serialize(item, systemTextJsonOptions);
            
            var fromNewtonsoft = JsonConvert.DeserializeObject<OperationItem>(newtonsoftJson);
            var fromSystemText = JsonConvert.DeserializeObject<OperationItem>(systemTextJson);
            
            AssertOperationItemsEqual(fromNewtonsoft, fromSystemText);

            var newtonsoftDoc = JObject.Parse(newtonsoftJson);
            var systemTextDoc = JsonDocument.Parse(systemTextJson);

            foreach (var prop in newtonsoftDoc.Properties())
            {
                Assert.IsTrue(systemTextDoc.RootElement.TryGetProperty(prop.Name, out _),
                    $"Property '{prop.Name}' not found in System.Text.Json output");
            }

            foreach (var prop in newtonsoftDoc.Properties())
            {
                if (systemTextDoc.RootElement.TryGetProperty(prop.Name, out var systemTextValue))
                {
                    var newtonsoftValue = newtonsoftDoc[prop.Name];
                    
                    if (newtonsoftValue.Type == JTokenType.Object || 
                        newtonsoftValue.Type == JTokenType.Array)
                    {
                        Assert.IsTrue(
                            systemTextValue.ValueKind == JsonValueKind.Object || 
                            systemTextValue.ValueKind == JsonValueKind.Array,
                            $"Property '{prop.Name}' should be an object or array in both serializations");
                        continue;
                    }

                    if (newtonsoftValue.Type == JTokenType.Integer || 
                        newtonsoftValue.Type == JTokenType.Float)
                    {
                        if (systemTextValue.ValueKind == JsonValueKind.Number)
                        {
                            decimal newtonVal = newtonsoftValue.Value<decimal>();
                            decimal sysVal = systemTextValue.GetDecimal();
                            Assert.AreEqual(newtonVal, sysVal, 
                                $"Property '{prop.Name}' has different numeric values");
                        }
                        else if (systemTextValue.ValueKind == JsonValueKind.String)
                        {
                            if (decimal.TryParse(newtonsoftValue.ToString(), out var newtonVal) && 
                                decimal.TryParse(systemTextValue.GetString(), out var sysVal))
                            {
                                Assert.AreEqual(newtonVal, sysVal, 
                                    $"Property '{prop.Name}' has different numeric values");
                            }
                        }
                        continue;
                    }

                    if (newtonsoftValue.Type == JTokenType.Date || 
                        (newtonsoftValue.Type == JTokenType.String && 
                         DateTimeOffset.TryParse(newtonsoftValue.ToString(), out _)))
                    {
                        if (systemTextValue.ValueKind == JsonValueKind.String)
                        {
                            if (DateTimeOffset.TryParse(newtonsoftValue.ToString(), out var date1) && 
                                DateTimeOffset.TryParse(systemTextValue.GetString(), out var date2))
                            {
                                var date1Utc = date1.ToUniversalTime();
                                var date2Utc = date2.ToUniversalTime();
                                    
                                Assert.AreEqual(date1Utc, date2Utc, 
                                    $"Property '{prop.Name}' has different date/time values");
                            }
                        }
                        continue;
                    }

                    if (newtonsoftValue.Type == JTokenType.Boolean)
                    {
                        bool newtonVal = newtonsoftValue.Value<bool>();
                        bool sysVal = systemTextValue.GetBoolean();
                        Assert.AreEqual(newtonVal, sysVal, 
                            $"Property '{prop.Name}' has different boolean values");
                        continue;
                    }

                    if (newtonsoftValue.Type == JTokenType.Null)
                    {
                        Assert.AreEqual(JsonValueKind.Null, systemTextValue.ValueKind,
                            $"Property '{prop.Name}' is null in Newtonsoft but not in System.Text.Json");
                        continue;
                    }

                    Assert.AreEqual(
                        newtonsoftValue.ToString(), 
                        GetJsonElementValueAsString(systemTextValue),
                        $"Property '{prop.Name}' has different values in the two serializations");
                }
            }
        }

        private static class SystemTextJsonConverters
        {
            public class DateTimeConverter : System.Text.Json.Serialization.JsonConverter<DateTime>
            {
                public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
                {
                    string dateStr = reader.GetString();
                    DateTime date = DateTime.Parse(dateStr, CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal);
                    return date;
                }

                public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
                {
                    writer.WriteStringValue(value.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ss.fffZ"));
                }
            }

            public class DateTimeOffsetConverter : System.Text.Json.Serialization.JsonConverter<DateTimeOffset>
            {
                public override DateTimeOffset Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
                {
                    string dateStr = reader.GetString();
                    DateTimeOffset date = DateTimeOffset.Parse(dateStr, CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal);
                    return date;
                }

                public override void Write(Utf8JsonWriter writer, DateTimeOffset value, JsonSerializerOptions options)
                {
                    writer.WriteStringValue(value.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ss.fffZ"));
                }
            }
        }

        private string GetJsonElementValueAsString(JsonElement element)
        {
            return element.ValueKind switch
            {
                JsonValueKind.String => element.GetString(),
                JsonValueKind.Number => element.GetRawText(),
                JsonValueKind.True => "true",
                JsonValueKind.False => "false",
                JsonValueKind.Null => "null",
                _ => element.ToString(),
            };
        }
#endif

        [Test]
        public void Clone_CreatesIdenticalCopy()
        {
            // Arrange
            var original = CreateTestOperationItem();

            // Act
            var cloned = (OperationItem)original.Clone();

            // Assert
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
            // Arrange
            var item = new OperationItem();

            // Act
            var json = JsonConvert.SerializeObject(item);
            var deserialized = JsonConvert.DeserializeObject<OperationItem>(json);

            // Assert
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