using System;
using System.Runtime.Serialization;

namespace fiskaltrust.ifPOS.v1.de
{
    [DataContract]
    public class TseLogData
    {
        [DataMember(Order = 1)]
        public string Operation { get; set; }

        [DataMember(Order = 2)]
        public DateTime TimeStamp { get; set; }

        [DataMember(Order = 3)]
        public string TimeStampFormat { get; set; }
    }
}
