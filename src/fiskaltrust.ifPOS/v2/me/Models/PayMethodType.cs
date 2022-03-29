using System.Runtime.Serialization;
namespace fiskaltrust.ifPOS.v2.me
{
    [DataContract]
    public class PayMethodType
    {
        public VoucherType[] Vouchers { get; set; }
        public PaymentMethodTypeSType Type { get; set; }
        public decimal Amt { get; set; }
        public string CompCard { get; set; }
        public string AdvIIC { get; set; }
        public string BankAcc { get; set; }
    }
}