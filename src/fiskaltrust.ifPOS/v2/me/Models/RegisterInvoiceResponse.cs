using System.Runtime.Serialization;
namespace fiskaltrust.ifPOS.v2.me
{
    [DataContract]
    public class RegisterInvoiceResponse
    {
        public RegisterInvoiceResponseHeaderType Header { get; set; }
        public string FIC { get; set; }
        public SignatureType Signature { get; set; }
    }
}