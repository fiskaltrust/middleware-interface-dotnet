using System.Runtime.Serialization;

namespace fiskaltrust.ifPOS.v1.it
{
    [DataContract]
    public class StartExportSessionResponse
    {
        [DataMember]
        public string TokenId { get; set; }

        [DataMember]
        public string PrinterId { get; set; }
    }
}
