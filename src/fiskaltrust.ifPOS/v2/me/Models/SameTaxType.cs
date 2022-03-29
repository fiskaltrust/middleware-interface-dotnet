using System.Runtime.Serialization;
namespace fiskaltrust.ifPOS.v2.me
{
    [DataContract]
    public class SameTaxType
    {
        public int NumOfItems { get; set; }
        public decimal PriceBefVAT { get; set; }
        public decimal VATRate { get; set; }
        public bool VATRateSpecified { get; set; }
        public ExemptFromVATSType ExemptFromVAT { get; set; }
        public bool ExemptFromVATSpecified { get; set; }
        public decimal VATAmt { get; set; }
        public bool VATAmtSpecified { get; set; }
    }
}