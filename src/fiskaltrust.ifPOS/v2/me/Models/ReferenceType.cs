using System.Runtime.Serialization;
namespace fiskaltrust.ifPOS.v2.me
{
    [DataContract]
    public class ReferenceType
    {
        public TransformType[] Transforms { get; set; }
        public DigestMethodType DigestMethod { get; set; }
        public byte[] DigestValue { get; set; }
        public string URI { get; set; }
    }
}