using System;
using fiskaltrust.Middleware.ifPOS.v2.Models;
using fiskaltrust.Middleware.Localization.v2.Models.ifPOS.v2.Cases;
using NUnit.Framework;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

#if NETSTANDARD2_1_TESTS
using System.Text.Json;
using System.Text.Json.Serialization;
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
            AssertSignatureItemsEqual(original, deserialized);
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
                PropertyNamingPolicy = null,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                Converters = { new JsonStringEnumConverter() }
            };

            // Act
            var json = System.Text.Json.JsonSerializer.Serialize(original, options);
            var deserialized = System.Text.Json.JsonSerializer.Deserialize<SignatureItem>(json, options);

            // Assert
            AssertSignatureItemsEqual(original, deserialized);
        }

        [Test]
        public void BothSerializers_ProduceSameOutput()
        {
            // Arrange
            var item = CreateTestSignatureItem();

            var newtonsoftSettings = new JsonSerializerSettings
            {
                Formatting = Formatting.None,
                DateFormatHandling = DateFormatHandling.IsoDateFormat,
                DateTimeZoneHandling = DateTimeZoneHandling.Utc,
                NullValueHandling = NullValueHandling.Ignore,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                PreserveReferencesHandling = PreserveReferencesHandling.None,
                Converters = { 
                    new Newtonsoft.Json.Converters.StringEnumConverter(namingStrategy: null)
                }
            };

            var systemTextJsonOptions = new JsonSerializerOptions
            {
                WriteIndented = false,
                PropertyNamingPolicy = null,
                PropertyNameCaseInsensitive = true, 
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                NumberHandling = JsonNumberHandling.AllowReadingFromString,
                ReferenceHandler = ReferenceHandler.IgnoreCycles,
                ReadCommentHandling = JsonCommentHandling.Skip,
                AllowTrailingCommas = true,
                MaxDepth = 64,
                Converters = {
                    new JsonStringEnumConverter(namingPolicy: null)
                }
            };

            // Act
            var newtonsoftJson = JsonConvert.SerializeObject(item, newtonsoftSettings);
            var systemTextJson = System.Text.Json.JsonSerializer.Serialize(item, systemTextJsonOptions);
            
            var fromNewtonsoft = JsonConvert.DeserializeObject<SignatureItem>(newtonsoftJson);
            var fromSystemText = JsonConvert.DeserializeObject<SignatureItem>(systemTextJson);
            
            AssertSignatureItemsEqual(fromNewtonsoft, fromSystemText);

            var newtonsoftDoc = JObject.Parse(newtonsoftJson);
            var systemTextDoc = JsonDocument.Parse(systemTextJson);

            foreach (var prop in newtonsoftDoc.Properties())
            {
                Assert.IsTrue(systemTextDoc.RootElement.TryGetProperty(prop.Name, out _),
                    $"Property '{prop.Name}' not found in System.Text.Json output");
            }

            foreach (var prop in newtonsoftDoc.Properties())
            {
                if (systemTextDoc.RootElement.TryGetProperty(prop.Name, out var systemTextValue))
                {
                    var newtonsoftValue = newtonsoftDoc[prop.Name];
                    
                    if (newtonsoftValue.Type == JTokenType.Object || 
                        newtonsoftValue.Type == JTokenType.Array)
                    {
                        Assert.IsTrue(
                            systemTextValue.ValueKind == JsonValueKind.Object || 
                            systemTextValue.ValueKind == JsonValueKind.Array,
                            $"Property '{prop.Name}' should be an object or array in both serializations");
                        continue;
                    }

                    if (prop.Name.EndsWith("Format") || prop.Name.EndsWith("Type"))
                    {
                        string newtonStr = newtonsoftValue.ToString().ToLowerInvariant();
                        string sysStr = GetJsonElementValueAsString(systemTextValue).ToLowerInvariant();
                        Assert.AreEqual(newtonStr, sysStr,
                            $"Property '{prop.Name}' has different enum values");
                        continue;
                    }

                    if (newtonsoftValue.Type == JTokenType.Null)
                    {
                        Assert.AreEqual(JsonValueKind.Null, systemTextValue.ValueKind,
                            $"Property '{prop.Name}' is null in Newtonsoft but not in System.Text.Json");
                        continue;
                    }

                    Assert.AreEqual(
                        newtonsoftValue.ToString(), 
                        GetJsonElementValueAsString(systemTextValue),
                        $"Property '{prop.Name}' has different values in the two serializations");
                }
            }
        }

        private string GetJsonElementValueAsString(JsonElement element)
        {
            return element.ValueKind switch
            {
                JsonValueKind.String => element.GetString(),
                JsonValueKind.Number => element.GetRawText(),
                JsonValueKind.True => "true",
                JsonValueKind.False => "false",
                JsonValueKind.Null => "null",
                _ => element.ToString(),
            };
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