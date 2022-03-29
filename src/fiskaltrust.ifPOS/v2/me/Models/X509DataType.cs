using System.Runtime.Serialization;
namespace fiskaltrust.ifPOS.v2.me
{
    [DataContract]
    public class X509DataType
    {
        public object[] Items { get; set; }
        public ItemsChoiceType[] ItemsElementName { get; set; }
    }
}