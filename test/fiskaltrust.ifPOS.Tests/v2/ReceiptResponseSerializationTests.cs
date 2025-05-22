using System;
using System.Collections.Generic;
using System.Globalization;
using fiskaltrust.Middleware.ifPOS.v2.Models;
using fiskaltrust.Middleware.Localization.v2.Models.ifPOS.v2.Cases;
using NUnit.Framework;
using Newtonsoft.Json;

#if NETSTANDARD2_1_TESTS
using System.Text.Json;
using System.Text.Json.Serialization;
#endif

namespace fiskaltrust.Middleware.Interface.Tests.v2
{
    [TestFixture]
    public class ReceiptResponseSerializationTests
    {
        private ReceiptResponse CreateTestReceiptResponse()
        {
            return new ReceiptResponse
            {
                ftQueueID = Guid.Parse("12345678-1234-1234-1234-123456789012"),
                ftQueueItemID = Guid.Parse("87654321-4321-4321-4321-210987654321"),
                ftQueueRow = 12345,
                ftCashBoxIdentification = "CB001",
                ftCashBoxID = Guid.Parse("11111111-2222-3333-4444-555555555555"),
                cbTerminalID = "TERM001",
                cbReceiptReference = "RECEIPT-2024-001",
                ftReceiptIdentification = "FT-RECEIPT-001",
                ftReceiptMoment = new DateTime(2024, 1, 15, 10, 30, 0, DateTimeKind.Utc),
                ftReceiptHeader = new List<string> { "Header Line 1", "Header Line 2" },
                ftChargeItems = new List<ChargeItem>
                {
                    new ChargeItem
                    {
                        Description = "Product 1",
                        Amount = 10.99m,
                        VATRate = 19.0m
                    }
                },
                ftChargeLines = new List<string> { "Charge Line 1", "Charge Line 2" },
                ftPayItems = new List<PayItem>
                {
                    new PayItem
                    {
                        Description = "Cash",
                        Amount = 10.99m
                    }
                },
                ftPayLines = new List<string> { "Pay Line 1", "Pay Line 2" },
                ftSignatures = new List<SignatureItem>
                {
                    new SignatureItem
                    {
                        ftSignatureFormat = SignatureFormat.QRCode,
                        ftSignatureType = SignatureType.Unknown,
                        Caption = "Signature",
                        Data = "SIGNATURE_DATA"
                    }
                },
                ftReceiptFooter = new List<string> { "Footer Line 1", "Footer Line 2" },
                ftState = State.Success,
                ftStateData = new { Status = "Success" }
            };
        }

        [Test]
        public void Newtonsoft_SerializeDeserialize_PreservesAllProperties()
        {
            var original = CreateTestReceiptResponse();
            var json = JsonConvert.SerializeObject(original, Formatting.Indented);
            var deserialized = JsonConvert.DeserializeObject<ReceiptResponse>(json);
            AssertReceiptResponsesEqual(original, deserialized);
        }

#if NETSTANDARD2_1_TESTS
        [Test]
        public void SystemTextJson_SerializeDeserialize_PreservesAllProperties()
        {
            var original = CreateTestReceiptResponse();
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                PropertyNamingPolicy = null,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                Converters = { new JsonStringEnumConverter() }
            };
            var json = System.Text.Json.JsonSerializer.Serialize(original, options);
            var deserialized = System.Text.Json.JsonSerializer.Deserialize<ReceiptResponse>(json, options);
            AssertReceiptResponsesEqual(original, deserialized);
        }

        [Test]
        public void BothSerializers_ProduceSameOutput()
        {
            // Arrange
            var item = CreateTestReceiptResponse();
            
            // JSON settings aligned for consistent output between Newtonsoft.Json and System.Text.Json,
            // matching formatting, dates, numbers, enums, null handling, and reference behavior,
            // based on Microsoft's migration guide.

            var newtonsoftSettings = new JsonSerializerSettings
            {
                Formatting = Formatting.None, 
                DateFormatHandling = DateFormatHandling.IsoDateFormat, 
                DateTimeZoneHandling = DateTimeZoneHandling.Utc, 
                NullValueHandling = NullValueHandling.Ignore, 
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore, 
                PreserveReferencesHandling = PreserveReferencesHandling.None, 
                FloatFormatHandling = FloatFormatHandling.DefaultValue, 
                Culture = CultureInfo.InvariantCulture, 
                Converters = { 
                    new Newtonsoft.Json.Converters.StringEnumConverter(namingStrategy: null), 
                    new NewtonsoftDateTimeConverter(), 
                    new GlobalNumberConverter() 
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
                    new JsonStringEnumConverter(namingPolicy: null), 
                    new SystemTextJsonConverters.DateTimeConverter(), 
                    new SystemTextJsonConverters.DecimalConverter() 
                }
            };

            // Act
            var newtonsoftJson = JsonConvert.SerializeObject(item, newtonsoftSettings);
            var systemTextJson = System.Text.Json.JsonSerializer.Serialize(item, systemTextJsonOptions);

            // Assert
            Assert.AreEqual(newtonsoftJson, systemTextJson);
        }

