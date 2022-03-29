using System;
using System.Runtime.Serialization;

namespace fiskaltrust.ifPOS.v2.me
{
    [DataContract]
    public class CashDepositType
    {
        public DateTime ChangeDateTime { get; set; }

        public CashDepositOperationSType Operation { get; set; }

        public decimal CashAmt { get; set; }

        public string TCRCode { get; set; }

        public string IssuerTIN { get; set; }
    }
}