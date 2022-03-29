using System.Runtime.Serialization;
namespace fiskaltrust.ifPOS.v2.me
{
    [DataContract]
    public class SellerType
    {
        public IDTypeSType IDType { get; set; }
        public string IDNum { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Town { get; set; }
        public CountryCodeSType Country { get; set; }
        public bool CountrySpecified { get; set; }
    }
}