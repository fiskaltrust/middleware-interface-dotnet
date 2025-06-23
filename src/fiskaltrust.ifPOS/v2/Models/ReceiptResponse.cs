using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using fiskaltrust.ifPOS.v2.Cases;
#if !WCF
using System.Text.Json.Serialization;
#endif

namespace fiskaltrust.ifPOS.v2
{
#nullable enable
        public class ReceiptResponse
        {
                [Newtonsoft.Json.JsonProperty("ftQueueID", DefaultValueHandling = Newtonsoft.Json.DefaultValueHandling.Include)]
#if !WCF
                [JsonPropertyName("ftQueueID")]
                [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
#endif
                [DataMember(EmitDefaultValue = true, IsRequired = true)]
                public Guid ftQueueID { get; set; }

                [Newtonsoft.Json.JsonProperty("ftQueueItemID", DefaultValueHandling = Newtonsoft.Json.DefaultValueHandling.Include)]
#if !WCF
                [JsonPropertyName("ftQueueItemID")]
                [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
#endif
                [DataMember(EmitDefaultValue = true, IsRequired = true)]
                public Guid ftQueueItemID { get; set; }

                [Newtonsoft.Json.JsonProperty("ftQueueRow", DefaultValueHandling = Newtonsoft.Json.DefaultValueHandling.Include)]
#if !WCF
                [JsonPropertyName("ftQueueRow")]
                [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
#endif
                [DataMember(EmitDefaultValue = true, IsRequired = true)]
                public long ftQueueRow { get; set; }

                [Newtonsoft.Json.JsonProperty("ftCashBoxIdentification", DefaultValueHandling = Newtonsoft.Json.DefaultValueHandling.Include)]
#if !WCF
                [JsonPropertyName("ftCashBoxIdentification")]
                [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
#endif
                [DataMember(EmitDefaultValue = true, IsRequired = true)]
                public string ftCashBoxIdentification { get; set; }

                [Newtonsoft.Json.JsonProperty("ftCashBoxID", DefaultValueHandling = Newtonsoft.Json.DefaultValueHandling.Include)]
#if !WCF
                [JsonPropertyName("ftCashBoxID")]
                [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
#endif
                [DataMember(EmitDefaultValue = true, IsRequired = true)]
                public Guid? ftCashBoxID { get; set; }

                [Newtonsoft.Json.JsonProperty("cbTerminalID", DefaultValueHandling = Newtonsoft.Json.DefaultValueHandling.Ignore)]
#if !WCF
                [JsonPropertyName("cbTerminalID")]
                [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
#endif
                [DataMember(EmitDefaultValue = true, IsRequired = true)]
                public string? cbTerminalID { get; set; }

                [Newtonsoft.Json.JsonProperty("cbReceiptReference")]
#if !WCF
                [JsonPropertyName("cbReceiptReference")]
#endif
                [DataMember(EmitDefaultValue = true, IsRequired = true)]
                public string? cbReceiptReference { get; set; }

                [Newtonsoft.Json.JsonProperty("ftReceiptIdentification", DefaultValueHandling = Newtonsoft.Json.DefaultValueHandling.Include)]
#if !WCF
                [JsonPropertyName("ftReceiptIdentification")]
                [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
#endif
                [DataMember(EmitDefaultValue = true, IsRequired = true)]
                public string ftReceiptIdentification { get; set; }

                [Newtonsoft.Json.JsonProperty("ftReceiptMoment", DefaultValueHandling = Newtonsoft.Json.DefaultValueHandling.Include)]
#if !WCF
                [JsonPropertyName("ftReceiptMoment")]
                [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
#endif
                [DataMember(EmitDefaultValue = true, IsRequired = true)]
                public DateTime ftReceiptMoment { get; set; }

                [Newtonsoft.Json.JsonProperty("ftReceiptHeader", DefaultValueHandling = Newtonsoft.Json.DefaultValueHandling.Ignore)]
#if !WCF
                [JsonPropertyName("ftReceiptHeader")]
                [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
#endif
                [DataMember(EmitDefaultValue = true, IsRequired = true)]
                public List<string>? ftReceiptHeader { get; set; }

                [Newtonsoft.Json.JsonProperty("ftChargeItems", DefaultValueHandling = Newtonsoft.Json.DefaultValueHandling.Ignore)]
#if !WCF
                [JsonPropertyName("ftChargeItems")]
                [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
#endif
                [DataMember(EmitDefaultValue = true, IsRequired = true)]
                public List<ChargeItem>? ftChargeItems { get; set; }

                [Newtonsoft.Json.JsonProperty("ftChargeLines", DefaultValueHandling = Newtonsoft.Json.DefaultValueHandling.Ignore)]
#if !WCF
                [JsonPropertyName("ftChargeLines")]
                [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
#endif
                [DataMember(EmitDefaultValue = true, IsRequired = true)]
                public List<string>? ftChargeLines { get; set; }

                [Newtonsoft.Json.JsonProperty("ftPayItems", DefaultValueHandling = Newtonsoft.Json.DefaultValueHandling.Ignore)]
#if !WCF
                [JsonPropertyName("ftPayItems")]
                [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
#endif
                [DataMember(EmitDefaultValue = true, IsRequired = true)]
                public List<PayItem>? ftPayItems { get; set; }

                [Newtonsoft.Json.JsonProperty("ftPayLines", DefaultValueHandling = Newtonsoft.Json.DefaultValueHandling.Ignore)]
#if !WCF
                [JsonPropertyName("ftPayLines")]
                [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
#endif
                [DataMember(EmitDefaultValue = true, IsRequired = true)]
                public List<string>? ftPayLines { get; set; }

                [Newtonsoft.Json.JsonProperty("ftSignatures", DefaultValueHandling = Newtonsoft.Json.DefaultValueHandling.Ignore)]
#if !WCF
                [JsonPropertyName("ftSignatures")]
                [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
#endif
                [DataMember(EmitDefaultValue = true, IsRequired = true)]
                public List<SignatureItem> ftSignatures { get; set; } = new List<SignatureItem>();

                [Newtonsoft.Json.JsonProperty("ftReceiptFooter", DefaultValueHandling = Newtonsoft.Json.DefaultValueHandling.Ignore)]
#if !WCF
                [JsonPropertyName("ftReceiptFooter")]
                [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
#endif
                [DataMember(EmitDefaultValue = true, IsRequired = true)]
                public List<string>? ftReceiptFooter { get; set; }

                [Newtonsoft.Json.JsonProperty("ftState", DefaultValueHandling = Newtonsoft.Json.DefaultValueHandling.Include)]
#if !WCF
                [JsonPropertyName("ftState")]
                [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
#endif
                [DataMember(EmitDefaultValue = true, IsRequired = true)]
                public State ftState { get; set; }

                [Newtonsoft.Json.JsonProperty("ftStateData", DefaultValueHandling = Newtonsoft.Json.DefaultValueHandling.Ignore)]
#if !WCF
                [JsonPropertyName("ftStateData")]
                [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
#endif
                [DataMember(EmitDefaultValue = true, IsRequired = true)]
                public object? ftStateData { get; set; }
        }
}