        public class GlobalNumberConverter : Newtonsoft.Json.JsonConverter
        {
            public override bool CanConvert(Type objectType)
            {
                return objectType == typeof(int) || objectType == typeof(int?) ||
                       objectType == typeof(long) || objectType == typeof(long?) ||
                       objectType == typeof(decimal) || objectType == typeof(decimal?) ||
                       objectType == typeof(double) || objectType == typeof(double?) ||
                       objectType == typeof(float) || objectType == typeof(float?);
            }

            public override void WriteJson(JsonWriter writer, object value, Newtonsoft.Json.JsonSerializer serializer)
            {
                if (value == null)
                {
                    writer.WriteNull();
                    return;
                }

                if (value is int || value is long)
                {
                    writer.WriteValue(value);
                }
                else if (value is decimal decimalValue)
                {
                    if (decimalValue == Math.Floor(decimalValue))
                    {
                        writer.WriteValue((long)decimalValue);
                    }
                    else
                    {
                        writer.WriteValue(decimalValue);
                    }
                }
                else if (value is double doubleValue)
                {
                    if (doubleValue == Math.Floor(doubleValue))
                    {
                        writer.WriteValue((long)doubleValue);
                    }
                    else
                    {
                        writer.WriteValue(doubleValue);
                    }
                }
                else if (value is float floatValue)
                {
                    if (floatValue == Math.Floor(floatValue))
                    {
                        writer.WriteValue((long)floatValue);
                    }
                    else
                    {
                        writer.WriteValue(floatValue);
                    }
                }
                else
                {
                    writer.WriteValue(value);
                }
            }

            public override object ReadJson(JsonReader reader, Type objectType, object existingValue, Newtonsoft.Json.JsonSerializer serializer)
            {
                if (reader.Value == null) return null;
                
                if (objectType == typeof(int) || objectType == typeof(int?))
                    return Convert.ToInt32(reader.Value);
                if (objectType == typeof(long) || objectType == typeof(long?))
                    return Convert.ToInt64(reader.Value);
                if (objectType == typeof(decimal) || objectType == typeof(decimal?))
                    return Convert.ToDecimal(reader.Value);
                if (objectType == typeof(double) || objectType == typeof(double?))
                    return Convert.ToDouble(reader.Value);
                if (objectType == typeof(float) || objectType == typeof(float?))
                    return Convert.ToSingle(reader.Value);
                    
                return reader.Value;
            }
        }

        public class NewtonsoftDateTimeConverter : Newtonsoft.Json.JsonConverter<DateTime>
        {
            public override void WriteJson(JsonWriter writer, DateTime value, Newtonsoft.Json.JsonSerializer serializer)
            {
                writer.WriteValue(value.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ssZ", CultureInfo.InvariantCulture));
            }

            public override DateTime ReadJson(JsonReader reader, Type objectType, DateTime existingValue, bool hasExistingValue, Newtonsoft.Json.JsonSerializer serializer)
            {
                if (reader.Value == null) return default;
                return DateTime.Parse(reader.Value.ToString(), CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal);
            }
        }

        private static class SystemTextJsonConverters
        {
            public class DateTimeConverter : System.Text.Json.Serialization.JsonConverter<DateTime>
            {
                public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
                {
                    string dateStr = reader.GetString();
                    return DateTime.Parse(dateStr, CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal);
                }

                public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
                {
                    writer.WriteStringValue(value.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ssZ", CultureInfo.InvariantCulture));
                }
            }

            public class DecimalConverter : System.Text.Json.Serialization.JsonConverter<decimal>
            {
                public override decimal Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
                {
                    if (reader.TokenType == JsonTokenType.String)
                    {
                        return decimal.Parse(reader.GetString(), CultureInfo.InvariantCulture);
                    }
                    return reader.GetDecimal();
                }

