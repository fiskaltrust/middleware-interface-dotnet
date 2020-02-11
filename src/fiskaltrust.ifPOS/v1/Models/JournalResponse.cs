using System.Collections.Generic;
using System.Runtime.Serialization;

namespace fiskaltrust.ifPOS.v1
{
    [DataContract]
    public class JournalResponse
    {
        [DataMember(Order = 1)]
        public List<byte> Chunk { get; set; }
    }
}
