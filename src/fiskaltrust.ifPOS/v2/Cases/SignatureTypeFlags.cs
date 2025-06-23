namespace fiskaltrust.ifPOS.v2.Cases;

public enum SignatureTypeFlags
{
    /// <value><c>0x0000_0000_0001_0000</c></value>
    ArchivingRequired = 0x0000_0000_0001_0000,

    /// <value><c>0x0000_0000_0010_0000</c></value>
    VisualizationOptional = 0x0000_0000_0010_0000,
    /// <value><c>0x0000_0000_0020_0000</c></value>
    DontVisualize = 0x0000_0000_0020_0000,
    /// <value><c>0x0000_0000_0040_0000</c></value>
    PrintedReceiptOnly = 0x0000_0000_0040_0000,
    /// <value><c>0x0000_0000_0080_0000</c></value>
    DigitalReceiptOnly = 0x0000_0000_0080_0000,
}

public static class SignatureTypeFlagsExt
{
    public static SignatureType WithFlag(this SignatureType self, SignatureTypeFlags flag) => (SignatureType)((ulong)self | (ulong)flag);
    public static bool IsFlag(this SignatureType self, SignatureTypeFlags flag) => ((ulong)self & (ulong)flag) == (ulong)flag;
}