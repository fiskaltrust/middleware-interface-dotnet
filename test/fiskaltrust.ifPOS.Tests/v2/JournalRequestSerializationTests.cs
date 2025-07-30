using fiskaltrust.ifPOS.v2;
using NUnit.Framework;
using Newtonsoft.Json;
using fiskaltrust.ifPOS.v2.Cases;
using System;

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
                ftJournalType = (JournalType)0x4445000000000001,
                From = new DateTime(2022, 01, 01).Ticks,
                To = new DateTime(2022, 12, 31).Ticks,
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

        private void AssertJournalRequestsEqual(JournalRequest expected, JournalRequest actual)
        {
            Assert.IsNotNull(actual);
            Assert.AreEqual(expected.ftJournalType, actual.ftJournalType);
            Assert.AreEqual(expected.From, actual.From);
            Assert.AreEqual(expected.To, actual.To);
        }
    }
}