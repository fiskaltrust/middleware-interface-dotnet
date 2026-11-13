using fiskaltrust.ifPOS.v2.Cases;
using FluentAssertions;
using NUnit.Framework;

namespace fiskaltrust.Middleware.Interface.Tests.v2.Cases
{
    [TestFixture]
    public class ReceiptCaseFlagsTests
    {
        [Test]
        public void GroupByPositionNumber_HasThePublishedValue()
        {
            ((ulong)ReceiptCaseFlags.GroupByPositionNumber).Should().Be(0x0000_0000_0800_0000UL);
        }
    }
}
