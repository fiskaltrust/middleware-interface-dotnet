namespace fiskaltrust.ifPOS.v2.Cases;

public enum StateFlags : long
{
    SecurityMechanismDeactivated = 0x0000_0000_0000_0001,
    SCUTemporaryOutOfService = 0x0000_0000_0000_0002,
    LateSigningModeIsActive = 0x0000_0000_0000_0008,
    MessageIsPending = 0x0000_0000_0000_0040,
    DailyClosingIsDue = 0x0000_0000_0000_0100,
}