using System;
using System.Runtime.Serialization;
namespace fiskaltrust.ifPOS.v2.me
{
    [DataContract]
    public class BadDebtInvType
    {
        public string IICRef { get; set; }
        public DateTime IssueDateTime { get; set; }
    }
}