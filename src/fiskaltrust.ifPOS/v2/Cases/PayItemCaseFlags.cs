namespace fiskaltrust.ifPOS.v2.Cases;

public enum PayItemCaseFlags : ulong
{
    /// <value><c>0x0000_0000_0001_0000</c></value>
    Void = 0x0000_0000_0001_0000,
    /// <value><c>0x0000_0000_0002_0000</c></value>
    Refund = 0x0000_0000_0002_0000,
    /// <value><c>0x0000_0000_0004_0000</c></value>
    Reserved = 0x0000_0000_0004_0000,
    /// <value><c>0x0000_0000_0008_0000</c></value>
    Downpayment = 0x0000_0000_0008_0000,
    /// <value><c>0x0000_0000_0010_0000</c></value>
    ForeignCurrency = 0x0000_0000_0010_0000,
    /// <value><c>0x0000_0000_0020_0000</c></value>
    Change = 0x0000_0000_0020_0000,
    /// <value><c>0x0000_0000_0040_0000</c></value>
    Tip = 0x0000_0000_0040_0000,
    /// <value><c>0x0000_0000_0080_0000</c></value>
    Electronic = 0x0000_0000_0080_0000,
    /// <value><c>0x0000_0000_0100_0000</c></value>
    Interface = 0x0000_0000_0100_0000,
    /// <value><c>0x0000_0000_8000_0000</c></value>
    ShowInChargeItems = 0x0000_0000_8000_0000,
}

public static class PayItemCaseFlagsExt
{
    public static PayItemCase WithFlag(this PayItemCase self, PayItemCaseFlags flag) => (PayItemCase)((ulong)self | (ulong)flag);

    // HasFlag would be a nicer name but that method does alrady exist for all enums
    public static bool IsFlag(this PayItemCase self, PayItemCaseFlags flag) => ((ulong)self & (ulong)flag) == (ulong)flag;
}