using System;
using System.Collections.Generic;
using fiskaltrust.Middleware.ifPOS.v2.Models;
using NUnit.Framework;
using Newtonsoft.Json;

#if NETSTANDARD2_1_TESTS
using System.Text.Json;
#endif

namespace fiskaltrust.Middleware.Interface.Tests.v2
{
    [TestFixture]
    public class OperationItemSerializationTests
    {
        private OperationItem CreateTestOperationItem()
        {
            return new OperationItem
            {
                cbOperationItemID = Guid.Parse("12345678-1234-1234-1234-123456789012"),
                ftQueueID = Guid.Parse("aaaaaaaa-bbbb-cccc-dddd-eeeeeeeeeeee"),
                ftPosSystemID = Guid.Parse("11111111-2222-3333-4444-555555555555"),
                cbTerminalID = "TERM001",
                Method = "POST",
                Path = "/api/receipt",
                RequestHeaders = new Dictionary<string, string[]>
                {
                    { "Content-Type", new[] { "application/json" } },
                    { "Accept", new[] { "application/json" } }
                },
                Request = "{\"test\":\"data\"}",
                Response = "{\"status\":\"success\"}",
                ResponseCode = 200,
                LastState = "Completed",
                TimeStamp = new DateTimeOffset(2024, 1, 15, 10, 30, 0, TimeSpan.Zero),
                ftOperationItemMoment = new DateTimeOffset(2024, 1, 15, 10, 30, 0, TimeSpan.Zero)
            };
        }

        [Test]
        public void Newtonsoft_SerializeDeserialize_PreservesAllProperties()
        {
            // Arrange
            var original = CreateTestOperationItem();

            // Act
            var json = JsonConvert.SerializeObject(original, Formatting.Indented);
            var deserialized = JsonConvert.DeserializeObject<OperationItem>(json);

            // Assert
            Assert.AreEqual(original.cbOperationItemID, deserialized.cbOperationItemID);
            Assert.AreEqual(original.ftQueueID, deserialized.ftQueueID);
            Assert.AreEqual(original.ftPosSystemID, deserialized.ftPosSystemID);
            Assert.AreEqual(original.cbTerminalID, deserialized.cbTerminalID);
            Assert.AreEqual(original.Method, deserialized.Method);
            Assert.AreEqual(original.Path, deserialized.Path);
            Assert.AreEqual(original.RequestHeaders.Count, deserialized.RequestHeaders.Count);
            Assert.AreEqual(original.Request, deserialized.Request);
            Assert.AreEqual(original.Response, deserialized.Response);
            Assert.AreEqual(original.ResponseCode, deserialized.ResponseCode);
            Assert.AreEqual(original.LastState, deserialized.LastState);
            Assert.AreEqual(original.TimeStamp, deserialized.TimeStamp);
            Assert.AreEqual(original.ftOperationItemMoment, deserialized.ftOperationItemMoment);
        }

#if NETSTANDARD2_1_TESTS
        [Test]
        public void SystemTextJson_SerializeDeserialize_PreservesAllProperties()
        {
            // Arrange
            var original = CreateTestOperationItem();
            var options = new JsonSerializerOptions { WriteIndented = true };

            // Act
            var json = System.Text.Json.JsonSerializer.Serialize(original, options);
            var deserialized = System.Text.Json.JsonSerializer.Deserialize<OperationItem>(json, options);

            // Assert
            Assert.AreEqual(original.cbOperationItemID, deserialized.cbOperationItemID);
            Assert.AreEqual(original.ftQueueID, deserialized.ftQueueID);
            Assert.AreEqual(original.ftPosSystemID, deserialized.ftPosSystemID);
            Assert.AreEqual(original.cbTerminalID, deserialized.cbTerminalID);
            Assert.AreEqual(original.Method, deserialized.Method);
            Assert.AreEqual(original.Path, deserialized.Path);
            Assert.AreEqual(original.RequestHeaders.Count, deserialized.RequestHeaders.Count);
            Assert.AreEqual(original.Request, deserialized.Request);
            Assert.AreEqual(original.Response, deserialized.Response);
            Assert.AreEqual(original.ResponseCode, deserialized.ResponseCode);
            Assert.AreEqual(original.LastState, deserialized.LastState);
            Assert.AreEqual(original.TimeStamp, deserialized.TimeStamp);
            Assert.AreEqual(original.ftOperationItemMoment, deserialized.ftOperationItemMoment);
        }

        [Test]
        public void BothSerializers_ProduceSameStructure()
        {
            // Arrange
            var item = CreateTestOperationItem();
            var systemTextJsonOptions = new JsonSerializerOptions { WriteIndented = true };

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
        public void Clone_CreatesIdenticalCopy()
        {
            // Arrange
            var original = CreateTestOperationItem();

            // Act
            var cloned = (OperationItem)original.Clone();

            // Assert
            Assert.AreEqual(original.cbOperationItemID, cloned.cbOperationItemID);
            Assert.AreEqual(original.ftQueueID, cloned.ftQueueID);
            Assert.AreEqual(original.Method, cloned.Method);
            Assert.AreEqual(original.Path, cloned.Path);
            Assert.AreEqual(original.Request, cloned.Request);
            Assert.AreEqual(original.Response, cloned.Response);
        }

        [Test]
        public void DefaultValues_SerializeCorrectly()
        {
            // Arrange
            var item = new OperationItem();

            // Act
            var json = JsonConvert.SerializeObject(item);
            var deserialized = JsonConvert.DeserializeObject<OperationItem>(json);

            // Assert
            Assert.AreEqual(string.Empty, deserialized.Method);
            Assert.AreEqual(string.Empty, deserialized.Path);
            Assert.AreEqual(string.Empty, deserialized.Request);
            Assert.AreEqual(string.Empty, deserialized.LastState);
            Assert.IsNotNull(deserialized.RequestHeaders);
            Assert.AreEqual(0, deserialized.RequestHeaders.Count);
        }
    }
}