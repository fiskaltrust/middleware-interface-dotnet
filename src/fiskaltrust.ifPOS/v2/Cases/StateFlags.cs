namespace fiskaltrust.ifPOS.v2.Cases;

public enum StateFlags : long
{
    /// <remarks>
    /// Value: 0x0000_0000_0000_0001
    /// </remarks>
    SecurityMechanismDeactivated = 0x0000_0000_0000_0001,
    /// <remarks>
    /// Value: 0x0000_0000_0000_0002
    /// </remarks>
    SCUTemporaryOutOfService = 0x0000_0000_0000_0002,
    /// <remarks>
    /// Value: 0x0000_0000_0000_0008
    /// </remarks>
    LateSigningModeIsActive = 0x0000_0000_0000_0008,
    /// <remarks>
    /// Value: 0x0000_0000_0000_0040
    /// </remarks>
    MessageIsPending = 0x0000_0000_0000_0040,
    /// <remarks>
    /// Value: 0x0000_0000_0000_0100
    /// </remarks>
    DailyClosingIsDue = 0x0000_0000_0000_0100,
}

public static class StateFlagsExt
{
    public static State WithFlag(this State self, StateFlags flag) => (State)((long)self | (long)flag);
    public static bool IsFlag(this State self, StateFlags flag) => ((long)self & (long)flag) == (long)flag;
}