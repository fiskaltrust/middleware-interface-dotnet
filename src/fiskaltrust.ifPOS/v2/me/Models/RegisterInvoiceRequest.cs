using System.Runtime.Serialization;

namespace fiskaltrust.ifPOS.v2.me
{
    [DataContract]
    public class RegisterInvoiceRequest
    {
        public RegisterInvoiceRequestHeaderType Header { get; set; }

        public InvoiceType Invoice { get; set; }

        public SignatureType Signature { get; set; }

        public string Id { get; set; }  = "Request"; // TODO: Generalize. fixed to "Request"

        public int Version { get; set; } = 1; // TODO: Generalize. fixed to  1
    }
}