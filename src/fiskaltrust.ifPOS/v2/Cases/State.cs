﻿using System;

namespace fiskaltrust.ifPOS.v2.Cases;

public enum State : ulong
{
    /// <value><c>0x0000_0000</c></value>
    Success = 0x0000_0000,
    /// <value><c>0xEEEE_EEEE</c></value>
    Error = 0xEEEE_EEEE,
    /// <value><c>0xFFFF_FFFF</c></value>
    Fail = 0xFFFF_FFFF
}

public static class StateExt
{
    public static State AsState(this ulong self) => (State)self;
    public static T As<T>(this State self) where T : Enum, IConvertible => (T)Enum.ToObject(typeof(T), self);
    public static State Reset(this State self) => (State)(0xFFFF_F000_0000_0000 & (ulong)self);

    public static State WithVersion(this State self, byte version) => (State)((((ulong)self) & 0xFFFF_0FFF_FFFF_FFFF) | ((ulong)version << (4 * 11)));
    public static byte Version(this State self) => (byte)((((ulong)self) >> (4 * 11)) & 0xF);

    public static State WithCountry(this State self, string country)
        => country?.Length != 2
            ? throw new Exception($"'{country}' is not ISO country code")
            : self.WithCountry((ulong)(country[0] << (4 * 2)) + country[1]);

    public static State WithCountry(this State self, ulong country) => (State)(((ulong)self & 0x0000_FFFF_FFFF_FFFF) | (country << (4 * 12)));
    public static ulong CountryCode(this State self) => (ulong)self >> (4 * 12);
    public static string Country(this State self)
    {
        var countryCode = self.CountryCode();

        return Char.ConvertFromUtf32((char)(countryCode & 0xFF00) >> (4 * 2)) + Char.ConvertFromUtf32((char)(countryCode & 0x00FF));
    }
    public static bool IsState(this State self, State state) => ((ulong)self & 0xFFFF_FFFF) == (ulong)state;
    public static State WithState(this State self, State state) => (State)(((ulong)self & 0xFFFF_FFFF_0000_0000) | (ulong)state);
    public static State State(this State self) => (State)((ulong)self & 0xFFFF_FFFF);
}
