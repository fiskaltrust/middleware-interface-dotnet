using fiskaltrust.ifPOS.v2.Cases;
using System;

namespace fiskaltrust.ifPOS.v2.fr.Cases;

public enum JournalTypeFR : long
{
}

public static class JournalTypeFRExt
{
    public static T As<T>(this JournalTypeFR self) where T : Enum, IConvertible => (T)Enum.ToObject(typeof(T), self);

    public static bool IsType(this JournalType self, JournalTypeFR JournalTypeFR) => ((long)self & 0xFFFF) == ((long)JournalTypeFR & 0xFFFF);
    public static JournalType WithType(this JournalType self, JournalTypeFR state) => (JournalType)(((ulong)self & 0xFFFF_FFFF_FFFF_0000) | ((ulong)state & 0xFFFF));
    public static JournalTypeFR Type(this JournalType self) => (JournalTypeFR)((long)self & 0xFFFF);
}
