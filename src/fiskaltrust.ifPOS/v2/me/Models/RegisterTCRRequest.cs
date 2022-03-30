using System.Runtime.Serialization;
namespace fiskaltrust.ifPOS.v2.me
{
    [DataContract]
    public class RegisterTCRRequest
    {
        public RegisterTCRRequestHeaderType Header { get; set; }
        public TCRType TCR { get; set; }
        public SignatureType Signature { get; set; }
    }
}