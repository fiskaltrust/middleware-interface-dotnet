using System;

namespace fiskaltrust.ifPOS.v2.Cases;

public enum ChargeItemCase : ulong
{
    /// <value><c>0x0</c></value>
    UnknownService = 0x0,
    /// <value><c>0x1</c></value>
    DiscountedVatRate1 = 0x1,
    /// <value><c>0x2</c></value>
    DiscountedVatRate2 = 0x2,
    /// <value><c>0x3</c></value>
    NormalVatRate = 0x3,
    /// <value><c>0x4</c></value>
    SuperReducedVatRate1 = 0x4,
    /// <value><c>0x5</c></value>
    SuperReducedVatRate2 = 0x5,
    /// <value><c>0x6</c></value>
    ParkingVatRate = 0x6,
    /// <value><c>0x7</c></value>
    ZeroVatRate = 0x7,
    /// <value><c>0x8</c></value>
    NotTaxable = 0x8
}

public static class ChargeItemCaseExt
{
    public static ChargeItemCase AsChargeItemCase(this ulong self) => (ChargeItemCase)self;
    public static T As<T>(this ChargeItemCase self) where T : Enum, IConvertible => (T)Enum.ToObject(typeof(T), self);
    public static ChargeItemCase Reset(this ChargeItemCase self) => (ChargeItemCase)(0xFFFF_F000_0000_0000 & (ulong)self);

    public static ChargeItemCase WithVersion(this ChargeItemCase self, byte version) => (ChargeItemCase)((((ulong)self) & 0xFFFF_0FFF_FFFF_FFFF) | ((ulong)version << (4 * 11)));
    public static byte Version(this ChargeItemCase self) => (byte)((((ulong)self) >> (4 * 11)) & 0xF);

    public static ChargeItemCase WithCountry(this ChargeItemCase self, string country)
        => country?.Length != 2
            ? throw new Exception($"'{country}' is not ISO country code")
            : self.WithCountry((ulong)(country[0] << (4 * 2)) + country[1]);


    public static ChargeItemCase WithCountry(this ChargeItemCase self, ulong country) => (ChargeItemCase)(((ulong)self & 0x0000_FFFF_FFFF_FFFF) | (country << (4 * 12)));
    public static ulong CountryCode(this ChargeItemCase self) => (ulong)self >> (4 * 12);
#nullable enable
    public static string? Country(this ChargeItemCase self)
    {
        var countryCode = self.CountryCode();
        if (countryCode == 0)
        {
            return null;
        }
        return Char.ConvertFromUtf32((char)(countryCode & 0xFF00) >> (4 * 2)) + Char.ConvertFromUtf32((char)(countryCode & 0x00FF));
    }
#nullable disable
    public static bool IsVat(this ChargeItemCase self, ChargeItemCase @case) => ((ulong)self & 0xF) == (ulong)@case;
    public static ChargeItemCase WithVat(this ChargeItemCase self, ChargeItemCase state) => (ChargeItemCase)(((ulong)self & 0xFFFF_FFFF_FFFF_FFF0) | (ulong)state);
    public static ChargeItemCase Vat(this ChargeItemCase self) => (ChargeItemCase)((ulong)self & 0xF);
}
