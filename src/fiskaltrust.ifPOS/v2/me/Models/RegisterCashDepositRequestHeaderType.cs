using System;
using System.Runtime.Serialization;

namespace fiskaltrust.ifPOS.v2.me
{
    [DataContract]
    public class RegisterCashDepositRequestHeaderType
    {
        public string UUID { get; set; }

        public DateTime SendDateTime { get; set; }

        public SubseqDelivTypeSType SubseqDelivType { get; set; }

        public bool SubseqDelivTypeSpecified { get; set; }
    }
}