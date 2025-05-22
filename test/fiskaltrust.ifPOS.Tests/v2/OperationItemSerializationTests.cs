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
                Request = "test-request-data",
                Response = "success-response-data",
                ResponseCode = 200,
                LastState = "Completed",
                TimeStamp = new DateTimeOffset(2024, 1, 15, 10, 30, 0, TimeSpan.Zero),
                ftOperationItemMoment = new DateTimeOffset(2024, 1, 15, 10, 30, 0, TimeSpan.Zero)
            };
        }

        [Test]
        public void Newtonsoft_SerializeDeserialize_PreservesAllProperties()
        {
            var original = CreateTestOperationItem();
            var json = JsonConvert.SerializeObject(original, Formatting.Indented);
            var deserialized = JsonConvert.DeserializeObject<OperationItem>(json);
            AssertOperationItemsEqual(original, deserialized);
        }

#if NETSTANDARD2_1_TESTS
        [Test]
        public void SystemTextJson_SerializeDeserialize_PreservesAllProperties()
        {
            var original = CreateTestOperationItem();
            var json = System.Text.Json.JsonSerializer.Serialize(original);
            var deserialized = System.Text.Json.JsonSerializer.Deserialize<OperationItem>(json);
            AssertOperationItemsEqual(original, deserialized);
        }

        [Test]
        public void BothSerializers_ProduceSameOutput()
        {
            // Arrange
            var item = CreateTestOperationItem();

            // Act
            var newtonsoftJson = JsonConvert.SerializeObject(item);
            var systemTextJson = System.Text.Json.JsonSerializer.Serialize(item);

            // Assert
            Assert.AreEqual(newtonsoftJson, systemTextJson);
        }
#endif

        [Test]
        public void Clone_CreatesIdenticalCopy()
        {
            var original = CreateTestOperationItem();
            var cloned = (OperationItem)original.Clone();

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
            var item = new OperationItem();
            var json = JsonConvert.SerializeObject(item);
            var deserialized = JsonConvert.DeserializeObject<OperationItem>(json);

            Assert.AreEqual(string.Empty, deserialized.Method);
            Assert.AreEqual(string.Empty, deserialized.Path);
            Assert.AreEqual(string.Empty, deserialized.Request);
            Assert.AreEqual(string.Empty, deserialized.LastState);
            Assert.IsNotNull(deserialized.RequestHeaders);
            Assert.AreEqual(0, deserialized.RequestHeaders.Count);
        }

        private void AssertOperationItemsEqual(OperationItem expected, OperationItem actual)
        {
            Assert.AreEqual(expected.cbOperationItemID, actual.cbOperationItemID);
            Assert.AreEqual(expected.ftQueueID, actual.ftQueueID);
            Assert.AreEqual(expected.ftPosSystemID, actual.ftPosSystemID);
            Assert.AreEqual(expected.cbTerminalID, actual.cbTerminalID);
            Assert.AreEqual(expected.Method, actual.Method);
            Assert.AreEqual(expected.Path, actual.Path);
            Assert.AreEqual(expected.RequestHeaders.Count, actual.RequestHeaders.Count);
            Assert.AreEqual(expected.Request, actual.Request);
            Assert.AreEqual(expected.Response, actual.Response);
            Assert.AreEqual(expected.ResponseCode, actual.ResponseCode);
            Assert.AreEqual(expected.LastState, actual.LastState);
            Assert.AreEqual(expected.TimeStamp, actual.TimeStamp);
            Assert.AreEqual(expected.ftOperationItemMoment, actual.ftOperationItemMoment);
        }
    }
}