using System;
using fiskaltrust.ifPOS.v2;
using fiskaltrust.ifPOS.v2.Cases;
using NUnit.Framework;
using Newtonsoft.Json;

#if NETCOREAPP3_0_OR_GREATER
using System.Text.Json;
#endif

namespace fiskaltrust.Middleware.Interface.Tests.v2
{
    [TestFixture]
    public class SignatureItemSerializationTests
    {
        private SignatureItem CreateTestSignatureItem()
        {
            return new SignatureItem
            {
                ftSignatureItemId = Guid.Parse("12345678-1234-1234-1234-123456789012"),
                ftSignatureFormat = SignatureFormat.QRCode,
                ftSignatureType = SignatureType.Unknown,
                Caption = "Test Signature",
                Data = "BASE64ENCODEDDATA=="
            };
        }

        [Test]
        public void Newtonsoft_SerializeDeserialize_PreservesAllProperties()
        {
            var original = CreateTestSignatureItem();
            var json = JsonConvert.SerializeObject(original, Formatting.Indented);
            var deserialized = JsonConvert.DeserializeObject<SignatureItem>(json);
            AssertSignatureItemsEqual(original, deserialized);
        }

#if NETCOREAPP3_0_OR_GREATER
        [Test]
        public void SystemTextJson_SerializeDeserialize_PreservesAllProperties()
        {
            var original = CreateTestSignatureItem();
            var json = System.Text.Json.JsonSerializer.Serialize(original);
            var deserialized = System.Text.Json.JsonSerializer.Deserialize<SignatureItem>(json);
            AssertSignatureItemsEqual(original, deserialized);
        }

        [Test]
        public void BothSerializers_ProduceSameOutput()
        {
            // Arrange
            var item = CreateTestSignatureItem();

            // Act
            var newtonsoftJson = JsonConvert.SerializeObject(item);
            var systemTextJson = System.Text.Json.JsonSerializer.Serialize(item);

            // Assert
            Assert.AreEqual(newtonsoftJson, systemTextJson);
        }
#endif

        [Test]
        public void NullableCaption_SerializesCorrectly()
        {
            var item = new SignatureItem
            {
                ftSignatureFormat = 0,
                ftSignatureType = 0,
                Caption = null,
                Data = "TestData"
            };

            var json = JsonConvert.SerializeObject(item);
            var deserialized = JsonConvert.DeserializeObject<SignatureItem>(json);

            Assert.IsNull(deserialized.Caption);
            Assert.AreEqual(item.Data, deserialized.Data);
        }

        [Test]
        public void NullableProperties_SerializeCorrectly()
        {
            var item = new SignatureItem
            {
                ftSignatureFormat = SignatureFormat.QRCode,
                ftSignatureType = SignatureType.Unknown,
                Data = "TestData"
            };

            var json = JsonConvert.SerializeObject(item);
            var deserialized = JsonConvert.DeserializeObject<SignatureItem>(json);

            Assert.IsNull(deserialized.ftSignatureItemId);
            Assert.IsNull(deserialized.Caption);
            Assert.AreEqual(item.Data, deserialized.Data);
        }

        private void AssertSignatureItemsEqual(SignatureItem expected, SignatureItem actual)
        {
            Assert.AreEqual(expected.ftSignatureItemId, actual.ftSignatureItemId);
            Assert.AreEqual(expected.ftSignatureFormat, actual.ftSignatureFormat);
            Assert.AreEqual(expected.ftSignatureType, actual.ftSignatureType);
            Assert.AreEqual(expected.Caption, actual.Caption);
            Assert.AreEqual(expected.Data, actual.Data);
        }
    }
}