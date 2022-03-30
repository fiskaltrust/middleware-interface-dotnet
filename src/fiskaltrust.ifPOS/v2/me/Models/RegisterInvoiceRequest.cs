using System.Runtime.Serialization;

namespace fiskaltrust.ifPOS.v2.me
{
    [DataContract]
    public class RegisterInvoiceRequest
    {
        public RegisterInvoiceRequestHeaderType Header { get; set; }
        public InvoiceType Invoice { get; set; }
        public SignatureType Signature { get; set; }
    }
}