using System.Runtime.Serialization;
namespace fiskaltrust.ifPOS.v2.me
{
    [DataContract]
    public class SignedInfoType
    {
        public CanonicalizationMethodType CanonicalizationMethod { get; set; }
        public SignatureMethodType SignatureMethod { get; set; }
        public ReferenceType[] Reference { get; set; }
        public string Id { get; set; }
    }
}