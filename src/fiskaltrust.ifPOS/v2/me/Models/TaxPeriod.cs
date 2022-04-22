#nullable enable
using System.Runtime.Serialization;

namespace fiskaltrust.ifPOS.v2.me
{
    [DataContract]
    public class TaxPeriod
    {
        [DataMember(Order = 10)]
        public uint Year { get; set; }
        [DataMember(Order = 20)]
        public uint Month { get; set; }
    }
}
