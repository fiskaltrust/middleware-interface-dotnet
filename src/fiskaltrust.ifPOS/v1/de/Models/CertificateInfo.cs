using System.Runtime.Serialization;

namespace fiskaltrust.ifPOS.v1.de
{
    [DataContract]
    public class CertificateInfo
    {
        [DataMember(Order = 1)]
        public string FileName { get; set; }

        [DataMember(Order = 2)]
        public string CertificateContentBase64 { get; set; }
    }
}
