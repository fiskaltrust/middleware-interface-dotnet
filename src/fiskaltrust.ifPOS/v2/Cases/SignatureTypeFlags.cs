namespace fiskaltrust.ifPOS.v2.Cases;

public enum SignatureTypeFlags
{
    /// <value><c>0x0000_0000_0001_0000</c></value>
    ArchivingRequired = 0x0000_0000_0001_0000,

    // These three are actually not flags but a case since 0x3 = 0x1 | 0x2
    /// <value><c>0x0000_0000_0010_0000</c></value>
    VisualizationOptional = 0x0000_0000_0010_0000,
    /// <value><c>0x0000_0000_0020_0000</c></value>
    DontVisualize = 0x0000_0000_0020_0000,
    /// <value><c>0x0000_0000_0030_0000</c></value>
    DigitalReceiptOnly = 0x0000_0000_0030_0000,
}

public static class SignatureTypeFlagsExt
{
    public static SignatureType WithFlag(this SignatureType self, SignatureTypeFlags flag) => (SignatureType)((long)self | (long)flag);
    public static bool IsFlag(this SignatureType self, SignatureTypeFlags flag) => ((long)self & (long)flag) == (long)flag;
}