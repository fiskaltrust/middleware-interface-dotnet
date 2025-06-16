namespace fiskaltrust.ifPOS.v2.Cases;

public enum ChargeItemCaseFlags : long
{
    /// <remarks>
    /// Value: 0x0000_0000_0001_0000
    /// </remarks>
    Void = 0x0000_0000_0001_0000,
    /// <remarks>
    /// Value: 0x0000_0000_0002_0000
    /// </remarks>
    Refund = 0x0000_0000_0002_0000,
    /// <remarks>
    /// Value: 0x0000_0000_0004_0000
    /// </remarks>
    ExtraOrDiscount = 0x0000_0000_0004_0000,
}

public static class ChargeItemCaseFlagsExt
{
    public static ChargeItemCase WithFlag(this ChargeItemCase self, ChargeItemCaseFlags flag) => (ChargeItemCase)((long)self | (long)flag);

    // HasFlag would be a nicer name but that method does alrady exist for all enums
    public static bool IsFlag(this ChargeItemCase self, ChargeItemCaseFlags flag) => ((long)self & (long)flag) == (long)flag;
}