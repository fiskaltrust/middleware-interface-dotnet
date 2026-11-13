using System;
using System.Linq;
using fiskaltrust.ifPOS.v2.Cases;
using FluentAssertions;
using NUnit.Framework;

namespace fiskaltrust.Middleware.Interface.Tests.v2.Cases
{
    [TestFixture]
    public class StateFlagsTests
    {
        [TestCase(StateFlags.SecurityMechanismDeactivated, 0x0000_0000_0000_0001UL)]
        [TestCase(StateFlags.SCUTemporaryOutOfService, 0x0000_0000_0000_0002UL)]
        [TestCase(StateFlags.LateSigningModeIsActive, 0x0000_0000_0000_0008UL)]
        [TestCase(StateFlags.MessageIsPending, 0x0000_0000_0000_0040UL)]
        [TestCase(StateFlags.DailyClosingIsDue, 0x0000_0000_0000_0100UL)]
        [TestCase(StateFlags.MonthlyClosingIsDue, 0x0000_0000_0000_0200UL)]
        [TestCase(StateFlags.YearlyClosingIsDue, 0x0000_0000_0000_0400UL)]
        [TestCase(StateFlags.ArchiveIsDue, 0x0000_0000_0000_0800UL)]
        public void StateFlags_HaveThePublishedValues(StateFlags flag, ulong expected)
        {
            ((ulong)flag).Should().Be(expected);
        }

        [Test]
        public void StateFlags_AreDistinctSingleBits()
        {
            var values = Enum.GetValues(typeof(StateFlags)).Cast<StateFlags>().Select(f => (ulong)f).ToList();

            values.Should().OnlyHaveUniqueItems();
            values.Should().OnlyContain(v => v != 0 && (v & (v - 1)) == 0);
        }

        [Test]
        public void WithFlag_CombinesFlagsAndIsFlagReadsEachBack()
        {
            var state = State.Success
                .WithCountry("FR")
                .WithVersion(2)
                .WithFlag(StateFlags.MonthlyClosingIsDue)
                .WithFlag(StateFlags.ArchiveIsDue);

            state.IsFlag(StateFlags.MonthlyClosingIsDue).Should().BeTrue();
            state.IsFlag(StateFlags.ArchiveIsDue).Should().BeTrue();
            state.IsFlag(StateFlags.YearlyClosingIsDue).Should().BeFalse();
            state.IsFlag(StateFlags.DailyClosingIsDue).Should().BeFalse();
            ((ulong)state & 0x0000_0000_FFFF_FFFFUL).Should().Be(0x0000_0A00UL);
        }
    }
}
