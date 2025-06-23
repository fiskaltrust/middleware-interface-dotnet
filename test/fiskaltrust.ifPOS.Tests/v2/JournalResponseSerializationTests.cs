using System.Collections.Generic;
using fiskaltrust.ifPOS.v2;
using NUnit.Framework;
using Newtonsoft.Json;

#if NETCOREAPP3_0_OR_GREATER
using System.Text.Json;
#endif

namespace fiskaltrust.Middleware.Interface.Tests.v2
{
    [TestFixture]
    public class JournalResponseSerializationTests
    {
        private JournalResponse CreateTestJournalResponse()
        {
            return new JournalResponse
            {
                Chunk = new List<byte> { 0x48, 0x65, 0x6C, 0x6C, 0x6F, 0x20, 0x57, 0x6F, 0x72, 0x6C, 0x64 }
            };
        }

        [Test]
        public void Newtonsoft_SerializeDeserialize_PreservesAllProperties()
        {
            var original = CreateTestJournalResponse();
            var json = JsonConvert.SerializeObject(original, Formatting.Indented);
            var deserialized = JsonConvert.DeserializeObject<JournalResponse>(json);
            AssertJournalResponsesEqual(original, deserialized);
        }

#if NETCOREAPP3_0_OR_GREATER
        [Test]
        public void SystemTextJson_SerializeDeserialize_PreservesAllProperties()
        {
            var original = CreateTestJournalResponse();
            var json = System.Text.Json.JsonSerializer.Serialize(original);
            var deserialized = System.Text.Json.JsonSerializer.Deserialize<JournalResponse>(json);
            AssertJournalResponsesEqual(original, deserialized);
        }

        [Test]
        public void BothSerializers_ProduceSameOutput()
        {
            var item = CreateTestJournalResponse();
            var newtonsoftJson = JsonConvert.SerializeObject(item);
            var systemTextJson = System.Text.Json.JsonSerializer.Serialize(item);
            Assert.AreEqual(newtonsoftJson, systemTextJson);
        }
#endif

        [Test]
        public void EmptyChunk_SerializesCorrectly()
        {
            var response = new JournalResponse
            {
                Chunk = new List<byte>()
            };

            var json = JsonConvert.SerializeObject(response);
            var deserialized = JsonConvert.DeserializeObject<JournalResponse>(json);

            Assert.IsNotNull(deserialized.Chunk);
            Assert.AreEqual(0, deserialized.Chunk.Count);
        }

        private void AssertJournalResponsesEqual(JournalResponse expected, JournalResponse actual)
        {
            Assert.IsNotNull(actual);
            Assert.IsNotNull(actual.Chunk);
            Assert.AreEqual(expected.Chunk.Count, actual.Chunk.Count);
            CollectionAssert.AreEqual(expected.Chunk, actual.Chunk);
        }
    }
}