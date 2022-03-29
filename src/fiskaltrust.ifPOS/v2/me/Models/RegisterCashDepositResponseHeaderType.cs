using System;
using System.Runtime.Serialization;

namespace fiskaltrust.ifPOS.v2.me
{
    [DataContract]
    public class RegisterCashDepositResponseHeaderType // TODO: Pull into me SCU?
    {
        public string UUID { get; set; }

        public string RequestUUID { get; set; }

        public DateTime SendDateTime { get; set; }
    }
}