using System;
using fiskaltrust.Middleware.ifPOS.v2.Models;
using fiskaltrust.Middleware.Localization.v2.Models.ifPOS.v2.Cases;
using NUnit.Framework;
using Newtonsoft.Json;

#if NETSTANDARD2_1_TESTS
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
            // Arrange
            var original = CreateTestSignatureItem();

            // Act
            var json = JsonConvert.SerializeObject(original, Formatting.Indented);
            var deserialized = JsonConvert.DeserializeObject<SignatureItem>(json);

            // Assert
            Assert.AreEqual(original.ftSignatureItemId, deserialized.ftSignatureItemId);
            Assert.AreEqual(original.ftSignatureFormat, deserialized.ftSignatureFormat);
            Assert.AreEqual(original.ftSignatureType, deserialized.ftSignatureType);
            Assert.AreEqual(original.Caption, deserialized.Caption);
            Assert.AreEqual(original.Data, deserialized.Data);
        }

#if NETSTANDARD2_1_TESTS
        [Test]
        public void SystemTextJson_SerializeDeserialize_PreservesAllProperties()
        {
            // Arrange
            var original = CreateTestSignatureItem();
            var options = new JsonSerializerOptions 
            { 
                WriteIndented = true,
                Converters = { new System.Text.Json.Serialization.JsonStringEnumConverter() }
            };

            // Act
            var json = System.Text.Json.JsonSerializer.Serialize(original, options);
            var deserialized = System.Text.Json.JsonSerializer.Deserialize<SignatureItem>(json, options);

            // Assert
            Assert.AreEqual(original.ftSignatureItemId, deserialized.ftSignatureItemId);
            Assert.AreEqual(original.ftSignatureFormat, deserialized.ftSignatureFormat);
            Assert.AreEqual(original.ftSignatureType, deserialized.ftSignatureType);
            Assert.AreEqual(original.Caption, deserialized.Caption);
            Assert.AreEqual(original.Data, deserialized.Data);
        }

        [Test]
        public void BothSerializers_ProduceSameStructure()
        {
            // Arrange
            var item = CreateTestSignatureItem();
            var systemTextJsonOptions = new JsonSerializerOptions 
            { 
                WriteIndented = true,
                Converters = { new System.Text.Json.Serialization.JsonStringEnumConverter() }
            };

            // Act
            var newtonsoftJson = JsonConvert.SerializeObject(item, Formatting.Indented);
            var systemTextJson = System.Text.Json.JsonSerializer.Serialize(item, systemTextJsonOptions);

            var newtonsoftDoc = Newtonsoft.Json.Linq.JObject.Parse(newtonsoftJson);
            var systemTextDoc = System.Text.Json.JsonDocument.Parse(systemTextJson);

            // Assert
            foreach (var prop in newtonsoftDoc.Properties())
            {
                Assert.IsTrue(systemTextDoc.RootElement.TryGetProperty(prop.Name, out _), 
                    $"Property '{prop.Name}' not found in System.Text.Json output");
            }
        }
#endif

        [Test]
        public void NullableCaption_SerializesCorrectly()
        {
            // Arrange
            var item = new SignatureItem
            {
                ftSignatureFormat = 0,
                ftSignatureType = 0,
                Caption = null,
                Data = "TestData"
            };

            // Act
            var json = JsonConvert.SerializeObject(item);
            var deserialized = JsonConvert.DeserializeObject<SignatureItem>(json);

            // Assert
            Assert.IsNull(deserialized.Caption);
            Assert.AreEqual(item.Data, deserialized.Data);
        }
    }
}