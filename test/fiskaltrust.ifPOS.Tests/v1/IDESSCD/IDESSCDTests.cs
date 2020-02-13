using FluentAssertions;
using fiskaltrust.ifPOS.v1.de;
using NUnit.Framework;

namespace fiskaltrust.ifPOS.Tests.v1.IDESSCD
{
    public abstract class IDESSCDTests
    {
        protected abstract void StartHost();
        protected abstract void StopHost();
        protected abstract ifPOS.v1.de.IDESSCD CreateClient();

        [OneTimeSetUp]
        public void BaseSetUp() => StartHost();

        [OneTimeTearDown]
        public void BaseTearDown() => StopHost();

        [Test]
        public void ExportDataAsync_ShouldReturn()
        {
            var client = CreateClient();
            var result = client.ExportDataAsync().Result;
            result.Should().NotBeNull();
        }

        [Test]
        public void FinishTransactionExportDataAsync_ShouldReturn()
        {
            var client = CreateClient();
            var result = client.FinishTransactionExportDataAsync(new FinishTransactionRequest()).Result;
            result.Should().NotBeNull();
        }

        [Test]
        public void GetTseInfoAsync_ShouldReturn()
        {
            var client = CreateClient();
            var result = client.GetTseInfoAsync().Result;
            result.Should().NotBeNull();
        }

        [Test]
        public void StartTransactionExportDataAsync_ShouldReturn()
        {
            var client = CreateClient();
            var result = client.StartTransactionExportDataAsync(new StartTransactionRequest()).Result;
            result.Should().NotBeNull();
        }

        [Test]
        public void UpdateTransactionExportDataAsync_ShouldReturn()
        {
            var client = CreateClient();
            var result = client.UpdateTransactionExportDataAsync(new UpdateTransactionRequest()).Result;
            result.Should().NotBeNull();
        }

        [Test]
        public void SetTseStateAsync_ShouldReturn()
        {
            var client = CreateClient();
            var result = client.SetTseStateAsync(new TseState()).Result;
            result.Should().NotBeNull();
        }
    }
}