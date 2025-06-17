namespace fiskaltrust.ifPOS.v2.Cases;

public enum ReceiptCaseType : ulong
{
    /// <value><c>0x0000</c></value>
    Receipt = 0x0000,
    /// <value><c>0x1000</c></value>
    Invoice = 0x1000,
    /// <value><c>0x2000</c></value>
    DailyOperations = 0x2000,
    /// <value><c>0x3000</c></value>
    Log = 0x3000,
    /// <value><c>0x4000</c></value>
    Lifecycle = 0x4000
}

public static class ReceiptCaseTypeExt
{
    public static bool IsType(this ReceiptCase self, ReceiptCaseType type) => ((ulong)self & 0xF000) == (ulong)type;
    public static ReceiptCase WithType(this ReceiptCase self, ReceiptCaseType state) => (ReceiptCase)(((ulong)self & 0xFFFF_FFFF_FFFF_0FFF) | (ulong)state);
    public static ReceiptCaseType Type(this ReceiptCase self) => (ReceiptCaseType)((ulong)self & 0xF000);
}