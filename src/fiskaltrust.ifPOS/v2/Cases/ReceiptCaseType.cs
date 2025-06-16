namespace fiskaltrust.ifPOS.v2.Cases;

public enum ReceiptCaseType : long
{
    /// <remarks>
    /// Value: 0x0000
    /// </remarks>
    Receipt = 0x0000,
    /// <remarks>
    /// Value: 0x1000
    /// </remarks>
    Invoice = 0x1000,
    /// <remarks>
    /// Value: 0x2000
    /// </remarks>
    DailyOperations = 0x2000,
    /// <remarks>
    /// Value: 0x3000
    /// </remarks>
    Log = 0x3000,
    /// <remarks>
    /// Value: 0x4000
    /// </remarks>
    Lifecycle = 0x4000
}

public static class ReceiptCaseTypeExt
{
    public static bool IsType(this ReceiptCase self, ReceiptCaseType type) => ((long)self & 0xF000) == (long)type;
    public static ReceiptCase WithType(this ReceiptCase self, ReceiptCaseType state) => (ReceiptCase)(((ulong)self & 0xFFFF_FFFF_FFFF_0FFF) | (ulong)state);
    public static ReceiptCaseType Type(this ReceiptCase self) => (ReceiptCaseType)((long)self & 0xF000);
}