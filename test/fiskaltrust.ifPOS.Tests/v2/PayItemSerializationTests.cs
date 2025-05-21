using System;
using fiskaltrust.Middleware.ifPOS.v2.Models;
using fiskaltrust.Middleware.Localization.v2.Models.ifPOS.v2.Cases;
using NUnit.Framework;
using Newtonsoft.Json;
using System.Reflection;
using Newtonsoft.Json.Linq;

#if NETSTANDARD2_1_TESTS
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Globalization;
#endif

namespace fiskaltrust.Middleware.Interface.Tests.v2
{
    [TestFixture]
    public class PayItemSerializationTests
    {
        private PayItem CreateTestPayItem()
        {
            return new PayItem
            {
                ftPayItemId = Guid.Parse("12345678-1234-1234-1234-123456789012"),
                Quantity = 1.5m,
                Description = "Cash Payment",
                Amount = 50.00m,
                ftPayItemCase = PayItemCase.CashPayment,
                ftPayItemCaseData = new { TransactionID = "TX123" },
                Moment = new DateTime(2024, 1, 15, 10, 30, 0, DateTimeKind.Utc),
                Position = 1,
                AccountNumber = "1000",
                CostCenter = "CC001",
                MoneyGroup = "CASH",
                MoneyNumber = "EUR_CASH",
                MoneyBarcode = "9876543210",
                Currency = Currency.EUR,
                DecimalPrecisionMultiplier = 100
            };
        }

        [Test]
        public void Newtonsoft_SerializeDeserialize_PreservesAllProperties()
        {
            // Arrange
            var original = CreateTestPayItem();

            // Act
            var json = JsonConvert.SerializeObject(original, Formatting.Indented);
            var deserialized = JsonConvert.DeserializeObject<PayItem>(json);

            // Assert
            AssertPayItemsEqual(original, deserialized);
        }

#if NETSTANDARD2_1_TESTS
        [Test]
        public void SystemTextJson_SerializeDeserialize_PreservesAllProperties()
        {
            // Arrange
            var original = CreateTestPayItem();
            var options = new JsonSerializerOptions 
            { 
                WriteIndented = true,
                PropertyNamingPolicy = null,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                Converters = { new JsonStringEnumConverter() }
            };

            // Act
            var json = System.Text.Json.JsonSerializer.Serialize(original, options);
            var deserialized = System.Text.Json.JsonSerializer.Deserialize<PayItem>(json, options);

            // Assert
            AssertPayItemsEqual(original, deserialized);
        }

        [Test]
        public void BothSerializers_ProduceSameOutput()
        {
            // Arrange
            var item = CreateTestPayItem();

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
            
            var fromNewtonsoft = JsonConvert.DeserializeObject<PayItem>(newtonsoftJson);
            var fromSystemText = JsonConvert.DeserializeObject<PayItem>(systemTextJson);
            
            AssertPayItemsEqual(fromNewtonsoft, fromSystemText);

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
                        prop.Name == "Amount" || prop.Name.EndsWith("Price") || 
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
        public void QuantitySerialization_HandlesDefaultValue()
        {
            // Arrange
            var item = new PayItem
            {
                Description = "Test",
                Amount = 10m,
                Quantity = 1 
            };

            // Act
            var json = JsonConvert.SerializeObject(item);
            var deserialized = JsonConvert.DeserializeObject<PayItem>(json);

            // Assert
            Assert.IsFalse(json.Contains("\"Quantity\":1"));
            
            var fieldInfo = typeof(PayItem).GetField("_quantity", BindingFlags.NonPublic | BindingFlags.Instance);
            var actualValue = (decimal)fieldInfo.GetValue(deserialized);
            Assert.AreEqual(1m, actualValue);
        }

        [Test]
        public void QuantitySerialization_HandlesNonDefaultValue()
        {
            // Arrange
            var item = new PayItem
            {
                Description = "Test",
                Amount = 10m,
                Quantity = 2.5m
            };

            // Act
            var json = JsonConvert.SerializeObject(item);
            var deserialized = JsonConvert.DeserializeObject<PayItem>(json);

            // Assert
            Assert.IsTrue(json.Contains("\"Quantity\":2.5"));
            Assert.AreEqual(2.5m, deserialized.Quantity);
        }

        private void AssertPayItemsEqual(PayItem expected, PayItem actual)
        {
            Assert.AreEqual(expected.ftPayItemId, actual.ftPayItemId);
            Assert.AreEqual(expected.Quantity, actual.Quantity);
            Assert.AreEqual(expected.Description, actual.Description);
            Assert.AreEqual(expected.Amount, actual.Amount);
            Assert.AreEqual(expected.ftPayItemCase, actual.ftPayItemCase);
            Assert.AreEqual(expected.Moment, actual.Moment);
            Assert.AreEqual(expected.Position, actual.Position);
            Assert.AreEqual(expected.AccountNumber, actual.AccountNumber);
            Assert.AreEqual(expected.CostCenter, actual.CostCenter);
            Assert.AreEqual(expected.MoneyGroup, actual.MoneyGroup);
            Assert.AreEqual(expected.MoneyNumber, actual.MoneyNumber);
            Assert.AreEqual(expected.MoneyBarcode, actual.MoneyBarcode);
            Assert.AreEqual(expected.Currency, actual.Currency);
            Assert.AreEqual(expected.DecimalPrecisionMultiplier, actual.DecimalPrecisionMultiplier);
        }
    }
}