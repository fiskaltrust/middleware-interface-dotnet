namespace fiskaltrust.ifPOS.v2.Cases;

public enum SignatureTypeCategory
{
    /// <value><c>0x0000</c></value>
    Uncategorized = 0x0000,
    /// <value><c>0x1000</c></value>
    Information = 0x1000,
    /// <value><c>0x2000</c></value>
    Alert = 0x2000,
    /// <value><c>0x3000</c></value>
    Failure = 0x3000,
}

public static class SignatureTypeCategoryExt
{
    public static SignatureType WithCategory(this SignatureType self, SignatureTypeCategory category) => (SignatureType)(((ulong)self & 0xFFFF_FFFF_FFFF_0FFF) | (ulong)category);
    public static bool IsCategory(this SignatureType self, SignatureTypeCategory category) => ((ulong)self & 0xF000) == (ulong)category;
}