using fiskaltrust.ifPOS.v2.Cases;
using System;

namespace fiskaltrust.ifPOS.v2.fr.Cases;

public enum SignatureTypeFR : long
{
    /// <value><c>0x4652_2000_0000_1001</c></value>
    InitialOperationReceipt = 0x4652_2000_0000_1001,
    /// <value><c>0x4652_2000_0000_1002</c></value>
    OutOfOperationReceipt = 0x4652_2000_0000_1002,
}

public static class SignatureTypeFRExt
{
    public static T As<T>(this SignatureTypeFR self) where T : Enum, IConvertible => (T)Enum.ToObject(typeof(T), self);

    public static bool IsType(this SignatureType self, SignatureTypeFR signatureTypeFR) => ((long)self & 0xFFFF) == ((long)signatureTypeFR & 0xFFFF);
    public static SignatureType WithType(this SignatureType self, SignatureTypeFR state) => (SignatureType)(((ulong)self & 0xFFFF_FFFF_FFFF_0000) | ((ulong)state & 0xFFFF));
    public static SignatureTypeFR Type(this SignatureType self) => (SignatureTypeFR)((long)self & 0xFFFF);
}
