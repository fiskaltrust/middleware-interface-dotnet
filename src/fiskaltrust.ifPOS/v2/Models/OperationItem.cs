using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
#if NETSTANDARD2_1
using System.Text.Json.Serialization;
#endif

namespace fiskaltrust.ifPOS.v2
{
    public class OperationItem : ICloneable
    {
#if NETSTANDARD2_1
        [JsonPropertyName("cbOperationItemID")]
#endif
        [DataMember(EmitDefaultValue = true, IsRequired = true)]
        public Guid cbOperationItemID { get; set; }

#if NETSTANDARD2_1
        [JsonPropertyName("ftQueueID")]
#endif
        [DataMember(EmitDefaultValue = true, IsRequired = true)]
        public Guid ftQueueID { get; set; }

#if NETSTANDARD2_1
        [JsonPropertyName("ftPosSystemID")]
#endif
        [DataMember(EmitDefaultValue = false, IsRequired = false)]
        public Guid? ftPosSystemID { get; set; }

#if NETSTANDARD2_1
        [JsonPropertyName("cbTerminalID")]
#endif
        [DataMember(EmitDefaultValue = false, IsRequired = false)]
        public string? cbTerminalID { get; set; }

#if NETSTANDARD2_1
        [JsonPropertyName("Method")]
#endif
        [DataMember(EmitDefaultValue = true, IsRequired = true)]
        public string Method { get; set; } = string.Empty;

#if NETSTANDARD2_1
        [JsonPropertyName("Path")]
#endif
        [DataMember(EmitDefaultValue = true, IsRequired = true)]
        public string Path { get; set; } = string.Empty;

#if NETSTANDARD2_1
        [JsonPropertyName("RequestHeaders")]
#endif
        [DataMember(EmitDefaultValue = true, IsRequired = true)]
        public Dictionary<string, string[]> RequestHeaders { get; set; } = new Dictionary<string, string[]>();

#if NETSTANDARD2_1
        [JsonPropertyName("Request")]
#endif
        [DataMember(EmitDefaultValue = true, IsRequired = true)]
        public string Request { get; set; } = string.Empty;

#if NETSTANDARD2_1
        [JsonPropertyName("Response")]
#endif
        [DataMember(EmitDefaultValue = false, IsRequired = false)]
        public string? Response { get; set; }

#if NETSTANDARD2_1
        [JsonPropertyName("ResponseCode")]
#endif
        [DataMember(EmitDefaultValue = false, IsRequired = false)]
        public int? ResponseCode { get; set; }

#if NETSTANDARD2_1
        [JsonPropertyName("LastState")]
#endif
        [DataMember(EmitDefaultValue = true, IsRequired = true)]
        public string LastState { get; set; } = string.Empty;

#if NETSTANDARD2_1
        [JsonPropertyName("TimeStamp")]
#endif
        [DataMember(EmitDefaultValue = true, IsRequired = true)]
        public DateTimeOffset TimeStamp { get; set; } = DateTimeOffset.UtcNow;

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