using System;
using fiskaltrust.Middleware.ifPOS.v2.Models;
using fiskaltrust.Middleware.Localization.v2.Models.ifPOS.v2.Cases;
using NUnit.Framework;
using Newtonsoft.Json;

#if NETSTANDARD2_1_TESTS
using System.Text.Json;
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
            // Arrange
            var original = CreateTestChargeItem();

            // Act
            var json = JsonConvert.SerializeObject(original, Formatting.Indented);
            var deserialized = JsonConvert.DeserializeObject<ChargeItem>(json);

            // Assert
            Assert.AreEqual(original.ftChargeItemId, deserialized.ftChargeItemId);
            Assert.AreEqual(original.Quantity, deserialized.Quantity);
            Assert.AreEqual(original.Description, deserialized.Description);
            Assert.AreEqual(original.Amount, deserialized.Amount);
            Assert.AreEqual(original.VATRate, deserialized.VATRate);
            Assert.AreEqual(original.ftChargeItemCase, deserialized.ftChargeItemCase);
            Assert.AreEqual(original.VATAmount, deserialized.VATAmount);
            Assert.AreEqual(original.Moment, deserialized.Moment);
            Assert.AreEqual(original.Position, deserialized.Position);
            Assert.AreEqual(original.AccountNumber, deserialized.AccountNumber);
            Assert.AreEqual(original.CostCenter, deserialized.CostCenter);
            Assert.AreEqual(original.ProductGroup, deserialized.ProductGroup);
            Assert.AreEqual(original.ProductNumber, deserialized.ProductNumber);
            Assert.AreEqual(original.ProductBarcode, deserialized.ProductBarcode);
            Assert.AreEqual(original.Unit, deserialized.Unit);
            Assert.AreEqual(original.UnitQuantity, deserialized.UnitQuantity);
            Assert.AreEqual(original.UnitPrice, deserialized.UnitPrice);
            Assert.AreEqual(original.Currency, deserialized.Currency);
            Assert.AreEqual(original.DecimalPrecisionMultiplier, deserialized.DecimalPrecisionMultiplier);
        }

