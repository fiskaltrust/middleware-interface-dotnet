using System;

namespace fiskaltrust.ifPOS.v2.Cases;

public enum PayItemCase : ulong
{
    /// <value><c>0x00</c></value>
    UnknownPaymentType = 0x00,
    /// <value><c>0x01</c></value>
    CashPayment = 0x01,
    /// <value><c>0x02</c></value>
    NonCash = 0x02,
    /// <value><c>0x03</c></value>
    CrossedCheque = 0x03,
    /// <value><c>0x04</c></value>
    DebitCardPayment = 0x04,
    /// <value><c>0x05</c></value>
    CreditCardPayment = 0x05,
    /// <value><c>0x06</c></value>
    VoucherPaymentCouponVoucherByMoneyValue = 0x06,
    /// <value><c>0x07</c></value>
    OnlinePayment = 0x07,
    /// <value><c>0x08</c></value>
    LoyaltyProgramCustomerCardPayment = 0x08,
    /// <value><c>0x09</c></value>
    AccountsReceivable = 0x09,
    /// <value><c>0x0A</c></value>
    SEPATransfer = 0x0A,
    /// <value><c>0x0B</c></value>
    OtherBankTransfer = 0x0B,
    /// <value><c>0x0C</c></value>
    TransferToCashbookVaultOwnerEmployee = 0x0C,
    /// <value><c>0x0D</c></value>
    InternalMaterialConsumption = 0x0D,
    /// <value><c>0x0E</c></value>
    Grant = 0x0E,
    /// <value><c>0x0F</c></value>
    TicketRestaurant = 0x0F
}

public static class PayItemCaseExt
{
    public static PayItemCase AsPayItemCase(this ulong self) => (PayItemCase)self;
    public static T As<T>(this PayItemCase self) where T : Enum, IConvertible => (T)Enum.ToObject(typeof(T), self);
    public static PayItemCase Reset(this PayItemCase self) => (PayItemCase)(0xFFFF_F000_0000_0000 & (ulong)self);

    public static PayItemCase WithVersion(this PayItemCase self, byte version) => (PayItemCase)((((ulong)self) & 0xFFFF_0FFF_FFFF_FFFF) | ((ulong)version << (4 * 11)));
    public static byte Version(this PayItemCase self) => (byte)((((ulong)self) >> (4 * 11)) & 0xF);

    public static PayItemCase WithCountry(this PayItemCase self, string country)
        => country?.Length != 2
            ? throw new Exception($"'{country}' is not ISO country code")
            : self.WithCountry((ulong)(country[0] << (4 * 2)) + country[1]);


    public static PayItemCase WithCountry(this PayItemCase self, ulong country) => (PayItemCase)(((ulong)self & 0x0000_FFFF_FFFF_FFFF) | (country << (4 * 12)));
    public static ulong CountryCode(this PayItemCase self) => (ulong)self >> (4 * 12);
#nullable enable
    public static string? Country(this PayItemCase self)
    {
        var countryCode = self.CountryCode();
        if (countryCode == 0)
        {
            return null;
        }
        return Char.ConvertFromUtf32((char)(countryCode & 0xFF00) >> (4 * 2)) + Char.ConvertFromUtf32((char)(countryCode & 0x00FF));
    }
#nullable disable
    public static bool IsCase(this PayItemCase self, PayItemCase @case) => ((ulong)self & 0xFF) == (ulong)@case;
    public static PayItemCase WithCase(this PayItemCase self, PayItemCase state) => (PayItemCase)(((ulong)self & 0xFFFF_FFFF_FFFF_FF00) | (ulong)state);
    public static PayItemCase Case(this PayItemCase self) => (PayItemCase)((ulong)self & 0xFF);
}
