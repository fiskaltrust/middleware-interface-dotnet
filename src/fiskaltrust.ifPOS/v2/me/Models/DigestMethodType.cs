using System.Runtime.Serialization;
namespace fiskaltrust.ifPOS.v2.me
{
    [DataContract]
    public class DigestMethodType
    {
        public System.Xml.XmlNode[] Any { get; set; }
        public string Algorithm { get; set; }
    }
}