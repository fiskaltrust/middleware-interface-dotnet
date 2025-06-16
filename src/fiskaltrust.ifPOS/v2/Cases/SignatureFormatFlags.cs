namespace fiskaltrust.ifPOS.v2.Cases;

public enum SignatureFormatPosition : long
{
    /// <remarks>
    /// Value: 0x0000_0000_0000_0000
    /// </remarks>
    AfterPayItemBlockBeforeFooter = 0x0000_0000_0000_0000,
    /// <remarks>
    /// Value: 0x0000_0000_0001_0000
    /// </remarks>
    AfterHeader = 0x0000_0000_0001_0000,
    /// <remarks>
    /// Value: 0x0000_0000_0002_0000
    /// </remarks>
    AfterChargeItemBlock = 0x0000_0000_0002_0000,
    /// <remarks>
    /// Value: 0x0000_0000_0003_0000
    /// </remarks>
    AfterTotalTaxBlock = 0x0000_0000_0003_0000,
    /// <remarks>
    /// Value: 0x0000_0000_0004_0000
    /// </remarks>
    AfterFooter = 0x0000_0000_0004_0000,
    /// <remarks>
    /// Value: 0x0000_0000_0005_0000
    /// </remarks>
    BeforeHeader = 0x0000_0000_0005_0000,
}

public static class SignatureFormatPositionExt
{
    public static SignatureFormat WithPosition(this SignatureFormat self, SignatureFormatPosition flag) => (SignatureFormat)((long)self | (long)flag);
    public static bool IsPosition(this SignatureFormat self, SignatureFormatPosition flag) => ((long)self & (long)flag) == (long)flag;
}