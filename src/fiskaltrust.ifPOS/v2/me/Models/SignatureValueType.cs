using System.Runtime.Serialization;
namespace fiskaltrust.ifPOS.v2.me
{
    [DataContract]
    public class SignatureValueType
    {
        public string Id { get; set; }
        public byte[] Value { get; set; }
    }
}