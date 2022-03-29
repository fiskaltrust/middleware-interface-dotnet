using System;
using System.Runtime.Serialization;

namespace fiskaltrust.ifPOS.v2.me
{
    [DataContract]
    public class RegisterInvoiceRequestHeaderType // TODO: Pull into me SCU?
    {
        public string UUID { get; set; }

        public DateTime SendDateTime { get; set; }

        public SubseqDelivTypeSType SubseqDelivType { get; set; }

        public bool SubseqDelivTypeSpecified { get; set; }
    }
}