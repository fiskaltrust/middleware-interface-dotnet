namespace fiskaltrust.ifPOS.v2.Cases;

public enum ReceiptCaseFlags : long
{
    /// <remarks>
    /// Value: 0x0000_0000_0001_0000
    /// </remarks>
    LateSigning = 0x0000_0000_0001_0000,
    /// <remarks>
    /// Value: 0x0000_0000_0002_0000
    /// </remarks>
    Training = 0x0000_0000_0002_0000,
    /// <remarks>
    /// Value: 0x0000_0000_0004_0000
    /// </remarks>
    Void = 0x0000_0000_0004_0000,
    /// <remarks>
    /// Value: 0x0000_0000_0008_0000
    /// </remarks>
    HandWritten = 0x0000_0000_0008_0000,
    /// <remarks>
    /// Value: 0x0000_0000_0010_0000
    /// </remarks>
    IssuerIsSmallBusiness = 0x0000_0000_0010_0000,
    /// <remarks>
    /// Value: 0x0000_0000_0020_0000
    /// </remarks>
    ReceiverIsBusiness = 0x0000_0000_0020_0000,
    /// <remarks>
    /// Value: 0x0000_0000_0040_0000
    /// </remarks>
    ReceiverIsKnown = 0x0000_0000_0040_0000,
    /// <remarks>
    /// Value: 0x0000_0000_0080_0000
    /// </remarks>
    SaleInForeignCountry = 0x0000_0000_0080_0000,
    /// <remarks>
    /// Value: 0x0000_0000_0100_0000
    /// </remarks>
    Refund = 0x0000_0000_0100_0000,
    /// <remarks>
    /// Value: 0x0000_0000_8000_0000
    /// </remarks>
    ReceiptRequested = 0x0000_0000_8000_0000,

    /// <remarks>
    /// Value: 0x0000_0000_0200_0000
    /// </remarks>
    AdditionalInformationRequested = 0x0000_0000_0200_0000,
    /// <remarks>
    /// Value: 0x0000_0000_0400_0000
    /// </remarks>
    SCUDataDownloadRequested = 0x0000_0000_0400_0000,
    /// <remarks>
    /// Value: 0x0000_0000_0800_0000
    /// </remarks>
    EnforceServiceOperations = 0x0000_0000_0800_0000,
    /// <remarks>
    /// Value: 0x0000_0000_1000_0000
    /// </remarks>
    CleanupOpenTransactions = 0x0000_0000_1000_0000,

    /// <remarks>
    /// Value: 0x0000_0000_2000_0000
    /// </remarks>
    PreventEnablingOrDisablingSigningDevices = 0x0000_0000_2000_0000,
}

public static class ReceiptCaseFlagsExt
{
    public static ReceiptCase WithFlag(this ReceiptCase self, ReceiptCaseFlags flag) => (ReceiptCase)((long)self | (long)flag);
    public static bool IsFlag(this ReceiptCase self, ReceiptCaseFlags flag) => ((long)self & (long)flag) == (long)flag;
}