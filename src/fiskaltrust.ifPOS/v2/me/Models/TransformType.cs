using System.Runtime.Serialization;
namespace fiskaltrust.ifPOS.v2.me
{
    [DataContract]
    public class TransformType
    {
        public object[] Items { get; set; }
        public string[] Text { get; set; }
        public string Algorithm { get; set; }
    }
}