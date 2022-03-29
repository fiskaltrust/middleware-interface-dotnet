using System.Runtime.Serialization;
namespace fiskaltrust.ifPOS.v2.me
{
    [DataContract]
    public enum InvoiceTSType
    {
        INVOICE,
        CORRECTIVE,
        SUMMARY,
        PERIODICAL,
        ADVANCE,
        CREDIT_NOTE,
    }
}