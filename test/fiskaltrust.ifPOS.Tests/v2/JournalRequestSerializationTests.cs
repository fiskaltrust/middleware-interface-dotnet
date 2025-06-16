using fiskaltrust.ifPOS.v2;
using NUnit.Framework;
using Newtonsoft.Json;

#if !WCF
using System.Text.Json;
#endif

namespace fiskaltrust.Middleware.Interface.Tests.v2
{
    [TestFixture]
    public class JournalRequestSerializationTests
    {
        private JournalRequest CreateTestJournalRequest()
        {
            return new JournalRequest
            {
                ftJournalType = 4919338167972134913,
                From = 1640995200000, // 2022-01-01 00:00:00 UTC in milliseconds
                To = 1672531199000,   // 2022-12-31 23:59:59 UTC in milliseconds
                MaxChunkSize = 8192
            };
        }

        [Test]
        public void Newtonsoft_SerializeDeserialize_PreservesAllProperties()
        {
            var original = CreateTestJournalRequest();
            var json = JsonConvert.SerializeObject(original, Formatting.Indented);
            var deserialized = JsonConvert.DeserializeObject<JournalRequest>(json);
            AssertJournalRequestsEqual(original, deserialized);
        }

#if !WCF
        [Test]
        public void SystemTextJson_SerializeDeserialize_PreservesAllProperties()
        {
            var original = CreateTestJournalRequest();
            var json = System.Text.Json.JsonSerializer.Serialize(original);
            var deserialized = System.Text.Json.JsonSerializer.Deserialize<JournalRequest>(json);
            AssertJournalRequestsEqual(original, deserialized);
        }

        [Test]
        public void BothSerializers_ProduceSameOutput()
        {
            var item = CreateTestJournalRequest();
            var newtonsoftJson = JsonConvert.SerializeObject(item);
            var systemTextJson = System.Text.Json.JsonSerializer.Serialize(item);
            Assert.AreEqual(newtonsoftJson, systemTextJson);
        }
#endif

        [Test]
        public void DefaultMaxChunkSize_SerializesCorrectly()
        {
            var request = new JournalRequest
            {
                ftJournalType = 1,
                From = 0,
                To = 1000
            };

            var json = JsonConvert.SerializeObject(request);
            var deserialized = JsonConvert.DeserializeObject<JournalRequest>(json);

            Assert.AreEqual(4096, deserialized.MaxChunkSize);
        }

        [Test]
        public void CustomMaxChunkSize_SerializesCorrectly()
        {
            var request = new JournalRequest
            {
                ftJournalType = 1,
                From = 0,
                To = 1000,
                MaxChunkSize = 16384
            };

            var json = JsonConvert.SerializeObject(request);
            var deserialized = JsonConvert.DeserializeObject<JournalRequest>(json);

            Assert.AreEqual(16384, deserialized.MaxChunkSize);
        }

        [Test]
        public void LargeValues_SerializeCorrectly()
        {
            var request = new JournalRequest
            {
                ftJournalType = long.MaxValue,
                From = long.MaxValue - 1000,
                To = long.MaxValue,
                MaxChunkSize = int.MaxValue
            };

            var json = JsonConvert.SerializeObject(request);
            var deserialized = JsonConvert.DeserializeObject<JournalRequest>(json);

            AssertJournalRequestsEqual(request, deserialized);
        }

        private void AssertJournalRequestsEqual(JournalRequest expected, JournalRequest actual)
        {
            Assert.IsNotNull(actual);
            Assert.AreEqual(expected.ftJournalType, actual.ftJournalType);
            Assert.AreEqual(expected.From, actual.From);
            Assert.AreEqual(expected.To, actual.To);
            Assert.AreEqual(expected.MaxChunkSize, actual.MaxChunkSize);
        }
    }
}