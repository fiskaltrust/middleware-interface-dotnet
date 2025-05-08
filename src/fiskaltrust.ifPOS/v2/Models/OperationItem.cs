using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace fiskaltrust.Middleware.ifPOS.v2.Models
{
    public class OperationItem : ICloneable
    {
        [JsonProperty("cbOperationItemID")]
        [DataMember(EmitDefaultValue = true, IsRequired = true)]
        public Guid cbOperationItemID { get; set; }

        [JsonProperty("ftQueueID")]
        [DataMember(EmitDefaultValue = true, IsRequired = true)]
        public Guid ftQueueID { get; set; }

        [JsonProperty("ftPosSystemID")]
        [DataMember(EmitDefaultValue = false, IsRequired = false)]
        public Guid? ftPosSystemID { get; set; }

        [JsonProperty("cbTerminalID")]
        [DataMember(EmitDefaultValue = false, IsRequired = false)]
        public string? cbTerminalID { get; set; }

        [JsonProperty("Method")]
        [DataMember(EmitDefaultValue = true, IsRequired = true)]
        public string Method { get; set; } = string.Empty;

        [JsonProperty("Path")]
        [DataMember(EmitDefaultValue = true, IsRequired = true)]
        public string Path { get; set; } = string.Empty;

        [JsonProperty("RequestHeaders")]
        [DataMember(EmitDefaultValue = true, IsRequired = true)]
        public Dictionary<string, string[]> RequestHeaders { get; set; } = new Dictionary<string, string[]>();

        [JsonProperty("Request")]
        [DataMember(EmitDefaultValue = true, IsRequired = true)]
        public string Request { get; set; } = string.Empty;

        [JsonProperty("Response")]
        [DataMember(EmitDefaultValue = false, IsRequired = false)]
        public string? Response { get; set; }

        [JsonProperty("ResponseCode")]
        [DataMember(EmitDefaultValue = false, IsRequired = false)]
        public int? ResponseCode { get; set; }

        [JsonProperty("LastState")]
        [DataMember(EmitDefaultValue = true, IsRequired = true)]
        public string LastState { get; set; } = string.Empty;

        [JsonProperty("TimeStamp")]
        [DataMember(EmitDefaultValue = true, IsRequired = true)]
        public DateTimeOffset TimeStamp { get; set; } = DateTimeOffset.UtcNow;

        [JsonProperty("ftOperationItemMoment")]
        [DataMember(EmitDefaultValue = true, IsRequired = true)]
        public DateTimeOffset ftOperationItemMoment { get; set; } = DateTimeOffset.UtcNow;

        #region ICloneable Members
        public object Clone() => MemberwiseClone() as OperationItem;
        #endregion
    }
}