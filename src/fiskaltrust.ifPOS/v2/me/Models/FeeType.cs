using System.Runtime.Serialization;
namespace fiskaltrust.ifPOS.v2.me
{
    [DataContract]
    public class FeeType
    {
        public FeeTypeSType Type { get; set; }
        public decimal Amt { get; set; }
    }
}