using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace fiskaltrust.Middleware.ifPOS.v2.Models
{
    public class OperationItem : ICloneable
    {
        [JsonPropertyName("cbOperationItemID")]
        [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
        [DataMember(EmitDefaultValue = true, IsRequired = true)]
        public Guid cbOperationItemID { get; set; }

        [JsonPropertyName("ftQueueID")]
        [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
        [DataMember(EmitDefaultValue = true, IsRequired = true)]
        public Guid ftQueueID { get; set; }

        [JsonPropertyName("ftPosSystemID")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        [DataMember(EmitDefaultValue = false, IsRequired = false)]
        public Guid? ftPosSystemID { get; set; }

        [JsonPropertyName("cbTerminalID")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        [DataMember(EmitDefaultValue = false, IsRequired = false)]
        public string? cbTerminalID { get; set; }

        [JsonPropertyName("Method")]
        [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
        [DataMember(EmitDefaultValue = true, IsRequired = true)]
        public string Method { get; set; } = string.Empty;

        [JsonPropertyName("Path")]
        [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
        [DataMember(EmitDefaultValue = true, IsRequired = true)]
        public string Path { get; set; } = string.Empty;

        [JsonPropertyName("RequestHeaders")]
        [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
        [DataMember(EmitDefaultValue = true, IsRequired = true)]
        public Dictionary<string, string[]> RequestHeaders { get; set; } = new Dictionary<string, string[]>();

        [JsonPropertyName("Request")]
        [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
        [DataMember(EmitDefaultValue = true, IsRequired = true)]
        public string Request { get; set; } = string.Empty;

        [JsonPropertyName("Response")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        [DataMember(EmitDefaultValue = false, IsRequired = false)]
        public string? Response { get; set; }

        [JsonPropertyName("ResponseCode")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        [DataMember(EmitDefaultValue = false, IsRequired = false)]
        public int? ResponseCode { get; set; }

        [JsonPropertyName("LastState")]
        [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
        [DataMember(EmitDefaultValue = true, IsRequired = true)]
        public string LastState { get; set; } = string.Empty;

        [JsonPropertyName("TimeStamp")]
        [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
        [DataMember(EmitDefaultValue = true, IsRequired = true)]
        public DateTimeOffset TimeStamp { get; set; } = DateTimeOffset.UtcNow;

        [JsonPropertyName("ftOperationItemMoment")]
        [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
        [DataMember(EmitDefaultValue = true, IsRequired = true)]
        public DateTimeOffset ftOperationItemMoment { get; set; } = DateTimeOffset.UtcNow;

        #region ICloneable Members
        public object Clone() => MemberwiseClone() as OperationItem;
        #endregion
    }
}