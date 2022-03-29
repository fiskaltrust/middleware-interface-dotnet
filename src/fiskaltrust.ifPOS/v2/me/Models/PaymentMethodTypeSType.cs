using System.Runtime.Serialization;
namespace fiskaltrust.ifPOS.v2.me
{
    [DataContract]
    public enum PaymentMethodTypeSType
    {
        BANKNOTE,
        CARD,
        BUSINESSCARD,
        SVOUCHER,
        COMPANY,
        ORDER,
        ADVANCE,
        ACCOUNT,
        FACTORING,
        OTHER,
        OTHERCASH,
    }
}