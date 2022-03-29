using System.Runtime.Serialization;
namespace fiskaltrust.ifPOS.v2.me
{
    [DataContract]
    public enum IDTypeSType
    {
        TIN,
        ID,
        PASS,
        VAT,
        TAX,
        SOC,
    }
}