using System.Runtime.Serialization;
namespace fiskaltrust.ifPOS.v2.me
{
    [DataContract]
    public class CurrencyType
    {
        public CurrencyCodeSType Code { get; set; }
        public double ExRate { get; set; }
    }
}