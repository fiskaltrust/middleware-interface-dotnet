using System;

namespace fiskaltrust.ifPOS.v2.Cases;

public enum SignatureFormat : long
{
    /// <remarks>
    /// Value: 0x0000
    /// </remarks>
    Unknown = 0x0000,
    /// <remarks>
    /// Value: 0x0001
    /// </remarks>
    Text = 0x0001,
    /// <remarks>
    /// Value: 0x0002
    /// </remarks>
    Link = 0x0002,
    /// <remarks>
    /// Value: 0x0003
    /// </remarks>
    QRCode = 0x0003,
    /// <remarks>
    /// Value: 0x0004
    /// </remarks>
    Code128 = 0x0004,
    /// <remarks>
    /// Value: 0x0005
    /// </remarks>
    OcrA = 0x0005,
    /// <remarks>
    /// Value: 0x0006
    /// </remarks>
    Pdf417 = 0x0006,
    /// <remarks>
    /// Value: 0x0007
    /// </remarks>
    DataMatrix = 0x0007,
    /// <remarks>
    /// Value: 0x0008
    /// </remarks>
    Aztec = 0x0008,
    /// <remarks>
    /// Value: 0x0009
    /// </remarks>
    Ean8Barcode = 0x0009,

    /// <remarks>
    /// Value: 0x000A
    /// </remarks>
    Ean13 = 0x000A,
    /// <remarks>
    /// Value: 0x000B
    /// </remarks>
    UPCA = 0x000B,
    /// <remarks>
    /// Value: 0x000C
    /// </remarks>
    Code39 = 0x000C,
    /// <remarks>
    /// Value: 0x000D
    /// </remarks>
    Base64 = 0x000D
}

public static class SignatureFormatExt
{
    public static SignatureFormat AsSignatureFormat(this long self) => (SignatureFormat)self;
    public static T As<T>(this SignatureFormat self) where T : Enum, IConvertible => (T)Enum.ToObject(typeof(T), self);
    public static SignatureFormat Reset(this SignatureFormat self) => (SignatureFormat)(0xFFFF_F000_0000_0000 & (ulong)self);

    public static SignatureFormat WithVersion(this SignatureFormat self, byte version) => (SignatureFormat)((((ulong)self) & 0xFFFF_0FFF_FFFF_FFFF) | ((ulong)version << (4 * 11)));
    public static byte Version(this SignatureFormat self) => (byte)((((long)self) >> (4 * 11)) & 0xF);

    public static bool IsFormat(this SignatureFormat self, SignatureFormat format) => ((long)self & 0xFFFF) == (long)format;
    public static SignatureFormat WithFormat(this SignatureFormat self, SignatureFormat state) => (SignatureFormat)(((ulong)self & 0xFFFF_FFFF_FFFF_0000) | (ulong)state);
    public static SignatureFormat Format(this SignatureFormat self) => (SignatureFormat)((long)self & 0xFFFF);
}
