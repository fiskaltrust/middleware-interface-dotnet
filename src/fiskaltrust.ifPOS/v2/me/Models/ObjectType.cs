using System.Runtime.Serialization;
namespace fiskaltrust.ifPOS.v2.me
{
    [DataContract]
    public class ObjectType
    {
        public System.Xml.XmlNode[] Any { get; set; }
        public string Id { get; set; }
        public string MimeType { get; set; }
        public string Encoding { get; set; }
    }
}