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
    public class ReceiptResponseSerializationTests
    {
        private ReceiptResponse CreateTestReceiptResponse()
        {
            return new ReceiptResponse
            {
                ftQueueID = Guid.Parse("12345678-1234-1234-1234-123456789012"),
                ftQueueItemID = Guid.Parse("87654321-4321-4321-4321-210987654321"),
                ftQueueRow = 12345,
                ftCashBoxIdentification = "CB001",
                ftCashBoxID = Guid.Parse("11111111-2222-3333-4444-555555555555"),
                cbTerminalID = "TERM001",
                cbReceiptReference = "RECEIPT-2024-001",
                ftReceiptIdentification = "FT-RECEIPT-001",
                ftReceiptMoment = new DateTime(2024, 1, 15, 10, 30, 0, DateTimeKind.Utc),
                ftReceiptHeader = new List<string> { "Header Line 1", "Header Line 2" },
                ftChargeItems = new List<ChargeItem>
                {
                    new ChargeItem
                    {
                        Description = "Product 1",
                        Amount = 10.99m,
                        VATRate = 19.0m
                    }
                },
                ftChargeLines = new List<string> { "Charge Line 1", "Charge Line 2" },
                ftPayItems = new List<PayItem>
                {
                    new PayItem
                    {
                        Description = "Cash",
                        Amount = 10.99m
                    }
                },
                ftPayLines = new List<string> { "Pay Line 1", "Pay Line 2" },
                ftSignatures = new List<SignatureItem>
                {
                    new SignatureItem
                    {
                        ftSignatureFormat = SignatureFormat.QRCode,
                        ftSignatureType = SignatureType.Unknown,
                        Caption = "Signature",
                        Data = "SIGNATURE_DATA"
                    }
                },
                ftReceiptFooter = new List<string> { "Footer Line 1", "Footer Line 2" },
                ftState = State.Success,
                ftStateData = new { Status = "Success" }
            };
        }

        [Test]
        public void Newtonsoft_SerializeDeserialize_PreservesAllProperties()
        {
            // Arrange
            var original = CreateTestReceiptResponse();

            // Act
            var json = JsonConvert.SerializeObject(original, Formatting.Indented);
            var deserialized = JsonConvert.DeserializeObject<ReceiptResponse>(json);

            // Assert
            Assert.AreEqual(original.ftQueueID, deserialized.ftQueueID);
            Assert.AreEqual(original.ftQueueItemID, deserialized.ftQueueItemID);
            Assert.AreEqual(original.ftQueueRow, deserialized.ftQueueRow);
            Assert.AreEqual(original.ftCashBoxIdentification, deserialized.ftCashBoxIdentification);
            Assert.AreEqual(original.ftCashBoxID, deserialized.ftCashBoxID);
            Assert.AreEqual(original.cbTerminalID, deserialized.cbTerminalID);
            Assert.AreEqual(original.cbReceiptReference, deserialized.cbReceiptReference);
            Assert.AreEqual(original.ftReceiptIdentification, deserialized.ftReceiptIdentification);
            Assert.AreEqual(original.ftReceiptMoment, deserialized.ftReceiptMoment);
            Assert.AreEqual(original.ftReceiptHeader.Count, deserialized.ftReceiptHeader.Count);
            Assert.AreEqual(original.ftChargeItems.Count, deserialized.ftChargeItems.Count);
            Assert.AreEqual(original.ftChargeLines.Count, deserialized.ftChargeLines.Count);
            Assert.AreEqual(original.ftPayItems.Count, deserialized.ftPayItems.Count);
            Assert.AreEqual(original.ftPayLines.Count, deserialized.ftPayLines.Count);
            Assert.AreEqual(original.ftSignatures.Count, deserialized.ftSignatures.Count);
            Assert.AreEqual(original.ftReceiptFooter.Count, deserialized.ftReceiptFooter.Count);
            Assert.AreEqual(original.ftState, deserialized.ftState);
        }

#if NETSTANDARD2_1_TESTS
        [Test]
        public void SystemTextJson_SerializeDeserialize_PreservesAllProperties()
        {
            // Arrange
            var original = CreateTestReceiptResponse();
            var options = new JsonSerializerOptions 
            { 
                WriteIndented = true,
                Converters = { new System.Text.Json.Serialization.JsonStringEnumConverter() }
            };

            // Act
            var json = System.Text.Json.JsonSerializer.Serialize(original, options);
            var deserialized = System.Text.Json.JsonSerializer.Deserialize<ReceiptResponse>(json, options);

            // Assert
            Assert.AreEqual(original.ftQueueID, deserialized.ftQueueID);
            Assert.AreEqual(original.ftQueueItemID, deserialized.ftQueueItemID);
            Assert.AreEqual(original.ftQueueRow, deserialized.ftQueueRow);
            Assert.AreEqual(original.ftCashBoxIdentification, deserialized.ftCashBoxIdentification);
            Assert.AreEqual(original.ftCashBoxID, deserialized.ftCashBoxID);
            Assert.AreEqual(original.cbTerminalID, deserialized.cbTerminalID);
            Assert.AreEqual(original.cbReceiptReference, deserialized.cbReceiptReference);
            Assert.AreEqual(original.ftReceiptIdentification, deserialized.ftReceiptIdentification);
            Assert.AreEqual(original.ftReceiptMoment, deserialized.ftReceiptMoment);
            Assert.AreEqual(original.ftReceiptHeader.Count, deserialized.ftReceiptHeader.Count);
            Assert.AreEqual(original.ftChargeItems.Count, deserialized.ftChargeItems.Count);
            Assert.AreEqual(original.ftChargeLines.Count, deserialized.ftChargeLines.Count);
            Assert.AreEqual(original.ftPayItems.Count, deserialized.ftPayItems.Count);
            Assert.AreEqual(original.ftPayLines.Count, deserialized.ftPayLines.Count);
            Assert.AreEqual(original.ftSignatures.Count, deserialized.ftSignatures.Count);
            Assert.AreEqual(original.ftReceiptFooter.Count, deserialized.ftReceiptFooter.Count);
            Assert.AreEqual(original.ftState, deserialized.ftState);
        }

        [Test]
        public void BothSerializers_ProduceSameStructure()
        {
            // Arrange
            var item = CreateTestReceiptResponse();
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
        public void EmptyCollections_SerializeCorrectly()
        {
            // Arrange
            var response = new ReceiptResponse
            {
                ftQueueID = Guid.NewGuid(),
                ftQueueItemID = Guid.NewGuid(),
                ftQueueRow = 1,
                ftCashBoxIdentification = "CB001",
                ftReceiptIdentification = "RECEIPT001",
                ftReceiptMoment = DateTime.UtcNow,
                ftReceiptHeader = new List<string>(),
                ftChargeItems = new List<ChargeItem>(),
                ftChargeLines = new List<string>(),
                ftPayItems = new List<PayItem>(),
                ftPayLines = new List<string>(),
                ftSignatures = new List<SignatureItem>(),
                ftReceiptFooter = new List<string>(),
                ftState = State.Success
            };

            // Act
            var json = JsonConvert.SerializeObject(response);
            var deserialized = JsonConvert.DeserializeObject<ReceiptResponse>(json);

            // Assert
            Assert.IsNotNull(deserialized.ftReceiptHeader);
            Assert.AreEqual(0, deserialized.ftReceiptHeader.Count);
            Assert.IsNotNull(deserialized.ftChargeItems);
            Assert.AreEqual(0, deserialized.ftChargeItems.Count);
            Assert.IsNotNull(deserialized.ftSignatures);
            Assert.AreEqual(0, deserialized.ftSignatures.Count);
        }
    }
}