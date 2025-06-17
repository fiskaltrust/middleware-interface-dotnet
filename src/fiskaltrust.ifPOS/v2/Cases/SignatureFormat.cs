using System;

namespace fiskaltrust.ifPOS.v2.Cases;

public enum SignatureFormat : ulong
{
    /// <value><c>0x0000</c></value>
    Unknown = 0x0000,
    /// <value><c>0x0001</c></value>
    Text = 0x0001,
    /// <value><c>0x0002</c></value>
    Link = 0x0002,
    /// <value><c>0x0003</c></value>
    QRCode = 0x0003,
    /// <value><c>0x0004</c></value>
    Code128 = 0x0004,
    /// <value><c>0x0005</c></value>
    OcrA = 0x0005,
    /// <value><c>0x0006</c></value>
    Pdf417 = 0x0006,
    /// <value><c>0x0007</c></value>
    DataMatrix = 0x0007,
    /// <value><c>0x0008</c></value>
    Aztec = 0x0008,
    /// <value><c>0x0009</c></value>
    Ean8Barcode = 0x0009,

    /// <value><c>0x000A</c></value>
    Ean13 = 0x000A,
    /// <value><c>0x000B</c></value>
    UPCA = 0x000B,
    /// <value><c>0x000C</c></value>
    Code39 = 0x000C,
    /// <value><c>0x000D</c></value>
    Base64 = 0x000D
}

public static class SignatureFormatExt
{
    public static SignatureFormat AsSignatureFormat(this ulong self) => (SignatureFormat)self;
    public static T As<T>(this SignatureFormat self) where T : Enum, IConvertible => (T)Enum.ToObject(typeof(T), self);
    public static SignatureFormat Reset(this SignatureFormat self) => (SignatureFormat)(0xFFFF_F000_0000_0000 & (ulong)self);

    public static SignatureFormat WithVersion(this SignatureFormat self, byte version) => (SignatureFormat)((((ulong)self) & 0xFFFF_0FFF_FFFF_FFFF) | ((ulong)version << (4 * 11)));
    public static byte Version(this SignatureFormat self) => (byte)((((ulong)self) >> (4 * 11)) & 0xF);

    public static bool IsFormat(this SignatureFormat self, SignatureFormat format) => ((ulong)self & 0xFFFF) == (ulong)format;
    public static SignatureFormat WithFormat(this SignatureFormat self, SignatureFormat state) => (SignatureFormat)(((ulong)self & 0xFFFF_FFFF_FFFF_0000) | (ulong)state);
    public static SignatureFormat Format(this SignatureFormat self) => (SignatureFormat)((ulong)self & 0xFFFF);
}
