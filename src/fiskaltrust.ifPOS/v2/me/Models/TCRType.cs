using System;
using System.Runtime.Serialization;

#nullable enable
namespace fiskaltrust.ifPOS.v2.me
{
    [DataContract]
    public class TCRType
    {
        public string IssuerTIN { get; set; }
        public string BusinUnitCode { get; set; }
        public string TCRIntID { get; set; }
        public string? SoftCode { get; set; }
        public string? MaintainerCode { get; set; }
        public DateTime? ValidFrom { get; set; }
        public DateTime? ValidTo { get; set; }
        public TCRSType? Type { get; set; }
        public string? Value { get; set; }
    }
}