using fiskaltrust.ifPOS.v2.Cases;
using System;

namespace fiskaltrust.ifPOS.v2.es.Cases;

public enum SignatureTypeES : long
{
    /// <value><c>0x4553_2000_0000_1001</c></value>
    InitialOperationReceipt = 0x4553_2000_0000_1001,
    /// <value><c>0x4553_2000_0000_1002</c></value>
    OutOfOperationReceipt = 0x4553_2000_0000_1002,
    /// <value><c>0x4553_2000_0000_0001</c></value>
    Url = 0x4553_2000_0000_0001,
    /// <value><c>0x4553_2000_0000_0002</c></value>
    NIF = 0x4553_2000_0000_0002,
    /// <value><c>0x4553_2000_0000_0003</c></value>
    Signature = 0x4553_2000_0000_0003,
    /// <value><c>0x4553_2000_0000_0004</c></value>
    Huella = 0x4553_2000_0000_0004,
}

public static class SignatureTypeESExt
{
    public static T As<T>(this SignatureTypeES self) where T : Enum, IConvertible => (T)Enum.ToObject(typeof(T), self);

    public static bool IsType(this SignatureType self, SignatureTypeES signatureTypeES) => ((long)self & 0xFFFF) == ((long)signatureTypeES & 0xFFFF);
    public static SignatureType WithType(this SignatureType self, SignatureTypeES state) => (SignatureType)(((ulong)self & 0xFFFF_FFFF_FFFF_0000) | ((ulong)state & 0xFFFF));
    public static SignatureTypeES Type(this SignatureType self) => (SignatureTypeES)((long)self & 0xFFFF);
}