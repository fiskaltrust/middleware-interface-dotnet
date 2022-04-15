using System.Runtime.Serialization;

namespace fiskaltrust.ifPOS.v2.at
{
    [DataContract]
    public class CertificateResponse
    {
        [DataMember(Order = 10)]
        public byte[] Certificate { get; set; }
    }
}