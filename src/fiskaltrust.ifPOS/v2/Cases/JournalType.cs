using System;

namespace fiskaltrust.ifPOS.v2.Cases;

public enum JournalType : ulong
{
    /// <value><c>0x0000</c></value>
    Unknown = 0x0000,
    /// <value><c>0x0001</c></value>
    ActionJournal = 0x0001,
    /// <value><c>0x0002</c></value>
    ReceiptJournal = 0x0002,
    /// <value><c>0x0003</c></value>
    QueueItem = 0x0003,
    /// <value><c>0x00FF</c></value>
    Configuration = 0x00FF,
}

public static class JournalTypeExt
{
    public static JournalType AsJournalType(this ulong self) => (JournalType)self;
    public static T As<T>(this JournalType self) where T : Enum, IConvertible => (T)Enum.ToObject(typeof(T), self);
    public static JournalType Reset(this JournalType self) => (JournalType)(0xFFFF_F000_0000_0000 & (ulong)self);

    public static JournalType WithVersion(this JournalType self, byte version) => (JournalType)((((ulong)self) & 0xFFFF_0FFF_FFFF_FFFF) | ((ulong)version << (4 * 11)));
    public static byte Version(this JournalType self) => (byte)((((ulong)self) >> (4 * 11)) & 0xF);

    public static JournalType WithCountry(this JournalType self, string country)
        => country?.Length != 2
            ? throw new Exception($"'{country}' is not ISO country code")
            : self.WithCountry((ulong)(country[0] << (4 * 2)) + country[1]);

    public static JournalType WithCountry(this JournalType self, ulong country) => (JournalType)(((ulong)self & 0x0000_FFFF_FFFF_FFFF) | (country << (4 * 12)));
    public static ulong CountryCode(this JournalType self) => (ulong)self >> (4 * 12);
    public static string Country(this JournalType self)
    {
        var countryCode = self.CountryCode();

        return Char.ConvertFromUtf32((char)(countryCode & 0xFF00) >> (4 * 2)) + Char.ConvertFromUtf32((char)(countryCode & 0x00FF));
    }

    public static bool IsCase(this JournalType self, JournalType @case) => ((ulong)self & 0xFFFF) == (ulong)@case;
    public static JournalType WithCase(this JournalType self, JournalType state) => (JournalType)(((ulong)self & 0xFFFF_FFFF_FFFF_0000) | (ulong)state);
    public static JournalType Case(this JournalType self) => (JournalType)((ulong)self & 0xFFFF);
}
