using System;
using System.Collections.Generic;
using fiskaltrust.ifPOS.v2;
using fiskaltrust.ifPOS.v2.Cases;
using NUnit.Framework;
using Newtonsoft.Json;
using System.ServiceModel.Security;


#if !WCF
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
                cbUser = new { User = "User1" },
                cbArea = "Area1",
                cbCustomer = "Customer001",
                cbSettlement = "Settlement1",
                Currency = Currency.EUR,
                DecimalPrecisionMultiplier = 100
            };
        }

        [Test]
        public void Newtonsoft_object_IsSerializedAsJson()
        {
            var request = CreateTestReceiptRequest();
            var json = JsonConvert.SerializeObject(request);
            Assert.IsTrue(json.Contains("\"cbUser\":{\"User\":\"User1\"}"));
            Assert.IsTrue(json.Contains("\"cbArea\":\"Area1\""));
        }
#if !WCF
        [Test]
        public void SystemTextJson_object_IsSerializedAsJson()
        {
            var request = CreateTestReceiptRequest();
            var json = System.Text.Json.JsonSerializer.Serialize(request);
            Assert.IsTrue(json.Contains("\"cbUser\":{\"User\":\"User1\"}"));
            Assert.IsTrue(json.Contains("\"cbArea\":\"Area1\""));
        }
#endif

        [Test]
        public void Newtonsoft_SerializeDeserialize_PreservesAllProperties()
        {
            var original = CreateTestReceiptRequest();
            var json = JsonConvert.SerializeObject(original, Formatting.Indented);
            var deserialized = JsonConvert.DeserializeObject<ReceiptRequest>(json);
            AssertReceiptRequestsEqual(original, deserialized, true);
        }

