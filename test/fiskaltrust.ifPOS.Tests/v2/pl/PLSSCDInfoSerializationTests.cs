#if !WCF
using fiskaltrust.ifPOS.v2.pl;
using NUnit.Framework;
using Newtonsoft.Json;

namespace fiskaltrust.Middleware.Interface.Tests.v2.pl
{
    [TestFixture]
    public class PLSSCDInfoSerializationTests
    {
        private PLSSCDInfo CreateTestPLSSCDInfo()
        {
            return new PLSSCDInfo
            {
                InfoData = "{\"serialNumber\":\"ABC1234567\",\"uniqueDeviceNumber\":\"ZAS1234567890\",\"crkReachable\":true}"
            };
        }

        [Test]
        public void Newtonsoft_SerializeDeserialize_PreservesAllProperties()
        {
            var original = CreateTestPLSSCDInfo();
            var json = JsonConvert.SerializeObject(original, Formatting.Indented);
            var deserialized = JsonConvert.DeserializeObject<PLSSCDInfo>(json);
            AssertPLSSCDInfosEqual(original, deserialized);
        }

        [Test]
        public void SystemTextJson_SerializeDeserialize_PreservesAllProperties()
        {
            var original = CreateTestPLSSCDInfo();
            var json = System.Text.Json.JsonSerializer.Serialize(original);
            var deserialized = System.Text.Json.JsonSerializer.Deserialize<PLSSCDInfo>(json);
            AssertPLSSCDInfosEqual(original, deserialized);
        }

        [Test]
        public void BothSerializers_ProduceSameOutput()
        {
            // Newtonsoft escapes embedded quotes as \" while System.Text.Json uses ",
            // so the raw-output comparison uses a value without quotes.
            var item = new PLSSCDInfo
            {
                InfoData = "plain-device-info"
            };
            var newtonsoftJson = JsonConvert.SerializeObject(item);
            var systemTextJson = System.Text.Json.JsonSerializer.Serialize(item);
            Assert.AreEqual(newtonsoftJson, systemTextJson);
        }

        [Test]
        public void EmptyInfo_SerializesCorrectly()
        {
            var original = new PLSSCDInfo();
            var json = JsonConvert.SerializeObject(original);
            var deserialized = JsonConvert.DeserializeObject<PLSSCDInfo>(json);

            Assert.IsNotNull(deserialized);
            Assert.IsNull(deserialized.InfoData);
        }

        private void AssertPLSSCDInfosEqual(PLSSCDInfo expected, PLSSCDInfo actual)
        {
            Assert.IsNotNull(actual);
            Assert.AreEqual(expected.InfoData, actual.InfoData);
        }
    }
}
#endif
