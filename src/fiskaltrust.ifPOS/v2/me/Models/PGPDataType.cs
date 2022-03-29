using System.Runtime.Serialization;
namespace fiskaltrust.ifPOS.v2.me
{
    [DataContract]
    public class PGPDataType
    {
        public object[] Items { get; set; }
        public ItemsChoiceType1[] ItemsElementName { get; set; }
    }
}