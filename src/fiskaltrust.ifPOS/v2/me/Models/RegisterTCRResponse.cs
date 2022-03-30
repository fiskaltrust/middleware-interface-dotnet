using System.Runtime.Serialization;
namespace fiskaltrust.ifPOS.v2.me
{
    [DataContract]
    public class RegisterTCRResponse
    {
        public RegisterTCRResponseHeaderType Header { get; set; }
        public string TCRCode { get; set; }
        public SignatureType Signature { get; set; }
    }
}