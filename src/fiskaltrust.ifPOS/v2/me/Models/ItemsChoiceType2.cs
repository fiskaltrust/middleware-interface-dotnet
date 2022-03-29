using System.Runtime.Serialization;
namespace fiskaltrust.ifPOS.v2.me
{
    [DataContract]
    public enum ItemsChoiceType2
    {
        Item,
        KeyName,
        KeyValue,
        MgmtData,
        PGPData,
        RetrievalMethod,
        SPKIData,
        X509Data,
    }
}