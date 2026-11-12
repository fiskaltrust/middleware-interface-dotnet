using fiskaltrust.ifPOS.v2.Cases;
using FluentAssertions;
using NUnit.Framework;

namespace fiskaltrust.Middleware.Interface.Tests.v2.Cases
{
    [TestFixture]
    public class ReceiptCaseFlagsTests
    {
        [TestCase(ReceiptCaseFlags.GroupByPositionNumber)]
        [TestCase(ReceiptCaseFlags.EnforceServiceOperations)]
        public void Bit0800_HasThePublishedValue(ReceiptCaseFlags flag)
        {
            ((ulong)flag).Should().Be(0x0000_0000_0800_0000UL);
        }

        [Test]
        public void GroupByPositionNumberAndEnforceServiceOperations_ShareOneBit()
        {
            ((ulong)ReceiptCaseFlags.EnforceServiceOperations).Should().Be((ulong)ReceiptCaseFlags.GroupByPositionNumber);
            ((ReceiptCase)0x4652_2000_0000_0001UL).WithFlag(ReceiptCaseFlags.EnforceServiceOperations)
                .IsFlag(ReceiptCaseFlags.GroupByPositionNumber).Should().BeTrue();
        }
    }
}
