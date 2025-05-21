using System;
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
    public class ChargeItemSerializationTests
    {
        private ChargeItem CreateTestChargeItem()
        {
            return new ChargeItem
            {
                ftChargeItemId = Guid.Parse("12345678-1234-1234-1234-123456789012"),
                Quantity = 2.5m,
                Description = "Test Product",
                Amount = 99.99m,
                VATRate = 19.0m,
                ftChargeItemCase = ChargeItemCase.NormalVatRate,
                ftChargeItemCaseData = new { CustomField = "CustomValue" },
                VATAmount = 19.00m,
                Moment = new DateTime(2024, 1, 15, 10, 30, 0, DateTimeKind.Utc),
                Position = 1,
                AccountNumber = "4100",
                CostCenter = "CC100",
                ProductGroup = "FOOD",
                ProductNumber = "PROD-001",
                ProductBarcode = "1234567890123",
                Unit = "pcs",
                UnitQuantity = 1.0m,
                UnitPrice = 39.99m,
                Currency = Currency.EUR,
                DecimalPrecisionMultiplier = 100
            };
        }

        [Test]
        public void Newtonsoft_SerializeDeserialize_PreservesAllProperties()
        {
            var original = CreateTestChargeItem();
            var json = JsonConvert.SerializeObject(original, Formatting.Indented);
            var deserialized = JsonConvert.DeserializeObject<ChargeItem>(json);
            AssertChargeItemsEqual(original, deserialized);
        }

#if NETSTANDARD2_1_TESTS
        [Test]
        public void SystemTextJson_SerializeDeserialize_PreservesAllProperties()
        {
            var original = CreateTestChargeItem();
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                PropertyNamingPolicy = null,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                Converters = { new JsonStringEnumConverter() }
            };
            var json = System.Text.Json.JsonSerializer.Serialize(original, options);
            var deserialized = System.Text.Json.JsonSerializer.Deserialize<ChargeItem>(json, options);
            AssertChargeItemsEqual(original, deserialized);
        }

        [Test]
        public void BothSerializers_ProduceSameOutput()
        {
            // Arrange
            var item = CreateTestChargeItem();

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
            
            var fromNewtonsoft = JsonConvert.DeserializeObject<ChargeItem>(newtonsoftJson);
            var fromSystemText = JsonConvert.DeserializeObject<ChargeItem>(systemTextJson);
            
            AssertChargeItemsEqual(fromNewtonsoft, fromSystemText);

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
                        newtonsoftValue.Type == JTokenType.Float ||
                        prop.Name == "Amount" || prop.Name == "VATAmount" || 
                        prop.Name == "VATRate" || prop.Name.EndsWith("Price") || 
                        prop.Name.EndsWith("Quantity") || prop.Name == "Position")
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
                        prop.Name == "Moment" || prop.Name.EndsWith("Time") ||
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

                    if (prop.Name.EndsWith("Case") || prop.Name == "Currency")
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
        public void DecimalPrecisionMultiplier_SerializesAsZeroWhenOne()
        {
            var item = new ChargeItem
            {
                Description = "Test",
                Amount = 0,
                VATRate = 0,
                DecimalPrecisionMultiplier = 1
            };

            var json = JsonConvert.SerializeObject(item);
            Assert.IsTrue(json.Contains("\"DecimalPrecisionMultiplier\":0"));
        }

        [Test]
        public void DecimalPrecisionMultiplier_SerializesAsActualValueWhenNotOne()
        {
            var item = new ChargeItem
            {
                Description = "Test",
                Amount = 0,
                VATRate = 0,
                DecimalPrecisionMultiplier = 100
            };

            var json = JsonConvert.SerializeObject(item);
            Assert.IsTrue(json.Contains("\"DecimalPrecisionMultiplier\":100"));
        }

        [Test]
        public void NullableProperties_SerializeCorrectly()
        {
            var item = new ChargeItem
            {
                Description = "Test",
                Amount = 0,
                VATRate = 0,
            };

            var json = JsonConvert.SerializeObject(item);
            var deserialized = JsonConvert.DeserializeObject<ChargeItem>(json);

            Assert.IsNull(deserialized.ftChargeItemId);
            Assert.IsNull(deserialized.VATAmount);
            Assert.IsNull(deserialized.AccountNumber);
            Assert.IsNull(deserialized.CostCenter);
            Assert.IsNull(deserialized.ProductGroup);
            Assert.IsNull(deserialized.ProductNumber);
            Assert.IsNull(deserialized.ProductBarcode);
            Assert.IsNull(deserialized.Unit);
            Assert.IsNull(deserialized.UnitQuantity);
            Assert.IsNull(deserialized.UnitPrice);
        }

        private void AssertChargeItemsEqual(ChargeItem expected, ChargeItem actual)
        {
            Assert.AreEqual(expected.ftChargeItemId, actual.ftChargeItemId);
            Assert.AreEqual(expected.Quantity, actual.Quantity);
            Assert.AreEqual(expected.Description, actual.Description);
            Assert.AreEqual(expected.Amount, actual.Amount);
            Assert.AreEqual(expected.VATRate, actual.VATRate);
            Assert.AreEqual(expected.ftChargeItemCase, actual.ftChargeItemCase);
            Assert.AreEqual(expected.VATAmount, actual.VATAmount);
            Assert.AreEqual(expected.Moment, actual.Moment);
            Assert.AreEqual(expected.Position, actual.Position);
            Assert.AreEqual(expected.AccountNumber, actual.AccountNumber);
            Assert.AreEqual(expected.CostCenter, actual.CostCenter);
            Assert.AreEqual(expected.ProductGroup, actual.ProductGroup);
            Assert.AreEqual(expected.ProductNumber, actual.ProductNumber);
            Assert.AreEqual(expected.ProductBarcode, actual.ProductBarcode);
            Assert.AreEqual(expected.Unit, actual.Unit);
            Assert.AreEqual(expected.UnitQuantity, actual.UnitQuantity);
            Assert.AreEqual(expected.UnitPrice, actual.UnitPrice);
            Assert.AreEqual(expected.Currency, actual.Currency);
            Assert.AreEqual(expected.DecimalPrecisionMultiplier, actual.DecimalPrecisionMultiplier);
        }
    }
}