using fiskaltrust.ifPOS.v2;
using NUnit.Framework;
using Newtonsoft.Json;

#if !WCF
using System.Text.Json;
#endif

namespace fiskaltrust.Middleware.Interface.Tests.v2
{
    [TestFixture]
    public class EchoResponseSerializationTests
    {
        private EchoResponse CreateTestEchoResponse()
        {
            return new EchoResponse
            {
                Message = "Hello World!"
            };
        }

        [Test]
        public void Newtonsoft_SerializeDeserialize_PreservesAllProperties()
        {
            var original = CreateTestEchoResponse();
            var json = JsonConvert.SerializeObject(original, Formatting.Indented);
            var deserialized = JsonConvert.DeserializeObject<EchoResponse>(json);
            AssertEchoResponsesEqual(original, deserialized);
        }

#if !WCF
        [Test]
        public void SystemTextJson_SerializeDeserialize_PreservesAllProperties()
        {
            var original = CreateTestEchoResponse();
            var json = System.Text.Json.JsonSerializer.Serialize(original);
            var deserialized = System.Text.Json.JsonSerializer.Deserialize<EchoResponse>(json);
            AssertEchoResponsesEqual(original, deserialized);
        }

        [Test]
        public void BothSerializers_ProduceSameOutput()
        {
            var item = CreateTestEchoResponse();
            var newtonsoftJson = JsonConvert.SerializeObject(item);
            var systemTextJson = System.Text.Json.JsonSerializer.Serialize(item);
            Assert.AreEqual(newtonsoftJson, systemTextJson);
        }
#endif

        [Test]
        public void EmptyMessage_SerializesCorrectly()
        {
            var response = new EchoResponse
            {
                Message = ""
            };

            var json = JsonConvert.SerializeObject(response);
            var deserialized = JsonConvert.DeserializeObject<EchoResponse>(json);

            Assert.AreEqual("", deserialized.Message);
        }

        [Test]
        public void LongMessage_SerializesCorrectly()
        {
            var longMessage = new string('A', 10000);
            var response = new EchoResponse
            {
                Message = longMessage
            };

            var json = JsonConvert.SerializeObject(response);
            var deserialized = JsonConvert.DeserializeObject<EchoResponse>(json);

            Assert.AreEqual(longMessage, deserialized.Message);
        }

        private void AssertEchoResponsesEqual(EchoResponse expected, EchoResponse actual)
        {
            Assert.IsNotNull(actual);
            Assert.AreEqual(expected.Message, actual.Message);
        }
    }
}