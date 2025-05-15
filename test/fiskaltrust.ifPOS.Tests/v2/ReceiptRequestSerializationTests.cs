using System;
using System.Collections.Generic;
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
            Assert.AreEqual(original.cbTerminalID, deserialized.cbTerminalID);
            Assert.AreEqual(original.cbReceiptReference, deserialized.cbReceiptReference);
            Assert.AreEqual(original.cbReceiptMoment, deserialized.cbReceiptMoment);
            Assert.AreEqual(original.cbChargeItems.Count, deserialized.cbChargeItems.Count);
            Assert.AreEqual(original.cbPayItems.Count, deserialized.cbPayItems.Count);
            Assert.AreEqual(original.ftCashBoxID, deserialized.ftCashBoxID);
            Assert.AreEqual(original.ftPosSystemId, deserialized.ftPosSystemId);
            Assert.AreEqual(original.ftReceiptCase, deserialized.ftReceiptCase);
            Assert.AreEqual(original.ftQueueID, deserialized.ftQueueID);
            Assert.AreEqual(original.cbPreviousReceiptReference, deserialized.cbPreviousReceiptReference);
            Assert.AreEqual(original.cbReceiptAmount, deserialized.cbReceiptAmount);
            Assert.AreEqual(original.cbUser, deserialized.cbUser);
            Assert.AreEqual(original.cbArea, deserialized.cbArea);
            Assert.AreEqual(original.cbCustomer, deserialized.cbCustomer);
            Assert.AreEqual(original.cbSettlement, deserialized.cbSettlement);
            Assert.AreEqual(original.Currency, deserialized.Currency);
            Assert.AreEqual(original.DecimalPrecisionMultiplier, deserialized.DecimalPrecisionMultiplier);
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
                Converters = { new System.Text.Json.Serialization.JsonStringEnumConverter() }
            };

            // Act
            var json = System.Text.Json.JsonSerializer.Serialize(original, options);
            var deserialized = System.Text.Json.JsonSerializer.Deserialize<ReceiptRequest>(json, options);

            // Assert
            Assert.AreEqual(original.cbTerminalID, deserialized.cbTerminalID);
            Assert.AreEqual(original.cbReceiptReference, deserialized.cbReceiptReference);
            Assert.AreEqual(original.cbReceiptMoment, deserialized.cbReceiptMoment);
            Assert.AreEqual(original.cbChargeItems.Count, deserialized.cbChargeItems.Count);
            Assert.AreEqual(original.cbPayItems.Count, deserialized.cbPayItems.Count);
            Assert.AreEqual(original.ftCashBoxID, deserialized.ftCashBoxID);
            Assert.AreEqual(original.ftPosSystemId, deserialized.ftPosSystemId);
            Assert.AreEqual(original.ftReceiptCase, deserialized.ftReceiptCase);
            Assert.AreEqual(original.ftQueueID, deserialized.ftQueueID);
            Assert.AreEqual(original.cbPreviousReceiptReference, deserialized.cbPreviousReceiptReference);
            Assert.AreEqual(original.cbReceiptAmount, deserialized.cbReceiptAmount);
            Assert.AreEqual(original.cbUser, deserialized.cbUser);
            Assert.AreEqual(original.cbArea, deserialized.cbArea);
            Assert.AreEqual(original.cbCustomer, deserialized.cbCustomer);
            Assert.AreEqual(original.cbSettlement, deserialized.cbSettlement);
            Assert.AreEqual(original.Currency, deserialized.Currency);
            Assert.AreEqual(original.DecimalPrecisionMultiplier, deserialized.DecimalPrecisionMultiplier);
        }

        [Test]
        public void BothSerializers_ProduceSameStructure()
        {
            // Arrange
            var item = CreateTestReceiptRequest();
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
    }
}