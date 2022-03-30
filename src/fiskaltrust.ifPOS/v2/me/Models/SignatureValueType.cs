using System.Runtime.Serialization;
namespace fiskaltrust.ifPOS.v2.me
{
    [DataContract]
    public class SignatureValueType
    {
        public byte[] Value { get; set; }
    }
}