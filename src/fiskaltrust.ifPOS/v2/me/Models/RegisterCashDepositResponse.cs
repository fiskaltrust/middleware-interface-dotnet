using System.Runtime.Serialization;
namespace fiskaltrust.ifPOS.v2.me
{
    [DataContract]
    public class RegisterCashDepositResponse
    {
        public RegisterCashDepositResponseHeaderType Header { get; set; }
        public string FCDC { get; set; }
        public SignatureType Signature { get; set; }
    }
}