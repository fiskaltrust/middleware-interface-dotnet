using System.Runtime.Serialization;
namespace fiskaltrust.ifPOS.v2.me
{
    [DataContract]
    public class RetrievalMethodType
    {
        public TransformType[] Transforms { get; set; }
        public string URI { get; set; }
        public string Type { get; set; }
    }
}