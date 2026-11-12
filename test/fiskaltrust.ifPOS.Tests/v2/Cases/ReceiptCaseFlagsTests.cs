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

#pragma warning disable CS0618 // EnforceServiceOperations is obsolete; this test pins it as an alias.
        [Test]
        public void EnforceServiceOperations_IsAnAliasOfGroupByPositionNumber()
        {
            ((ulong)ReceiptCaseFlags.EnforceServiceOperations).Should().Be((ulong)ReceiptCaseFlags.GroupByPositionNumber);
            ((ReceiptCase)0x4652_2000_0000_0001UL).WithFlag(ReceiptCaseFlags.EnforceServiceOperations)
                .IsFlag(ReceiptCaseFlags.GroupByPositionNumber).Should().BeTrue();
        }
#pragma warning restore CS0618
    }
}
