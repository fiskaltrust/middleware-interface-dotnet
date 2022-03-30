using System.Runtime.Serialization;
namespace fiskaltrust.ifPOS.v2.me
{
    [DataContract]
    public class CanonicalizationMethodType
    {
        public string Algorithm { get; set; }
    }
}