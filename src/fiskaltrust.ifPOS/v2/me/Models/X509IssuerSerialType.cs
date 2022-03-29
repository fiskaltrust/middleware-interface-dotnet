using System.Runtime.Serialization;
namespace fiskaltrust.ifPOS.v2.me
{
    [DataContract]
    public class X509IssuerSerialType
    {
        public string X509IssuerName { get; set; }
        public string X509SerialNumber { get; set; }
    }
}