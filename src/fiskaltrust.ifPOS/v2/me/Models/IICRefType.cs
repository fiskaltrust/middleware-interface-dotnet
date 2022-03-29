using System;
using System.Runtime.Serialization;
namespace fiskaltrust.ifPOS.v2.me
{
    [DataContract]
    public class IICRefType
    {
        public string IIC { get; set; }
        public DateTime IssueDateTime { get; set; }
        public decimal Amount { get; set; }
    }
}