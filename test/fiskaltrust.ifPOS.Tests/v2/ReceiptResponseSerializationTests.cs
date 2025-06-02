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
                        VATRate = 19.0m,
                        Quantity = 1.5m,
                        Position = 1.5m
                    }
                },
                ftChargeLines = new List<string> { "Charge Line 1", "Charge Line 2" },
                ftPayItems = new List<PayItem>
                {
                    new PayItem
                    {
                        Description = "Cash",
                        Amount = 10.99m,
                        Position = 1.5m
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
            var original = CreateTestReceiptResponse();
            var json = JsonConvert.SerializeObject(original, Formatting.Indented);
            var deserialized = JsonConvert.DeserializeObject<ReceiptResponse>(json);
            AssertReceiptResponsesEqual(original, deserialized);
        }

#if NETSTANDARD2_1_TESTS
        [Test]
        public void SystemTextJson_SerializeDeserialize_PreservesAllProperties()
        {
            var original = CreateTestReceiptResponse();
            var json = System.Text.Json.JsonSerializer.Serialize(original);
            var deserialized = System.Text.Json.JsonSerializer.Deserialize<ReceiptResponse>(json);
            AssertReceiptResponsesEqual(original, deserialized);
        }

        [Test]
        public void BothSerializers_ProduceSameOutput()
        {
            // Arrange
            var item = CreateTestReceiptResponse();

            // Act
            var newtonsoftJson = JsonConvert.SerializeObject(item);
            var systemTextJson = System.Text.Json.JsonSerializer.Serialize(item);

            // Assert
            Assert.AreEqual(newtonsoftJson, systemTextJson);
        }
#endif

        [Test]
        public void EmptyCollections_SerializeCorrectly()
        {
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

            var json = JsonConvert.SerializeObject(response);
            var deserialized = JsonConvert.DeserializeObject<ReceiptResponse>(json);

            Assert.IsNotNull(deserialized.ftReceiptHeader);
            Assert.AreEqual(0, deserialized.ftReceiptHeader.Count);
            Assert.IsNotNull(deserialized.ftChargeItems);
            Assert.AreEqual(0, deserialized.ftChargeItems.Count);
            Assert.IsNotNull(deserialized.ftSignatures);
            Assert.AreEqual(0, deserialized.ftSignatures.Count);
        }

        [Test]
        public void NullableProperties_SerializeCorrectly()
        {
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

            var json = JsonConvert.SerializeObject(response);
            var deserialized = JsonConvert.DeserializeObject<ReceiptResponse>(json);

            Assert.IsNull(deserialized.ftCashBoxID);
            Assert.IsNull(deserialized.cbTerminalID);
            Assert.IsNull(deserialized.cbReceiptReference);
            Assert.IsNull(deserialized.ftStateData);
        }

        private void AssertReceiptResponsesEqual(ReceiptResponse expected, ReceiptResponse actual)
        {
            Assert.AreEqual(expected.ftQueueID, actual.ftQueueID);
            Assert.AreEqual(expected.ftQueueItemID, actual.ftQueueItemID);
            Assert.AreEqual(expected.ftQueueRow, actual.ftQueueRow);
            Assert.AreEqual(expected.ftCashBoxIdentification, actual.ftCashBoxIdentification);
            Assert.AreEqual(expected.ftCashBoxID, actual.ftCashBoxID);
            Assert.AreEqual(expected.cbTerminalID, actual.cbTerminalID);
            Assert.AreEqual(expected.cbReceiptReference, actual.cbReceiptReference);
            Assert.AreEqual(expected.ftReceiptIdentification, actual.ftReceiptIdentification);
            Assert.AreEqual(expected.ftReceiptMoment, actual.ftReceiptMoment);
            Assert.AreEqual(expected.ftReceiptHeader.Count, actual.ftReceiptHeader.Count);
            Assert.AreEqual(expected.ftChargeItems.Count, actual.ftChargeItems.Count);
            Assert.AreEqual(expected.ftChargeLines.Count, actual.ftChargeLines.Count);
            Assert.AreEqual(expected.ftPayItems.Count, actual.ftPayItems.Count);
            Assert.AreEqual(expected.ftPayLines.Count, actual.ftPayLines.Count);
            Assert.AreEqual(expected.ftSignatures.Count, actual.ftSignatures.Count);
            Assert.AreEqual(expected.ftReceiptFooter.Count, actual.ftReceiptFooter.Count);
            Assert.AreEqual(expected.ftState, actual.ftState);
        }
    }
}