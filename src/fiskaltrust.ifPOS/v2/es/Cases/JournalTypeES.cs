using fiskaltrust.ifPOS.v2.Cases;
using System;

namespace fiskaltrust.ifPOS.v2.es.Cases;

public enum JournalTypeES : long
{
    /// <value><c>0x4553_2000_0000_0001</c></value>
    VeriFactu = 0x4553_2000_0000_0001,
}

public static class JournalTypeESExt
{
    public static T As<T>(this JournalTypeES self) where T : Enum, IConvertible => (T)Enum.ToObject(typeof(T), self);

    public static bool IsType(this JournalType self, JournalTypeES JournalTypeES) => ((long)self & 0xFFFF) == ((long)JournalTypeES & 0xFFFF);
    public static JournalType WithType(this JournalType self, JournalTypeES state) => (JournalType)(((ulong)self & 0xFFFF_FFFF_FFFF_0000) | ((ulong)state & 0xFFFF));
    public static JournalTypeES Type(this JournalType self) => (JournalTypeES)((long)self & 0xFFFF);
}