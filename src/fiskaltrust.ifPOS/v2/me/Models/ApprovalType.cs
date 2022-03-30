using System.Runtime.Serialization;
namespace fiskaltrust.ifPOS.v2.me
{
    [DataContract]
    public class ApprovalType
    {
        public decimal? DiscountAmt { get; set; }
        public decimal? ReturnAmt { get; set; }
        public decimal? VATRate { get; set; }
        public decimal? ExemptFromVAT { get; set; }
        public decimal? VATAmt { get; set; }
        public decimal? TotalAmt { get; set; }
    }
}