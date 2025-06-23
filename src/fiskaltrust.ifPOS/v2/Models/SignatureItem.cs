using System;
using System.Runtime.Serialization;
using fiskaltrust.ifPOS.v2.Cases;
#if !WCF
using System.Text.Json.Serialization;
#endif

namespace fiskaltrust.ifPOS.v2
{
        public class SignatureItem
        {
                [Newtonsoft.Json.JsonProperty("ftSignatureItemId", DefaultValueHandling = Newtonsoft.Json.DefaultValueHandling.Ignore)]
#if !WCF
                [JsonPropertyName("ftSignatureItemId")]
                [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
#endif
                [DataMember(EmitDefaultValue = false, IsRequired = false)]
                public Guid? ftSignatureItemId { get; set; }

                [Newtonsoft.Json.JsonProperty("ftSignatureFormat", DefaultValueHandling = Newtonsoft.Json.DefaultValueHandling.Include)]
#if !WCF
                [JsonPropertyName("ftSignatureFormat")]
                [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
#endif
                [DataMember(EmitDefaultValue = true, IsRequired = true)]
                public SignatureFormat ftSignatureFormat { get; set; }

                [Newtonsoft.Json.JsonProperty("ftSignatureType", DefaultValueHandling = Newtonsoft.Json.DefaultValueHandling.Include)]
#if !WCF
                [JsonPropertyName("ftSignatureType")]
                [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
#endif
                [DataMember(EmitDefaultValue = true, IsRequired = true)]
                public SignatureType ftSignatureType { get; set; }

                [Newtonsoft.Json.JsonProperty("Caption", DefaultValueHandling = Newtonsoft.Json.DefaultValueHandling.Ignore)]
#if !WCF
                [JsonPropertyName("Caption")]
                [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
#endif
                [DataMember(EmitDefaultValue = false, IsRequired = false)]
                public string? Caption { get; set; }

                [Newtonsoft.Json.JsonProperty("Data", DefaultValueHandling = Newtonsoft.Json.DefaultValueHandling.Include)]
#if !WCF
                [JsonPropertyName("Data")]
                [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
#endif
                [DataMember(EmitDefaultValue = true, IsRequired = true)]
                public string Data { get; set; }
        }
}