using System.Runtime.Serialization;
namespace fiskaltrust.ifPOS.v2.me
{
    [DataContract]
    public class RegisterCashDepositRequest
    {
        public RegisterCashDepositRequestHeaderType Header { get; set; }
        public CashDepositType CashDeposit { get; set; }
        public SignatureType Signature { get; set; }
        public string Id { get; set; } = "Request";
        public int Version { get; set; } = 1;
    }
}