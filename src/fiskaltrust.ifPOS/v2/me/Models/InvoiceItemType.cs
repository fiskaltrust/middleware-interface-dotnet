using System;
using System.Runtime.Serialization;

#nullable enable
namespace fiskaltrust.ifPOS.v2.me
{
    [DataContract]
    public class InvoiceItemType
    {
        public string N { get; set; }
        public string? C { get; set; }
        public string U { get; set; }
        public double Q { get; set; }
        public decimal UPB { get; set; }
        public decimal UPA { get; set; }
        public decimal? R { get; set; }
        public bool? RR { get; set; }
        public decimal PB { get; set; }
        public decimal? VR { get; set; }
        public decimal? VA { get; set; }
        public bool? IN { get; set; }
        public decimal PA { get; set; }
        public ExemptFromVATSType? EX { get; set; }
        public DateTime? VD { get; set; }
        public decimal? VN { get; set; }
        public string? VSN { get; set; }
    }
}