using System.Runtime.Serialization;
namespace fiskaltrust.ifPOS.v2.me
{
    [DataContract]
    public enum ExemptFromVATSType
    {
        VAT_CL3,
        VAT_CL4,
        VAT_CL14,
        VAT_CL15,
        VAT_CL17,
        VAT_CL20,
        VAT_CL26,
        VAT_CL27,
        VAT_CL28,
        VAT_CL29,
        VAT_CL30,
        VAT_CL44,
    }
}