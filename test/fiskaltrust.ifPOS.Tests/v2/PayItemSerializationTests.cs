using System;
using fiskaltrust.Middleware.ifPOS.v2.Models;
using fiskaltrust.Middleware.Localization.v2.Models.ifPOS.v2.Cases;
using NUnit.Framework;
using Newtonsoft.Json;

#if NETSTANDARD2_1
using System.Text.Json;
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
            Assert.AreEqual(original.ftPayItemId, deserialized.ftPayItemId);
            Assert.AreEqual(original.Quantity, deserialized.Quantity);
            Assert.AreEqual(original.Description, deserialized.Description);
            Assert.AreEqual(original.Amount, deserialized.Amount);
            Assert.AreEqual(original.ftPayItemCase, deserialized.ftPayItemCase);
            Assert.AreEqual(original.Moment, deserialized.Moment);
            Assert.AreEqual(original.Position, deserialized.Position);
            Assert.AreEqual(original.AccountNumber, deserialized.AccountNumber);
            Assert.AreEqual(original.CostCenter, deserialized.CostCenter);
            Assert.AreEqual(original.MoneyGroup, deserialized.MoneyGroup);
            Assert.AreEqual(original.MoneyNumber, deserialized.MoneyNumber);
            Assert.AreEqual(original.MoneyBarcode, deserialized.MoneyBarcode);
            Assert.AreEqual(original.Currency, deserialized.Currency);
            Assert.AreEqual(original.DecimalPrecisionMultiplier, deserialized.DecimalPrecisionMultiplier);
        }

#if NETSTANDARD2_1
        [Test]
        public void SystemTextJson_SerializeDeserialize_PreservesAllProperties()
        {
            // Arrange
            var original = CreateTestPayItem();
            var options = new JsonSerializerOptions 
            { 
                WriteIndented = true,
                Converters = { new System.Text.Json.Serialization.JsonStringEnumConverter() }
            };

            // Act
            var json = System.Text.Json.JsonSerializer.Serialize(original, options);
            var deserialized = System.Text.Json.JsonSerializer.Deserialize<PayItem>(json, options);

            // Assert
            Assert.AreEqual(original.ftPayItemId, deserialized.ftPayItemId);
            Assert.AreEqual(original.Quantity, deserialized.Quantity);
            Assert.AreEqual(original.Description, deserialized.Description);
            Assert.AreEqual(original.Amount, deserialized.Amount);
            Assert.AreEqual(original.ftPayItemCase, deserialized.ftPayItemCase);
            Assert.AreEqual(original.Moment, deserialized.Moment);
            Assert.AreEqual(original.Position, deserialized.Position);
            Assert.AreEqual(original.AccountNumber, deserialized.AccountNumber);
            Assert.AreEqual(original.CostCenter, deserialized.CostCenter);
            Assert.AreEqual(original.MoneyGroup, deserialized.MoneyGroup);
            Assert.AreEqual(original.MoneyNumber, deserialized.MoneyNumber);
            Assert.AreEqual(original.MoneyBarcode, deserialized.MoneyBarcode);
            Assert.AreEqual(original.Currency, deserialized.Currency);
            Assert.AreEqual(original.DecimalPrecisionMultiplier, deserialized.DecimalPrecisionMultiplier);
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
            Assert.AreEqual(1, deserialized.Quantity); 
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
    }
}