using System.Runtime.Serialization;

#nullable enable
namespace fiskaltrust.ifPOS.v2.me
{
    [DataContract]
    public class SignatureType
    {
        public SignedInfoType? SignedInfo { get; set; }
        public SignatureValueType SignatureValue { get; set; }
        public KeyInfoType? KeyInfo { get; set; }
    }
}