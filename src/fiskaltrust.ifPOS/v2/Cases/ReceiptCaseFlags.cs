namespace fiskaltrust.ifPOS.v2.Cases;

public enum ReceiptCaseFlags : ulong
{
    /// <value><c>0x0000_0000_0001_0000</c></value>
    LateSigning = 0x0000_0000_0001_0000,
    /// <value><c>0x0000_0000_0002_0000</c></value>
    Training = 0x0000_0000_0002_0000,
    /// <value><c>0x0000_0000_0004_0000</c></value>
    Void = 0x0000_0000_0004_0000,
    /// <value><c>0x0000_0000_0008_0000</c></value>
    HandWritten = 0x0000_0000_0008_0000,
    /// <value><c>0x0000_0000_0010_0000</c></value>
    IssuerIsSmallBusiness = 0x0000_0000_0010_0000,
    /// <value><c>0x0000_0000_0020_0000</c></value>
    ReceiverIsBusiness = 0x0000_0000_0020_0000,
    /// <value><c>0x0000_0000_0040_0000</c></value>
    ReceiverIsKnown = 0x0000_0000_0040_0000,
    /// <value><c>0x0000_0000_0080_0000</c></value>
    SaleInForeignCountry = 0x0000_0000_0080_0000,
    /// <value><c>0x0000_0000_0100_0000</c></value>
    Refund = 0x0000_0000_0100_0000,
    /// <value><c>0x0000_0000_8000_0000</c></value>
    ReceiptRequested = 0x0000_0000_8000_0000,

    /// <value><c>0x0000_0000_0200_0000</c></value>
    AdditionalInformationRequested = 0x0000_0000_0200_0000,
    /// <value><c>0x0000_0000_0400_0000</c></value>
    SCUDataDownloadRequested = 0x0000_0000_0400_0000,
    /// <value><c>0x0000_0000_0800_0000</c></value>
    EnforceServiceOperations = 0x0000_0000_0800_0000,
    /// <value><c>0x0000_0000_1000_0000</c></value>
    CleanupOpenTransactions = 0x0000_0000_1000_0000,

    /// <value><c>0x0000_0000_2000_0000</c></value>
    PreventEnablingOrDisablingSigningDevices = 0x0000_0000_2000_0000,
}

public static class ReceiptCaseFlagsExt
{
    public static ReceiptCase WithFlag(this ReceiptCase self, ReceiptCaseFlags flag) => (ReceiptCase)((ulong)self | (ulong)flag);
    public static bool IsFlag(this ReceiptCase self, ReceiptCaseFlags flag) => ((ulong)self & (ulong)flag) == (ulong)flag;
}