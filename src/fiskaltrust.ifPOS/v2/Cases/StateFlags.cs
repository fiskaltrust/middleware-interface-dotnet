namespace fiskaltrust.ifPOS.v2.Cases;

public enum StateFlags : ulong
{
    /// <value><c>0x0000_0000_0000_0001</c></value>
    SecurityMechanismDeactivated = 0x0000_0000_0000_0001,
    /// <value><c>0x0000_0000_0000_0002</c></value>
    SCUTemporaryOutOfService = 0x0000_0000_0000_0002,
    /// <value><c>0x0000_0000_0000_0008</c></value>
    LateSigningModeIsActive = 0x0000_0000_0000_0008,
    /// <value><c>0x0000_0000_0000_0040</c></value>
    MessageIsPending = 0x0000_0000_0000_0040,
    /// <value><c>0x0000_0000_0000_0100</c></value>
    DailyClosingIsDue = 0x0000_0000_0000_0100,
}

public static class StateFlagsExt
{
    public static State WithFlag(this State self, StateFlags flag) => (State)((ulong)self | (ulong)flag);
    public static bool IsFlag(this State self, StateFlags flag) => ((ulong)self & (ulong)flag) == (ulong)flag;
}