#if NETSTANDARD2_1_TESTS
        [Test]
        public void SystemTextJson_SerializeDeserialize_PreservesAllProperties()
        {
            // Arrange
            var original = CreateTestChargeItem();
            var options = new JsonSerializerOptions 
            { 
                WriteIndented = true,
                Converters = { new System.Text.Json.Serialization.JsonStringEnumConverter() }
            };

            // Act
            var json = System.Text.Json.JsonSerializer.Serialize(original, options);
            var deserialized = System.Text.Json.JsonSerializer.Deserialize<ChargeItem>(json, options);

            // Assert
            Assert.AreEqual(original.ftChargeItemId, deserialized.ftChargeItemId);
            Assert.AreEqual(original.Quantity, deserialized.Quantity);
            Assert.AreEqual(original.Description, deserialized.Description);
            Assert.AreEqual(original.Amount, deserialized.Amount);
            Assert.AreEqual(original.VATRate, deserialized.VATRate);
            Assert.AreEqual(original.ftChargeItemCase, deserialized.ftChargeItemCase);
            Assert.AreEqual(original.VATAmount, deserialized.VATAmount);
            Assert.AreEqual(original.Moment, deserialized.Moment);
            Assert.AreEqual(original.Position, deserialized.Position);
            Assert.AreEqual(original.AccountNumber, deserialized.AccountNumber);
            Assert.AreEqual(original.CostCenter, deserialized.CostCenter);
            Assert.AreEqual(original.ProductGroup, deserialized.ProductGroup);
            Assert.AreEqual(original.ProductNumber, deserialized.ProductNumber);
            Assert.AreEqual(original.ProductBarcode, deserialized.ProductBarcode);
            Assert.AreEqual(original.Unit, deserialized.Unit);
            Assert.AreEqual(original.UnitQuantity, deserialized.UnitQuantity);
            Assert.AreEqual(original.UnitPrice, deserialized.UnitPrice);
            Assert.AreEqual(original.Currency, deserialized.Currency);
            Assert.AreEqual(original.DecimalPrecisionMultiplier, deserialized.DecimalPrecisionMultiplier);
        }

        [Test]
        public void BothSerializers_ProduceSameStructure()
        {
            // Arrange
            var item = CreateTestChargeItem();
            var systemTextJsonOptions = new JsonSerializerOptions 
            { 
                WriteIndented = true,
                Converters = { new System.Text.Json.Serialization.JsonStringEnumConverter() }
            };

            // Act
            var newtonsoftJson = JsonConvert.SerializeObject(item, Formatting.Indented);
            var systemTextJson = System.Text.Json.JsonSerializer.Serialize(item, systemTextJsonOptions);

            var newtonsoftDoc = Newtonsoft.Json.Linq.JObject.Parse(newtonsoftJson);
            var systemTextDoc = System.Text.Json.JsonDocument.Parse(systemTextJson);

            // Assert
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
                    
                    if (newtonsoftValue.Type == Newtonsoft.Json.Linq.JTokenType.Object || 
                        newtonsoftValue.Type == Newtonsoft.Json.Linq.JTokenType.Array)
                    {
                        continue;
                    }

                    if (prop.Name == "VATRate" || prop.Name == "VATAmount" || prop.Name == "Amount" || 
                        prop.Name.EndsWith("Price") || prop.Name.EndsWith("Quantity"))
                    {
                        continue;
                    }

                    if (prop.Name == "TimeStamp" || prop.Name.EndsWith("Moment"))
                    {
                        continue;
                    }

                    if (prop.Name.EndsWith("Case") || prop.Name.EndsWith("Format"))
                    {
                        continue;
                    }

                    Assert.AreEqual(
                        newtonsoftValue.ToString(), 
                        GetJsonElementValueAsString(systemTextValue),
                        $"Property '{prop.Name}' has different values in the two serializations");
                }
            }
        }

        private string GetJsonElementValueAsString(System.Text.Json.JsonElement element)
        {
            switch (element.ValueKind)
            {
                case System.Text.Json.JsonValueKind.String:
                    return element.GetString();
                case System.Text.Json.JsonValueKind.Number:
                    return element.GetRawText();
                case System.Text.Json.JsonValueKind.True:
                    return "true";
                case System.Text.Json.JsonValueKind.False:
                    return "false";
                case System.Text.Json.JsonValueKind.Null:
                    return "null";
                default:
                    return element.ToString();
            }
        }
#endif

        [Test]
        public void DecimalPrecisionMultiplier_SerializesAsZeroWhenOne()
        {
            // Arrange
            var item = new ChargeItem
            {
                Description = "Test",
                Amount = 0,
                VATRate = 0,
                DecimalPrecisionMultiplier = 1
            };

            // Act
            var json = JsonConvert.SerializeObject(item);

            // Assert
            Assert.IsTrue(json.Contains("\"DecimalPrecisionMultiplier\":0"));
        }

        [Test]
        public void DecimalPrecisionMultiplier_SerializesAsActualValueWhenNotOne()
        {
            // Arrange
            var item = new ChargeItem
            {
                Description = "Test",
                Amount = 0,
                VATRate = 0,
                DecimalPrecisionMultiplier = 100
            };

            // Act
            var json = JsonConvert.SerializeObject(item);

            // Assert
            Assert.IsTrue(json.Contains("\"DecimalPrecisionMultiplier\":100"));
        }

        [Test]
        public void NullableProperties_SerializeCorrectly()
        {
            // Arrange
            var item = new ChargeItem
            {
                Description = "Test",
                Amount = 0,
                VATRate = 0,
            };

            // Act
            var json = JsonConvert.SerializeObject(item);
            var deserialized = JsonConvert.DeserializeObject<ChargeItem>(json);

            // Assert
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
    }
}