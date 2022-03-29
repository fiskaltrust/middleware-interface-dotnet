using System.Runtime.Serialization;
namespace fiskaltrust.ifPOS.v2.me
{
    [DataContract]
    public enum InvoiceSType
    {
        CASH,
        NONCASH,
    }
}