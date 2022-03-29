using System.Runtime.Serialization;
namespace fiskaltrust.ifPOS.v2.me
{
    [DataContract]
    public enum SelfIssSType
    {
        AGREEMENT,
        DOMESTIC,
        ABROAD,
        SELF,
        OTHER,
    }
}