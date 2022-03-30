using System.Runtime.Serialization;
namespace fiskaltrust.ifPOS.v2.me
{
    [DataContract]
    public class DigestMethodType
    {
        public string Algorithm { get; set; }
    }
}