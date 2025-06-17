namespace fiskaltrust.ifPOS.v2.Cases;

public enum ChargeItemCaseFlags : ulong
{
    /// <value><c>0x0000_0000_0001_0000</c></value>
    Void = 0x0000_0000_0001_0000,
    /// <value><c>0x0000_0000_0002_0000</c></value>
    Refund = 0x0000_0000_0002_0000,
    /// <value><c>0x0000_0000_0004_0000</c></value>
    ExtraOrDiscount = 0x0000_0000_0004_0000,
}

public static class ChargeItemCaseFlagsExt
{
    public static ChargeItemCase WithFlag(this ChargeItemCase self, ChargeItemCaseFlags flag) => (ChargeItemCase)((ulong)self | (ulong)flag);

    // HasFlag would be a nicer name but that method does alrady exist for all enums
    public static bool IsFlag(this ChargeItemCase self, ChargeItemCaseFlags flag) => ((ulong)self & (ulong)flag) == (ulong)flag;
}