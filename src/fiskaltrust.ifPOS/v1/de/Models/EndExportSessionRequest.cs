using System.Runtime.Serialization;

namespace fiskaltrust.ifPOS.v1.de
{
    [DataContract]
    public class EndExportSessionRequest
    {
        [DataMember(Order = 10)]
        public string TokenId { get; set; }

        [DataMember(Order = 20)]
        public string Sha256ChecksumBase64 { get; set; }

        [DataMember(Order = 30)]
        public bool Erase { get; set; }
    }
}
