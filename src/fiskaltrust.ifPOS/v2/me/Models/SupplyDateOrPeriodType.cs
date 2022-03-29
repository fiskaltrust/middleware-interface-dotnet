using System;
using System.Runtime.Serialization;
namespace fiskaltrust.ifPOS.v2.me
{
    [DataContract]
    public class SupplyDateOrPeriodType
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
}