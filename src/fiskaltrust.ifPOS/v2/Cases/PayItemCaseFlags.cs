namespace fiskaltrust.ifPOS.v2.Cases;

public enum PayItemCaseFlags : long
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
    Reserved = 0x0000_0000_0004_0000,
    /// <remarks>
    /// Value: 0x0000_0000_0008_0000
    /// </remarks>
    Downpayment = 0x0000_0000_0008_0000,
    /// <remarks>
    /// Value: 0x0000_0000_0010_0000
    /// </remarks>
    ForeignCurrency = 0x0000_0000_0010_0000,
    /// <remarks>
    /// Value: 0x0000_0000_0020_0000
    /// </remarks>
    Change = 0x0000_0000_0020_0000,
    /// <remarks>
    /// Value: 0x0000_0000_0040_0000
    /// </remarks>
    Tip = 0x0000_0000_0040_0000,
    /// <remarks>
    /// Value: 0x0000_0000_0080_0000
    /// </remarks>
    Electronic = 0x0000_0000_0080_0000,
    /// <remarks>
    /// Value: 0x0000_0000_0100_0000
    /// </remarks>
    Interface = 0x0000_0000_0100_0000,
    /// <remarks>
    /// Value: 0x0000_0000_8000_0000
    /// </remarks>
    ShowInChargeItems = 0x0000_0000_8000_0000,
}

public static class PayItemCaseFlagsExt
{
    public static PayItemCase WithFlag(this PayItemCase self, PayItemCaseFlags flag) => (PayItemCase)((long)self | (long)flag);

    // HasFlag would be a nicer name but that method does alrady exist for all enums
    public static bool IsFlag(this PayItemCase self, PayItemCaseFlags flag) => ((long)self & (long)flag) == (long)flag;
}