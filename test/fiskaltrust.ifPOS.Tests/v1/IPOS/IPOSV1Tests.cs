using System;
using FluentAssertions;
using fiskaltrust.ifPOS.v1;
using NUnit.Framework;

namespace fiskaltrust.ifPOS.Tests.v1.IPOS
{
    public abstract class IPOSV1Tests
    {
        protected abstract void StartHost();
        protected abstract ifPOS.v1.IPOS CreateClient();

        [OneTimeSetUp]
        public void BaseSetUp()
        {
            StartHost();
        }

        [Test]
        public void SignAsync_ShouldReturnSameQueueId()
        {
            var client = CreateClient();
            var queueId = Guid.NewGuid().ToString();
            var response = client.SignAsync(new ReceiptRequest
            {
                ftQueueID = queueId
            }).Result;
            response.ftQueueID.Should().Be(queueId);
        }

        [Test]
        public void EchoAsync_ShouldReturnSameMessage()
        {
            var client = CreateClient();
            var inMessage = "Hello World!";
            var outMessage = client.EchoAsync(new EchoRequest
            {
                Message = inMessage
            }).Result;
            outMessage.Message.Should().Be(inMessage);
        }
    }
}