                public override void Write(Utf8JsonWriter writer, decimal value, JsonSerializerOptions options)
                {
                    if (value == Math.Floor(value))
                    {
                        writer.WriteNumberValue((long)value);
                    }
                    else
                    {
                        writer.WriteNumberValue(value);
                    }
                }
            }
        }
#endif

        [Test]
        public void EmptyCollections_SerializeCorrectly()
        {
            var response = new ReceiptResponse
            {
                ftQueueID = Guid.NewGuid(),
                ftQueueItemID = Guid.NewGuid(),
                ftQueueRow = 1,
                ftCashBoxIdentification = "CB001",
                ftReceiptIdentification = "RECEIPT001",
                ftReceiptMoment = DateTime.UtcNow,
                ftReceiptHeader = new List<string>(),
                ftChargeItems = new List<ChargeItem>(),
                ftChargeLines = new List<string>(),
                ftPayItems = new List<PayItem>(),
                ftPayLines = new List<string>(),
                ftSignatures = new List<SignatureItem>(),
                ftReceiptFooter = new List<string>(),
                ftState = State.Success
            };

            var json = JsonConvert.SerializeObject(response);
            var deserialized = JsonConvert.DeserializeObject<ReceiptResponse>(json);

            Assert.IsNotNull(deserialized.ftReceiptHeader);
            Assert.AreEqual(0, deserialized.ftReceiptHeader.Count);
            Assert.IsNotNull(deserialized.ftChargeItems);
            Assert.AreEqual(0, deserialized.ftChargeItems.Count);
            Assert.IsNotNull(deserialized.ftSignatures);
            Assert.AreEqual(0, deserialized.ftSignatures.Count);
        }

        [Test]
        public void NullableProperties_SerializeCorrectly()
        {
            var response = new ReceiptResponse
            {
                ftQueueID = Guid.NewGuid(),
                ftQueueItemID = Guid.NewGuid(),
                ftQueueRow = 1,
                ftCashBoxIdentification = "CB001",
                ftReceiptIdentification = "RECEIPT001",
                ftReceiptMoment = DateTime.UtcNow,
                ftReceiptHeader = new List<string>(),
                ftChargeItems = new List<ChargeItem>(),
                ftChargeLines = new List<string>(),
                ftPayItems = new List<PayItem>(),
                ftPayLines = new List<string>(),
                ftSignatures = new List<SignatureItem>(),
                ftReceiptFooter = new List<string>(),
                ftState = State.Success
            };

            var json = JsonConvert.SerializeObject(response);
            var deserialized = JsonConvert.DeserializeObject<ReceiptResponse>(json);

            Assert.IsNull(deserialized.ftCashBoxID);
            Assert.IsNull(deserialized.cbTerminalID);
            Assert.IsNull(deserialized.cbReceiptReference);
            Assert.IsNull(deserialized.ftStateData);
        }

        private void AssertReceiptResponsesEqual(ReceiptResponse expected, ReceiptResponse actual)
        {
            Assert.AreEqual(expected.ftQueueID, actual.ftQueueID);
            Assert.AreEqual(expected.ftQueueItemID, actual.ftQueueItemID);
            Assert.AreEqual(expected.ftQueueRow, actual.ftQueueRow);
            Assert.AreEqual(expected.ftCashBoxIdentification, actual.ftCashBoxIdentification);
            Assert.AreEqual(expected.ftCashBoxID, actual.ftCashBoxID);
            Assert.AreEqual(expected.cbTerminalID, actual.cbTerminalID);
            Assert.AreEqual(expected.cbReceiptReference, actual.cbReceiptReference);
            Assert.AreEqual(expected.ftReceiptIdentification, actual.ftReceiptIdentification);
            Assert.AreEqual(expected.ftReceiptMoment, actual.ftReceiptMoment);
            Assert.AreEqual(expected.ftReceiptHeader.Count, actual.ftReceiptHeader.Count);
            Assert.AreEqual(expected.ftChargeItems.Count, actual.ftChargeItems.Count);
            Assert.AreEqual(expected.ftChargeLines.Count, actual.ftChargeLines.Count);
            Assert.AreEqual(expected.ftPayItems.Count, actual.ftPayItems.Count);
            Assert.AreEqual(expected.ftPayLines.Count, actual.ftPayLines.Count);
            Assert.AreEqual(expected.ftSignatures.Count, actual.ftSignatures.Count);
            Assert.AreEqual(expected.ftReceiptFooter.Count, actual.ftReceiptFooter.Count);
            Assert.AreEqual(expected.ftState, actual.ftState);
        }
    }
}