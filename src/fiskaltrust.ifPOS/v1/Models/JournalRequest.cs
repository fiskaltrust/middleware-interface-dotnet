using System.Collections.Generic;
using System.Runtime.Serialization;

namespace fiskaltrust.ifPOS.v1
{
    [DataContract]
    public class JournalRequest
    {
        [DataMember(Order = 1)]
        public long ftJournalType { get; set; }

        [DataMember(Order = 2)]
        public long From { get; set; }

        [DataMember(Order = 3)]
        public long To { get; set; }
    }
}
