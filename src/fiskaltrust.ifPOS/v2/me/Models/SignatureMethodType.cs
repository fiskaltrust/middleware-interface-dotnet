using System.Runtime.Serialization;
namespace fiskaltrust.ifPOS.v2.me
{
    [DataContract]
    public class SignatureMethodType
    {
        public string HMACOutputLength { get; set; }
        public System.Xml.XmlNode[] Any { get; set; }
        public string Algorithm { get; set; }
    }
}