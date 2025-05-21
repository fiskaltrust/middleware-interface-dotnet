using System;
using System.Collections.Generic;
using System.Globalization;
using fiskaltrust.Middleware.ifPOS.v2.Models;
using fiskaltrust.Middleware.Localization.v2.Models.ifPOS.v2.Cases;
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
    public class ReceiptRequestSerializationTests
    {
        private ReceiptRequest CreateTestReceiptRequest()
        {
            return new ReceiptRequest
            {
                cbTerminalID = "TERM001",
                cbReceiptReference = "RECEIPT-2024-001",
                cbReceiptMoment = new DateTime(2024, 1, 15, 10, 30, 0, DateTimeKind.Utc),
                cbChargeItems = new List<ChargeItem>
                {
                    new ChargeItem
                    {
                        Description = "Product 1",
                        Amount = 10.99m,
                        VATRate = 19.0m,
                        Quantity = 1m
                    }
                },
                cbPayItems = new List<PayItem>
                {
                    new PayItem
                    {
                        Description = "Cash",
                        Amount = 10.99m,
                        ftPayItemCase = 0
                    }
                },
                ftCashBoxID = Guid.Parse("11111111-2222-3333-4444-555555555555"),
                ftPosSystemId = Guid.Parse("66666666-7777-8888-9999-000000000000"),
                ftReceiptCase = ReceiptCase.OneReceipt0x2001,
                ftReceiptCaseData = new { CustomData = "TestValue" },
                ftQueueID = Guid.Parse("aaaaaaaa-bbbb-cccc-dddd-eeeeeeeeeeee"),
                cbPreviousReceiptReference = "RECEIPT-2024-000",
                cbReceiptAmount = 10.99m,
                cbUser = "User123",
                cbArea = "Area1",
                cbCustomer = "Customer001",
                cbSettlement = "Settlement1",
                Currency = Currency.EUR,
                DecimalPrecisionMultiplier = 100
            };
        }

        [Test]
        public void Newtonsoft_SerializeDeserialize_PreservesAllProperties()
        {
            // Arrange
            var original = CreateTestReceiptRequest();

            // Act
            var json = JsonConvert.SerializeObject(original, Formatting.Indented);
            var deserialized = JsonConvert.DeserializeObject<ReceiptRequest>(json);

            // Assert
            AssertReceiptRequestsEqual(original, deserialized);
        }

#if NETSTANDARD2_1_TESTS
        [Test]
        public void SystemTextJson_SerializeDeserialize_PreservesAllProperties()
        {
            // Arrange
            var original = CreateTestReceiptRequest();
            var options = new JsonSerializerOptions 
            { 
                WriteIndented = true,
                PropertyNamingPolicy = null,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                Converters = { new JsonStringEnumConverter() }
            };

            // Act
            var json = System.Text.Json.JsonSerializer.Serialize(original, options);
            var deserialized = System.Text.Json.JsonSerializer.Deserialize<ReceiptRequest>(json, options);

            // Assert
            AssertReceiptRequestsEqual(original, deserialized);
        }

        [Test]
        public void BothSerializers_ProduceSameOutput()
        {
            // Arrange
            var item = CreateTestReceiptRequest();

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
                    new SystemTextJsonConverters.DecimalConverter()
                }
            };

            // Act
            var newtonsoftJson = JsonConvert.SerializeObject(item, newtonsoftSettings);
            var systemTextJson = System.Text.Json.JsonSerializer.Serialize(item, systemTextJsonOptions);
            
            var fromNewtonsoft = JsonConvert.DeserializeObject<ReceiptRequest>(newtonsoftJson);
            var fromSystemText = JsonConvert.DeserializeObject<ReceiptRequest>(systemTextJson);
            
            AssertReceiptRequestsEqual(fromNewtonsoft, fromSystemText);

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
                         DateTime.TryParse(newtonsoftValue.ToString(), out _)))
                    {
                        if (systemTextValue.ValueKind == JsonValueKind.String)
                        {
                            if (DateTime.TryParse(newtonsoftValue.ToString(), out var date1) && 
                                DateTime.TryParse(systemTextValue.GetString(), out var date2))
                            {
                                var date1Utc = date1.Kind == DateTimeKind.Unspecified ? 
                                    DateTime.SpecifyKind(date1, DateTimeKind.Utc) : date1.ToUniversalTime();
                                var date2Utc = date2.Kind == DateTimeKind.Unspecified ? 
                                    DateTime.SpecifyKind(date2, DateTimeKind.Utc) : date2.ToUniversalTime();
                                    
                                Assert.AreEqual(date1Utc, date2Utc, 
                                    $"Property '{prop.Name}' has different date/time values");
                            }
                        }
                        continue;
                    }

                    if (prop.Name == "Currency" || prop.Name.EndsWith("Case"))
                    {
                        string newtonStr = newtonsoftValue.ToString().ToLowerInvariant();
                        string sysStr = GetJsonElementValueAsString(systemTextValue).ToLowerInvariant();
                        Assert.AreEqual(newtonStr, sysStr,
                            $"Property '{prop.Name}' has different enum values");
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
                    writer.WriteNumberValue(value);
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
        public void DecimalPrecisionMultiplier_SerializesCorrectly()
        {
            // Arrange
            var request = new ReceiptRequest
            {
                cbReceiptReference = "TEST",
                cbReceiptMoment = DateTime.UtcNow,
                cbChargeItems = new List<ChargeItem>(),
                cbPayItems = new List<PayItem>(),
                DecimalPrecisionMultiplier = 1
            };

            // Act
            var json = JsonConvert.SerializeObject(request);

            // Assert
            Assert.IsTrue(json.Contains("\"DecimalPrecisionMultiplier\":0"));
        }

        private void AssertReceiptRequestsEqual(ReceiptRequest expected, ReceiptRequest actual)
        {
            Assert.AreEqual(expected.cbTerminalID, actual.cbTerminalID);
            Assert.AreEqual(expected.cbReceiptReference, actual.cbReceiptReference);
            Assert.AreEqual(expected.cbReceiptMoment, actual.cbReceiptMoment);
            Assert.AreEqual(expected.cbChargeItems.Count, actual.cbChargeItems.Count);
            Assert.AreEqual(expected.cbPayItems.Count, actual.cbPayItems.Count);
            Assert.AreEqual(expected.ftCashBoxID, actual.ftCashBoxID);
            Assert.AreEqual(expected.ftPosSystemId, actual.ftPosSystemId);
            Assert.AreEqual(expected.ftReceiptCase, actual.ftReceiptCase);
            Assert.AreEqual(expected.ftQueueID, actual.ftQueueID);
            Assert.AreEqual(expected.cbPreviousReceiptReference, actual.cbPreviousReceiptReference);
            Assert.AreEqual(expected.cbReceiptAmount, actual.cbReceiptAmount);
            Assert.AreEqual(expected.cbUser?.ToString(), actual.cbUser?.ToString());
            Assert.AreEqual(expected.cbArea?.ToString(), actual.cbArea?.ToString());
            Assert.AreEqual(expected.cbCustomer?.ToString(), actual.cbCustomer?.ToString());
            Assert.AreEqual(expected.cbSettlement?.ToString(), actual.cbSettlement?.ToString());
            Assert.AreEqual(expected.Currency, actual.Currency);
            Assert.AreEqual(expected.DecimalPrecisionMultiplier, actual.DecimalPrecisionMultiplier);
        }
    }
}