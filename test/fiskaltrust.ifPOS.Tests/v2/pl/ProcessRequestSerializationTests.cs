#if !WCF
using System;
using fiskaltrust.ifPOS.v2;
using fiskaltrust.ifPOS.v2.pl;
using NUnit.Framework;
using Newtonsoft.Json;

namespace fiskaltrust.Middleware.Interface.Tests.v2.pl
{
    [TestFixture]
    public class ProcessRequestSerializationTests
    {
        private ProcessRequest CreateTestProcessRequest()
        {
            return new ProcessRequest
            {
                ReceiptRequest = new ReceiptRequest
                {
                    cbReceiptReference = "pl-receipt-1",
                    ftCashBoxID = Guid.Parse("11111111-2222-3333-4444-555555555555")
                },
                ReceiptResponse = new ReceiptResponse
                {
                    ftCashBoxID = Guid.Parse("11111111-2222-3333-4444-555555555555"),
                    ftCashBoxIdentification = "ZAS1234567890",
                    ftReceiptIdentification = "ft1#"
                }
            };
        }

        [Test]
        public void Newtonsoft_SerializeDeserialize_PreservesNestedProperties()
        {
            var original = CreateTestProcessRequest();
            var json = JsonConvert.SerializeObject(original);
            var deserialized = JsonConvert.DeserializeObject<ProcessRequest>(json);

            Assert.IsNotNull(deserialized);
            Assert.AreEqual(original.ReceiptRequest.cbReceiptReference, deserialized.ReceiptRequest.cbReceiptReference);
            Assert.AreEqual(original.ReceiptResponse.ftCashBoxIdentification, deserialized.ReceiptResponse.ftCashBoxIdentification);
        }

        [Test]
        public void SystemTextJson_SerializeDeserialize_PreservesNestedProperties()
        {
            var original = CreateTestProcessRequest();
            var json = System.Text.Json.JsonSerializer.Serialize(original);
            var deserialized = System.Text.Json.JsonSerializer.Deserialize<ProcessRequest>(json);

            Assert.IsNotNull(deserialized);
            Assert.AreEqual(original.ReceiptRequest.cbReceiptReference, deserialized.ReceiptRequest.cbReceiptReference);
            Assert.AreEqual(original.ReceiptResponse.ftCashBoxIdentification, deserialized.ReceiptResponse.ftCashBoxIdentification);
        }

        [Test]
        public void ProcessResponse_SerializeDeserialize_PreservesReceiptResponse()
        {
            var original = new ProcessResponse
            {
                ReceiptResponse = new ReceiptResponse
                {
                    ftCashBoxID = Guid.Parse("11111111-2222-3333-4444-555555555555"),
                    ftReceiptIdentification = "ft1#"
                }
            };
            var json = System.Text.Json.JsonSerializer.Serialize(original);
            var deserialized = System.Text.Json.JsonSerializer.Deserialize<ProcessResponse>(json);

            Assert.IsNotNull(deserialized);
            Assert.AreEqual(original.ReceiptResponse.ftCashBoxID, deserialized.ReceiptResponse.ftCashBoxID);
            Assert.AreEqual(original.ReceiptResponse.ftReceiptIdentification, deserialized.ReceiptResponse.ftReceiptIdentification);
        }
    }
}
#endif
