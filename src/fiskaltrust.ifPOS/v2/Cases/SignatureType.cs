﻿using System;

namespace fiskaltrust.ifPOS.v2.Cases;

public enum SignatureType : ulong
{
    /// <value><c>0x0</c></value>
    Unknown = 0
}

public static class SignatureTypeExt
{
    public static SignatureType AsSignatureType(this ulong self) => (SignatureType)self;
    public static T As<T>(this SignatureType self) where T : Enum, IConvertible => (T)Enum.ToObject(typeof(T), self);
    public static SignatureType Reset(this SignatureType self) => (SignatureType)(0xFFFF_F000_0000_0000 & (ulong)self);

    public static SignatureType WithVersion(this SignatureType self, byte version) => (SignatureType)((((ulong)self) & 0xFFFF_0FFF_FFFF_FFFF) | ((ulong)version << (4 * 11)));
    public static byte Version(this SignatureType self) => (byte)((((ulong)self) >> (4 * 11)) & 0xF);

    public static SignatureType WithCountry(this SignatureType self, string country)
        => country?.Length != 2
            ? throw new Exception($"'{country}' is not ISO country code")
            : self.WithCountry((ulong)(country[0] << (4 * 2)) + country[1]);

    public static SignatureType WithCountry(this SignatureType self, ulong country) => (SignatureType)(((ulong)self & 0x0000_FFFF_FFFF_FFFF) | (country << (4 * 12)));
    public static ulong CountryCode(this SignatureType self) => (ulong)self >> (4 * 12);
    public static string Country(this SignatureType self)
    {
        var countryCode = self.CountryCode();

        return Char.ConvertFromUtf32((char)(countryCode & 0xFF00) >> (4 * 2)) + Char.ConvertFromUtf32((char)(countryCode & 0x00FF));
    }
}