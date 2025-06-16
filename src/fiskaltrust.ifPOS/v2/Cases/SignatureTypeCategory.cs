namespace fiskaltrust.ifPOS.v2.Cases;

public enum SignatureTypeCategory
{
    /// <remarks>
    /// Value: 0x0000
    /// </remarks>
    Uncategorized = 0x0000,
    /// <remarks>
    /// Value: 0x1000
    /// </remarks>
    Information = 0x1000,
    /// <remarks>
    /// Value: 0x2000
    /// </remarks>
    Alert = 0x2000,
    /// <remarks>
    /// Value: 0x3000
    /// </remarks>
    Failure = 0x3000,
}

public static class SignatureTypeCategoryExt
{
    public static SignatureType WithCategory(this SignatureType self, SignatureTypeCategory category) => (SignatureType)(((ulong)self & 0xFFFF_FFFF_FFFF_0FFF) | (ulong)category);
    public static bool IsCategory(this SignatureType self, SignatureTypeCategory category) => ((long)self & 0xF000) == (long)category;
}