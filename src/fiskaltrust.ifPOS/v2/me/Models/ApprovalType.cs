using System.Runtime.Serialization;
namespace fiskaltrust.ifPOS.v2.me
{
    [DataContract]
    public class ApprovalType
    {
        public decimal DiscountAmt { get; set; }
        public bool DiscountAmtSpecified { get; set; }
        public decimal ReturnAmt { get; set; }
        public bool ReturnAmtSpecified { get; set; }
        public decimal VATRate { get; set; }
        public bool VATRateSpecified { get; set; }
        public decimal ExemptFromVAT { get; set; }
        public bool ExemptFromVATSpecified { get; set; }
        public decimal VATAmt { get; set; }
        public bool VATAmtSpecified { get; set; }
        public decimal TotalAmt { get; set; }
    }
}