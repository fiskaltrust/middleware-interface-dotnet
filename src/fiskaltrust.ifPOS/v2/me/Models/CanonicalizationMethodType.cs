using System.Runtime.Serialization;
namespace fiskaltrust.ifPOS.v2.me
{
    [DataContract]
    public class CanonicalizationMethodType
    {
        public System.Xml.XmlNode[] Any { get; set; }
        public string Algorithm { get; set; }
    }
}