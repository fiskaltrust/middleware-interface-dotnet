using System.Runtime.Serialization;

namespace fiskaltrust.ifPOS.v1.de
{
    [DataContract]
    public class TseSignatureData
    {
        [DataMember(Order = 1)]
        public ulong SignatureCounter { get; set; }

        [DataMember(Order = 2)]
        public string SignatureAlgorithm { get; set; }

        [DataMember(Order = 3)]
        public string SignatureBase64 { get; set; }

        [DataMember(Order = 4)]
        public string PublicKeyBase64 { get; set; }
    }
}
