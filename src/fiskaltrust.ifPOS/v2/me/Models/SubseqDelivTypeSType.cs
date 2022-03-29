using System.Runtime.Serialization;

namespace fiskaltrust.ifPOS.v2.me
{
    [DataContract]
    public enum SubseqDelivTypeSType
    {
        NOINTERNET,
        BOUNDBOOK,
        SERVICE,
        TECHNICALERROR,
        BUSINESSNEEDS,
    }
}