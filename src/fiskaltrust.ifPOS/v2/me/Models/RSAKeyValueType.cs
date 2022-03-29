using System.Runtime.Serialization;
namespace fiskaltrust.ifPOS.v2.me
{
    [DataContract]
    public class RSAKeyValueType
    {
        public byte[] Modulus { get; set; }
        public byte[] Exponent { get; set; }
    }
}