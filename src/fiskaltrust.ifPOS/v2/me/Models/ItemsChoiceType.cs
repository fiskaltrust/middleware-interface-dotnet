using System.Runtime.Serialization;
namespace fiskaltrust.ifPOS.v2.me
{
    [DataContract]
    public enum ItemsChoiceType
    {
        Item,
        X509CRL,
        X509Certificate,
        X509IssuerSerial,
        X509SKI,
        X509SubjectName,
    }
}