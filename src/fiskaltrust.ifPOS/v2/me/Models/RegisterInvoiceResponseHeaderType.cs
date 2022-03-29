using System;
using System.Runtime.Serialization;
namespace fiskaltrust.ifPOS.v2.me
{
    [DataContract]
    public class RegisterInvoiceResponseHeaderType
    {
        public string UUID { get; set; }
        public string RequestUUID { get; set; }
        public DateTime SendDateTime { get; set; }
    }
}