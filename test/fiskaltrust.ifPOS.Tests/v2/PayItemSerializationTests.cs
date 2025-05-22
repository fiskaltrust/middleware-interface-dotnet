using System;
using System.Reflection;
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
                Position = 1.0m,
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
            var original = CreateTestPayItem();
            var json = JsonConvert.SerializeObject(original, Formatting.Indented);
            var deserialized = JsonConvert.DeserializeObject<PayItem>(json);
            AssertPayItemsEqual(original, deserialized);
        }

#if NETSTANDARD2_1_TESTS
        [Test]
        public void SystemTextJson_SerializeDeserialize_PreservesAllProperties()
        {
            var original = CreateTestPayItem();
            var json = System.Text.Json.JsonSerializer.Serialize(original);
            var deserialized = System.Text.Json.JsonSerializer.Deserialize<PayItem>(json);
            AssertPayItemsEqual(original, deserialized);
        }

        [Test]
        public void BothSerializers_ProduceSameOutput()
        {
            // Arrange
            var item = CreateTestPayItem();

            // Act
            var newtonsoftJson = JsonConvert.SerializeObject(item);
            var systemTextJson = System.Text.Json.JsonSerializer.Serialize(item);

            // Assert
            Assert.AreEqual(newtonsoftJson, systemTextJson);
        }
#endif

        [Test]
        public void QuantitySerialization_HandlesDefaultValue()
        {
            var item = new PayItem
            {
                Description = "Test",
                Amount = 10m,
                Quantity = 1 
            };

            var json = JsonConvert.SerializeObject(item);
            var deserialized = JsonConvert.DeserializeObject<PayItem>(json);

            Assert.IsFalse(json.Contains("\"Quantity\":1"));
            
            var fieldInfo = typeof(PayItem).GetField("_quantity", BindingFlags.NonPublic | BindingFlags.Instance);
            var actualValue = (decimal)fieldInfo.GetValue(deserialized);
            Assert.AreEqual(1m, actualValue);
        }

        [Test]
        public void QuantitySerialization_HandlesNonDefaultValue()
        {
            var item = new PayItem
            {
                Description = "Test",
                Amount = 10m,
                Quantity = 2.5m
            };

            var json = JsonConvert.SerializeObject(item);
            var deserialized = JsonConvert.DeserializeObject<PayItem>(json);

            Assert.IsTrue(json.Contains("\"Quantity\":2.5"));
            Assert.AreEqual(2.5m, deserialized.Quantity);
        }

        [Test]
        public void DecimalPrecisionMultiplier_SerializesAsZeroWhenOne()
        {
            var item = new PayItem
            {
                Description = "Test",
                Amount = 0,
                DecimalPrecisionMultiplier = 1
            };

            var json = JsonConvert.SerializeObject(item);
            Assert.IsTrue(json.Contains("\"DecimalPrecisionMultiplier\":0"));
        }

        [Test]
        public void DecimalPrecisionMultiplier_SerializesAsActualValueWhenNotOne()
        {
            var item = new PayItem
            {
                Description = "Test",
                Amount = 0,
                DecimalPrecisionMultiplier = 100
            };

            var json = JsonConvert.SerializeObject(item);
            Assert.IsTrue(json.Contains("\"DecimalPrecisionMultiplier\":100"));
        }

        [Test]
        public void NullableProperties_SerializeCorrectly()
        {
            var item = new PayItem
            {
                Description = "Test",
                Amount = 0
            };

            var json = JsonConvert.SerializeObject(item);
            var deserialized = JsonConvert.DeserializeObject<PayItem>(json);

            Assert.IsNull(deserialized.ftPayItemId);
            Assert.IsNull(deserialized.Moment);
            Assert.IsNull(deserialized.AccountNumber);
            Assert.IsNull(deserialized.CostCenter);
            Assert.IsNull(deserialized.MoneyGroup);
            Assert.IsNull(deserialized.MoneyNumber);
            Assert.IsNull(deserialized.MoneyBarcode);
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