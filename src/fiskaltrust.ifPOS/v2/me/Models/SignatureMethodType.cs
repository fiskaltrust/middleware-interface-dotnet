using System.Runtime.Serialization;

#nullable enable
namespace fiskaltrust.ifPOS.v2.me
{
    [DataContract]
    public class SignatureMethodType
    {
        public string? HMACOutputLength { get; set; }
        public string Algorithm { get; set; }
    }
}