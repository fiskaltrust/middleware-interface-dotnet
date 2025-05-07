using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace fiskaltrust.Middleware.ifPOS.v2.Models
{
    public class PayItemCaseData
    {
        [JsonPropertyName("Provider")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [DataMember(EmitDefaultValue = false, IsRequired = false)]
        public PayItemCaseProviderData? Provider { get; set; }

        [JsonPropertyName("Receipt")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [DataMember(EmitDefaultValue = false, IsRequired = false)]
        public List<string>? Receipt { get; set; }
    }

    public class InStoreAppPayItemCaseData
    {
        [JsonPropertyName("Provider")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [DataMember(EmitDefaultValue = false, IsRequired = false)]
        public PayItemCaseProviderDataInStoreApp? Provider { get; set; }

        [JsonPropertyName("Receipt")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [DataMember(EmitDefaultValue = false, IsRequired = false)]
        public List<string>? Receipt { get; set; }
    }
}