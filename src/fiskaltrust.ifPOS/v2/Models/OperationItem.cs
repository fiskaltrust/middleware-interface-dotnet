using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;

#if NETSTANDARD2_1
using System.Text.Json.Serialization;
#endif

namespace fiskaltrust.Middleware.ifPOS.v2.Models
{
    public class OperationItem : ICloneable
    {
        [JsonProperty("cbOperationItemID")]
#if NETSTANDARD2_1
        [JsonPropertyName("cbOperationItemID")]
#endif
        [DataMember(EmitDefaultValue = true, IsRequired = true)]
        public Guid cbOperationItemID { get; set; }

        [JsonProperty("ftQueueID")]
#if NETSTANDARD2_1
        [JsonPropertyName("ftQueueID")]
#endif
        [DataMember(EmitDefaultValue = true, IsRequired = true)]
        public Guid ftQueueID { get; set; }

        [JsonProperty("ftPosSystemID")]
#if NETSTANDARD2_1
        [JsonPropertyName("ftPosSystemID")]
#endif
        [DataMember(EmitDefaultValue = false, IsRequired = false)]
        public Guid? ftPosSystemID { get; set; }

        [JsonProperty("cbTerminalID")]
#if NETSTANDARD2_1
        [JsonPropertyName("cbTerminalID")]
#endif
        [DataMember(EmitDefaultValue = false, IsRequired = false)]
        public string? cbTerminalID { get; set; }

        [JsonProperty("Method")]
#if NETSTANDARD2_1
        [JsonPropertyName("Method")]
#endif
        [DataMember(EmitDefaultValue = true, IsRequired = true)]
        public string Method { get; set; } = string.Empty;

        [JsonProperty("Path")]
#if NETSTANDARD2_1
        [JsonPropertyName("Path")]
#endif
        [DataMember(EmitDefaultValue = true, IsRequired = true)]
        public string Path { get; set; } = string.Empty;

        [JsonProperty("RequestHeaders")]
#if NETSTANDARD2_1
        [JsonPropertyName("RequestHeaders")]
#endif
        [DataMember(EmitDefaultValue = true, IsRequired = true)]
        public Dictionary<string, string[]> RequestHeaders { get; set; } = new Dictionary<string, string[]>();

        [JsonProperty("Request")]
#if NETSTANDARD2_1
        [JsonPropertyName("Request")]
#endif
        [DataMember(EmitDefaultValue = true, IsRequired = true)]
        public string Request { get; set; } = string.Empty;

        [JsonProperty("Response")]
#if NETSTANDARD2_1
        [JsonPropertyName("Response")]
#endif
        [DataMember(EmitDefaultValue = false, IsRequired = false)]
        public string? Response { get; set; }

        [JsonProperty("ResponseCode")]
#if NETSTANDARD2_1
        [JsonPropertyName("ResponseCode")]
#endif
        [DataMember(EmitDefaultValue = false, IsRequired = false)]
        public int? ResponseCode { get; set; }

        [JsonProperty("LastState")]
#if NETSTANDARD2_1
        [JsonPropertyName("LastState")]
#endif
        [DataMember(EmitDefaultValue = true, IsRequired = true)]
        public string LastState { get; set; } = string.Empty;

        [JsonProperty("TimeStamp")]
#if NETSTANDARD2_1
        [JsonPropertyName("TimeStamp")]
#endif
        [DataMember(EmitDefaultValue = true, IsRequired = true)]
        public DateTimeOffset TimeStamp { get; set; } = DateTimeOffset.UtcNow;

        [JsonProperty("ftOperationItemMoment")]
#if NETSTANDARD2_1
        [JsonPropertyName("ftOperationItemMoment")]
#endif
        [DataMember(EmitDefaultValue = true, IsRequired = true)]
        public DateTimeOffset ftOperationItemMoment { get; set; } = DateTimeOffset.UtcNow;

        #region ICloneable Members
        public object Clone() => MemberwiseClone() as OperationItem;
        #endregion
    }
}