#if !WCF
        [Test]
        public void SystemTextJson_SerializeDeserialize_PreservesAllProperties()
        {
            var original = CreateTestReceiptRequest();
            var json = System.Text.Json.JsonSerializer.Serialize(original);
            var deserialized = System.Text.Json.JsonSerializer.Deserialize<ReceiptRequest>(json);
            AssertReceiptRequestsEqual(original, deserialized, false);
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
        public void Newtonsoft_SinglePreviousReceiptReference_SerializesAsString()
        {
            // Arrange
            var request = CreateTestReceiptRequest();
            request.cbPreviousReceiptReference = "SINGLE-REFERENCE";

            Assert.That(request.cbPreviousReceiptReference.IsSingle);
            Assert.AreEqual(request.cbPreviousReceiptReference.SingleValue, "SINGLE-REFERENCE");

            // Act
            var json = JsonConvert.SerializeObject(request);

            // Assert
            Assert.IsTrue(json.Contains("\"cbPreviousReceiptReference\":\"SINGLE-REFERENCE\""));
            Assert.IsFalse(json.Contains("\"cbPreviousReceiptReference\":[\"SINGLE-REFERENCE\"]"));
        }

        [Test]
        public void Newtonsoft_MultiplePreviousReceiptReferences_SerializesAsArray()
        {
            // Arrange
            var request = CreateTestReceiptRequest();
            request.cbPreviousReceiptReference = new string[] { "REF-1", "REF-2" };

            Assert.That(request.cbPreviousReceiptReference.IsGroup);
            Assert.AreEqual(request.cbPreviousReceiptReference.GroupValue, new string[] { "REF-1", "REF-2" });
            // Act
            var json = JsonConvert.SerializeObject(request);

            request.cbPreviousReceiptReference.Match(
                single => Console.WriteLine(single),
                group => Console.WriteLine(string.Join(", ", group))
            );

            // Assert
            Assert.IsTrue(json.Contains("\"cbPreviousReceiptReference\":[\"REF-1\",\"REF-2\"]"));
        }

        [Test]
        public void Newtonsoft_NullPreviousReceiptReference_SerializesAsString()
        {
            // Arrange
            var request = CreateTestReceiptRequest();
            request.cbPreviousReceiptReference = null;

            // Act
            var json = JsonConvert.SerializeObject(request);

            // Assert
            Assert.IsTrue(json.Contains("\"cbPreviousReceiptReference\":"));
        }

        [Test]
        public void Newtonsoft_StringInput_DeserializesToSingleItemArray()
        {
            // Arrange
            string json = "{\"cbReceiptReference\":\"TEST\",\"cbReceiptMoment\":\"2023-01-01T12:00:00Z\",\"cbChargeItems\":[],\"cbPayItems\":[],\"cbPreviousReceiptReference\":\"SINGLE-REF\"}";

            // Act
            var deserialized = JsonConvert.DeserializeObject<ReceiptRequest>(json);

            // Assert
            Assert.IsNotNull(deserialized.cbPreviousReceiptReference);
            Assert.That(deserialized.cbPreviousReceiptReference.IsSingle);
            Assert.AreEqual("SINGLE-REF", deserialized.cbPreviousReceiptReference.SingleValue);
        }

        [Test]
        public void Newtonsoft_Null_DeserializesToSingleItemArray()
        {
            // Arrange
            string json = "{\"cbReceiptReference\":\"TEST\",\"cbReceiptMoment\":\"2023-01-01T12:00:00Z\",\"cbChargeItems\":[],\"cbPayItems\":[]}";

            // Act
            var deserialized = JsonConvert.DeserializeObject<ReceiptRequest>(json);

            // Assert
            Assert.IsNull(deserialized.cbPreviousReceiptReference);
        }

        [Test]
        public void Newtonsoft_ArrayInput_DeserializesToArray()
        {
            // Arrange
            string json = "{\"cbReceiptReference\":\"TEST\",\"cbReceiptMoment\":\"2023-01-01T12:00:00Z\",\"cbChargeItems\":[],\"cbPayItems\":[],\"cbPreviousReceiptReference\":[\"REF-1\",\"REF-2\"]}";

            // Act
            var deserialized = JsonConvert.DeserializeObject<ReceiptRequest>(json);

            // Assert
            Assert.IsNotNull(deserialized.cbPreviousReceiptReference);
            Assert.That(deserialized.cbPreviousReceiptReference.IsGroup);
            Assert.AreEqual("REF-1", deserialized.cbPreviousReceiptReference.GroupValue[0]);
            Assert.AreEqual("REF-2", deserialized.cbPreviousReceiptReference.GroupValue[1]);
        }

#if !WCF
        [Test]
        public void SystemTextJson_SinglePreviousReceiptReference_SerializesAsString()
        {
            // Arrange
            var request = CreateTestReceiptRequest();
            request.cbPreviousReceiptReference = "SINGLE-REFERENCE";

            // Act
            var json = System.Text.Json.JsonSerializer.Serialize(request);

            // Assert
            Assert.IsTrue(json.Contains("\"cbPreviousReceiptReference\":\"SINGLE-REFERENCE\""));
            Assert.IsFalse(json.Contains("\"cbPreviousReceiptReference\":[\"SINGLE-REFERENCE\"]"));
        }

        [Test]
        public void SystemTextJson_MultiplePreviousReceiptReferences_SerializesAsArray()
        {
            // Arrange
            var request = CreateTestReceiptRequest();
            request.cbPreviousReceiptReference = new string[] { "REF-1", "REF-2" };

            // Act
            var json = System.Text.Json.JsonSerializer.Serialize(request);

            // Assert
            Assert.IsTrue(json.Contains("\"cbPreviousReceiptReference\":[\"REF-1\",\"REF-2\"]"));
        }

        [Test]
        public void SystemTextJson_StringInput_DeserializesToSingleItemArray()
        {
            // Arrange
            string json = "{\"cbReceiptReference\":\"TEST\",\"cbReceiptMoment\":\"2023-01-01T12:00:00Z\",\"cbChargeItems\":[],\"cbPayItems\":[],\"cbPreviousReceiptReference\":\"SINGLE-REF\"}";

            // Act
            var deserialized = System.Text.Json.JsonSerializer.Deserialize<ReceiptRequest>(json);

            // Assert
            Assert.IsNotNull(deserialized.cbPreviousReceiptReference);
            Assert.That(deserialized.cbPreviousReceiptReference.IsSingle);
            Assert.AreEqual("SINGLE-REF", deserialized.cbPreviousReceiptReference.SingleValue);
        }

        [Test]
        public void SystemTextJson_ArrayInput_DeserializesToArray()
        {
            // Arrange
            string json = "{\"cbReceiptReference\":\"TEST\",\"cbReceiptMoment\":\"2023-01-01T12:00:00Z\",\"cbChargeItems\":[],\"cbPayItems\":[],\"cbPreviousReceiptReference\":[\"REF-1\",\"REF-2\"]}";

            // Act
            var deserialized = System.Text.Json.JsonSerializer.Deserialize<ReceiptRequest>(json);

            // Assert
            Assert.IsNotNull(deserialized.cbPreviousReceiptReference);
            Assert.That(deserialized.cbPreviousReceiptReference.IsGroup);
            Assert.AreEqual("REF-1", deserialized.cbPreviousReceiptReference.GroupValue[0]);
            Assert.AreEqual("REF-2", deserialized.cbPreviousReceiptReference.GroupValue[1]);
        }
#endif

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

        private void AssertReceiptRequestsEqual(ReceiptRequest expected, ReceiptRequest actual, bool newtonsoft)
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
            if (newtonsoft)
            {
                Assert.AreEqual(JsonConvert.SerializeObject(expected.cbUser), JsonConvert.SerializeObject(actual.cbUser));
            }
            else
            {
#if !WCF
                Assert.AreEqual(System.Text.Json.JsonSerializer.Serialize(expected.cbUser), System.Text.Json.JsonSerializer.Serialize(actual.cbUser));
#endif
            }
            Assert.AreEqual(expected.cbArea?.ToString(), actual.cbArea?.ToString());
            Assert.AreEqual(expected.cbCustomer?.ToString(), actual.cbCustomer?.ToString());
            Assert.AreEqual(expected.cbSettlement?.ToString(), actual.cbSettlement?.ToString());
            Assert.AreEqual(expected.Currency, actual.Currency);
            Assert.AreEqual(expected.DecimalPrecisionMultiplier, actual.DecimalPrecisionMultiplier);
        }
    }
}