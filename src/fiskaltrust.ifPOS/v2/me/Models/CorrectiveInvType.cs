using System;
using System.Runtime.Serialization;
namespace fiskaltrust.ifPOS.v2.me
{
    [DataContract]
    public class CorrectiveInvType
    {
        public string IICRef { get; set; }
        public DateTime IssueDateTime { get; set; }
        public CorrectiveInvTypeSType Type { get; set; }
    }
}