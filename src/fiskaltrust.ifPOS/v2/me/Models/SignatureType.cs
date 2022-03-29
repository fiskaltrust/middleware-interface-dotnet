using System.Runtime.Serialization;
namespace fiskaltrust.ifPOS.v2.me
{
    [DataContract]
    public class SignatureType
    {
        public SignedInfoType SignedInfo { get; set; }
        public SignatureValueType SignatureValue { get; set; }
        public KeyInfoType KeyInfo { get; set; }
        public ObjectType[] Object { get; set; }
        public string Id { get; set; }
    }
}