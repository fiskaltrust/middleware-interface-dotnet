namespace fiskaltrust.ifPOS.v2.Cases;

public enum SignatureTypeFlags
{
    /// <remarks>
    /// Value: 0x0000_0000_0001_0000
    /// </remarks>
    ArchivingRequired = 0x0000_0000_0001_0000,

    // These three are actually not flags but a case since 0x3 = 0x1 | 0x2
    /// <remarks>
    /// Value: 0x0000_0000_0010_0000
    /// </remarks>
    VisualizationOptional = 0x0000_0000_0010_0000,
    /// <remarks>
    /// Value: 0x0000_0000_0020_0000
    /// </remarks>
    DontVisualize = 0x0000_0000_0020_0000,
    /// <remarks>
    /// Value: 0x0000_0000_0030_0000
    /// </remarks>
    DigitalReceiptOnly = 0x0000_0000_0030_0000,
}

public static class SignatureTypeFlagsExt
{
    public static SignatureType WithFlag(this SignatureType self, SignatureTypeFlags flag) => (SignatureType)((long)self | (long)flag);
    public static bool IsFlag(this SignatureType self, SignatureTypeFlags flag) => ((long)self & (long)flag) == (long)flag;
}