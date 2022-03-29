using System.Runtime.Serialization;
namespace fiskaltrust.ifPOS.v2.me
{
    [DataContract]
    public class SPKIDataType
    {
        public byte[][] SPKISexp { get; set; }
        public System.Xml.XmlElement Any { get; set; }
    }
}