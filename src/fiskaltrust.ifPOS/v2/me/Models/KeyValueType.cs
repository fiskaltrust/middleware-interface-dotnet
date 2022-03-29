using System.Runtime.Serialization;
namespace fiskaltrust.ifPOS.v2.me
{
    [DataContract]
    public class KeyValueType
    {
        public object Item { get; set; }
        public string[] Text { get; set; }
    }
}