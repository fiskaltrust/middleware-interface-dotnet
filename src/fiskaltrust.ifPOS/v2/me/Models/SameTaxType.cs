using System.Runtime.Serialization;
namespace fiskaltrust.ifPOS.v2.me
{
    [DataContract]
    public class SameTaxType
    {
        public int NumOfItems { get; set; }
        public decimal PriceBefVAT { get; set; }
        public decimal? VATRate { get; set; }
        public ExemptFromVATSType? ExemptFromVAT { get; set; }
        public decimal? VATAmt { get; set; }
    }
}