using System.Runtime.Serialization;
namespace fiskaltrust.ifPOS.v2.me
{
    [DataContract]
    public class KeyInfoType
    {
        public object[] Items { get; set; }
        public ItemsChoiceType2[] ItemsElementName { get; set; }
        public string[] Text { get; set; }
    }
}