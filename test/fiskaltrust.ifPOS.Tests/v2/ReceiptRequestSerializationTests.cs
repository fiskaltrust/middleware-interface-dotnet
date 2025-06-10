using System;
using System.Collections.Generic;
using fiskaltrust.ifPOS.v2;
using fiskaltrust.ifPOS.v2.Cases;
using NUnit.Framework;
using Newtonsoft.Json;

#if NETSTANDARD2_1_TESTS
using System.Text.Json;
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
                        Quantity = 1.5m,
                        Position = 1.5m
                    }
                },
                cbPayItems = new List<PayItem>
                {
                    new PayItem
                    {
                        Description = "Cash",
                        Amount = 10.99m,
                        ftPayItemCase = 0,
                        Position = 1.5m
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
            var original = CreateTestReceiptRequest();
            var json = JsonConvert.SerializeObject(original, Formatting.Indented);
            var deserialized = JsonConvert.DeserializeObject<ReceiptRequest>(json);
            AssertReceiptRequestsEqual(original, deserialized);
        }

#if NETSTANDARD2_1_TESTS
        [Test]
        public void SystemTextJson_SerializeDeserialize_PreservesAllProperties()
        {
            var original = CreateTestReceiptRequest();
            var json = System.Text.Json.JsonSerializer.Serialize(original);
            var deserialized = System.Text.Json.JsonSerializer.Deserialize<ReceiptRequest>(json);
            AssertReceiptRequestsEqual(original, deserialized);
        }

        [Test]
        public void BothSerializers_ProduceSameOutput()
        {
            // Arrange
            var item = CreateTestReceiptRequest();

            // Act
            var newtonsoftJson = JsonConvert.SerializeObject(item);
            var systemTextJson = System.Text.Json.JsonSerializer.Serialize(item);

            // Assert
            Assert.AreEqual(newtonsoftJson, systemTextJson);
        }
#endif

        [Test]
        public void DecimalPrecisionMultiplier_ShouldNotSerialize_WhenOne()
        {
            var request = new ReceiptRequest
            {
                cbReceiptReference = "TEST",
                cbReceiptMoment = DateTime.UtcNow,
                cbChargeItems = new List<ChargeItem>(),
                cbPayItems = new List<PayItem>(),
                DecimalPrecisionMultiplier = 1
            };

            var json = JsonConvert.SerializeObject(request);
            Assert.IsFalse(json.Contains("\"DecimalPrecisionMultiplier\":"));
        }

        [Test]
        public void DecimalPrecisionMultiplier_SerializesAsActualValueWhenNotOne()
        {
            var request = new ReceiptRequest
            {
                cbReceiptReference = "TEST",
                cbReceiptMoment = DateTime.UtcNow,
                cbChargeItems = new List<ChargeItem>(),
                cbPayItems = new List<PayItem>(),
                DecimalPrecisionMultiplier = 100
            };

            var json = JsonConvert.SerializeObject(request);
            Assert.IsTrue(json.Contains("\"DecimalPrecisionMultiplier\":100"));
        }

        [Test]
        public void NullableProperties_SerializeCorrectly()
        {
            var request = new ReceiptRequest
            {
                cbReceiptReference = "TEST",
                cbReceiptMoment = DateTime.UtcNow,
                cbChargeItems = new List<ChargeItem>(),
                cbPayItems = new List<PayItem>()
            };

            var json = JsonConvert.SerializeObject(request);
            var deserialized = JsonConvert.DeserializeObject<ReceiptRequest>(json);

            Assert.IsNull(deserialized.cbTerminalID);
            Assert.IsNull(deserialized.cbPreviousReceiptReference);
            Assert.IsNull(deserialized.cbUser);
            Assert.IsNull(deserialized.cbArea);
            Assert.IsNull(deserialized.cbCustomer);
            Assert.IsNull(deserialized.cbSettlement);
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