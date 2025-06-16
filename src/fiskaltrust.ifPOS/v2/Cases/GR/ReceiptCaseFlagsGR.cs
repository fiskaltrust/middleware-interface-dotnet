namespace fiskaltrust.ifPOS.v2.Cases;

public enum ReceiptCaseFlagsGR : long
{
    /// <remarks>
    /// Value: 0x0000_0100_0000_0000
    /// </remarks>
    IsSelfPricingOperation = 0x0000_0100_0000_0000,
}

public static class ReceiptCaseFlagsGRExt
{
    public static ReceiptCase WithFlag(this ReceiptCase self, ReceiptCaseFlagsGR flag) => (ReceiptCase)((long)self | (long)flag);
    public static bool IsFlag(this ReceiptCase self, ReceiptCaseFlags flag) => ((long)self & (long)flag) == (long)flag;
}