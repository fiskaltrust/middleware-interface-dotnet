using fiskaltrust.ifPOS.v2;
using fiskaltrust.ifPOS.v2.es;
using FluentAssertions;
using NUnit.Framework;

namespace fiskaltrust.Middleware.Interface.Client.Tests.IESSSCD
{
    public abstract class IESSSCDTests
    {
        protected abstract void StartHost();
        protected abstract void StopHost();
        protected abstract ifPOS.v2.es.IESSSCD CreateClient();

        [OneTimeSetUp]
        public void BaseSetUp() => StartHost();

        [OneTimeTearDown]
        public void BaseTearDown() => StopHost();

        [Test]
        public void EchoAsync_ShouldReturn()
        {
            var client = CreateClient();
            var result = client.EchoAsync(new EchoRequest
            {
                Message = "test"
            }).Result;
            result.Should().NotBeNull();
        }

        [Test]
        public void GetInfoAsync_ShouldReturn()
        {
            var client = CreateClient();
            var result = client.GetInfoAsync().Result;
            result.Should().NotBeNull();
        }

        [Test]
        public void ProcessReceiptAsync_ShouldReturn()
        {
            var client = CreateClient();
            var result = client.ProcessReceiptAsync(new ProcessRequest { }).Result;
            result.Should().NotBeNull();
            result.ReceiptResponse.Should().NotBeNull();
            result.ReceiptResponse.ftCashBoxID.ToString().Should().NotBeNullOrEmpty();
        }
    }
}