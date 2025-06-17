namespace fiskaltrust.ifPOS.v2.Cases;

public enum ReceiptCaseFlagsGR : ulong
{
    /// <value><c>0x0000_0100_0000_0000</c></value>
    IsSelfPricingOperation = 0x0000_0100_0000_0000,
}

public static class ReceiptCaseFlagsGRExt
{
    public static ReceiptCase WithFlag(this ReceiptCase self, ReceiptCaseFlagsGR flag) => (ReceiptCase)((ulong)self | (ulong)flag);
    public static bool IsFlag(this ReceiptCase self, ReceiptCaseFlagsGR flag) => ((ulong)self & (ulong)flag) == (ulong)flag;
}