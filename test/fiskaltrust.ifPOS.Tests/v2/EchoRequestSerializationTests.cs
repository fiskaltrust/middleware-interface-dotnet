using fiskaltrust.ifPOS.v2;
using NUnit.Framework;
using Newtonsoft.Json;

#if !WCF
using System.Text.Json;
#endif

namespace fiskaltrust.Middleware.Interface.Tests.v2
{
    [TestFixture]
    public class EchoRequestSerializationTests
    {
        private EchoRequest CreateTestEchoRequest()
        {
            return new EchoRequest
            {
                Message = "Hello World!"
            };
        }

        [Test]
        public void Newtonsoft_SerializeDeserialize_PreservesAllProperties()
        {
            var original = CreateTestEchoRequest();
            var json = JsonConvert.SerializeObject(original, Formatting.Indented);
            var deserialized = JsonConvert.DeserializeObject<EchoRequest>(json);
            AssertEchoRequestsEqual(original, deserialized);
        }

#if !WCF
        [Test]
        public void SystemTextJson_SerializeDeserialize_PreservesAllProperties()
        {
            var original = CreateTestEchoRequest();
            var json = System.Text.Json.JsonSerializer.Serialize(original);
            var deserialized = System.Text.Json.JsonSerializer.Deserialize<EchoRequest>(json);
            AssertEchoRequestsEqual(original, deserialized);
        }

        [Test]
        public void BothSerializers_ProduceSameOutput()
        {
            var item = CreateTestEchoRequest();
            var newtonsoftJson = JsonConvert.SerializeObject(item);
            var systemTextJson = System.Text.Json.JsonSerializer.Serialize(item);
            Assert.AreEqual(newtonsoftJson, systemTextJson);
        }
#endif

        [Test]
        public void EmptyMessage_SerializesCorrectly()
        {
            var request = new EchoRequest
            {
                Message = ""
            };

            var json = JsonConvert.SerializeObject(request);
            var deserialized = JsonConvert.DeserializeObject<EchoRequest>(json);

            Assert.AreEqual("", deserialized.Message);
        }

        [Test]
        public void SpecialCharacters_SerializeCorrectly()
        {
            var request = new EchoRequest
            {
                Message = "Special chars: ąęćłńóśźż !@#$%^&*()"
            };

            var json = JsonConvert.SerializeObject(request);
            var deserialized = JsonConvert.DeserializeObject<EchoRequest>(json);

            Assert.AreEqual(request.Message, deserialized.Message);
        }

        private void AssertEchoRequestsEqual(EchoRequest expected, EchoRequest actual)
        {
            Assert.IsNotNull(actual);
            Assert.AreEqual(expected.Message, actual.Message);
        }
    }
}