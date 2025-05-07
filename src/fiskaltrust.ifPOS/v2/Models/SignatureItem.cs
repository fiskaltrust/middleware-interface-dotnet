using System;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace fiskaltrust.Middleware.ifPOS.v2.Models
{
    public class SignatureItem
    {
        [JsonPropertyName("ftSignatureItemId")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        [DataMember(EmitDefaultValue = false, IsRequired = false)]
        public Guid? ftSignatureItemId { get; set; }

        [JsonPropertyName("ftSignatureFormat")]
        [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
        [DataMember(EmitDefaultValue = true, IsRequired = true)]
        public required ulong ftSignatureFormat { get; set; } = 0;

        [JsonPropertyName("ftSignatureType")]
        [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
        [DataMember(EmitDefaultValue = true, IsRequired = true)]
        public required ulong ftSignatureType { get; set; } = 0;

        [JsonPropertyName("Caption")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        [DataMember(EmitDefaultValue = false, IsRequired = false)]
        public string? Caption { get; set; }

        [JsonPropertyName("Data")]
        [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
        [DataMember(EmitDefaultValue = true, IsRequired = true)]
        public required object Data { get; set; }
    }
}