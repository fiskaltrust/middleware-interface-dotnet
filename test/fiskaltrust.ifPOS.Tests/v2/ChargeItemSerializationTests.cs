using System;
using System.Globalization;
using fiskaltrust.Middleware.ifPOS.v2.Models;
using fiskaltrust.Middleware.Localization.v2.Models.ifPOS.v2.Cases;
using NUnit.Framework;
using Newtonsoft.Json;

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
                Position = 1.0m,
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
            var json = System.Text.Json.JsonSerializer.Serialize(original);
            var deserialized = System.Text.Json.JsonSerializer.Deserialize<ChargeItem>(json);
            AssertChargeItemsEqual(original, deserialized);
        }

        [Test]
        public void BothSerializers_ProduceSameOutput()
        {
            // Arrange
            var item = CreateTestChargeItem();

            // Act
            var newtonsoftJson = JsonConvert.SerializeObject(item);
            var systemTextJson = System.Text.Json.JsonSerializer.Serialize(item);

            // Assert
            Assert.AreEqual(newtonsoftJson, systemTextJson);
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