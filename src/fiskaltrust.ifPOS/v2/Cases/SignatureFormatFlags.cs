namespace fiskaltrust.ifPOS.v2.Cases;

public enum SignatureFormatPosition : long
{
    /// <value><c>0x0000_0000_0000_0000</c></value>
    AfterPayItemBlockBeforeFooter = 0x0000_0000_0000_0000,
    /// <value><c>0x0000_0000_0001_0000</c></value>
    AfterHeader = 0x0000_0000_0001_0000,
    /// <value><c>0x0000_0000_0002_0000</c></value>
    AfterChargeItemBlock = 0x0000_0000_0002_0000,
    /// <value><c>0x0000_0000_0003_0000</c></value>
    AfterTotalTaxBlock = 0x0000_0000_0003_0000,
    /// <value><c>0x0000_0000_0004_0000</c></value>
    AfterFooter = 0x0000_0000_0004_0000,
    /// <value><c>0x0000_0000_0005_0000</c></value>
    BeforeHeader = 0x0000_0000_0005_0000,
}

public static class SignatureFormatPositionExt
{
    public static SignatureFormat WithPosition(this SignatureFormat self, SignatureFormatPosition flag) => (SignatureFormat)((long)self | (long)flag);
    public static bool IsPosition(this SignatureFormat self, SignatureFormatPosition flag) => ((long)self & (long)flag) == (long)flag;
}