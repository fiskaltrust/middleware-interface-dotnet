using System;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using fiskaltrust.ifPOS.v2.Cases;

#if NETSTANDARD2_1
using System.Text.Json.Serialization;
#endif

namespace fiskaltrust.ifPOS.v2
{
    public class PayItem
    {
#if NETSTANDARD2_1
        [JsonPropertyName("ftPayItemId")]
#endif
        [DataMember(EmitDefaultValue = false, IsRequired = false)]
        public Guid? ftPayItemId { get; set; }

        private decimal _quantity = 1;

#if NETSTANDARD2_1
        [JsonPropertyName("Quantity")]
#endif
        [DataMember(EmitDefaultValue = true, IsRequired = true)]
        public decimal? Quantity
        {
            get => _quantity == 1 ? null : _quantity;
            set => _quantity = value ?? 1;
        }

#if NETSTANDARD2_1
        [JsonPropertyName("Description")]
#endif
        [DataMember(EmitDefaultValue = true, IsRequired = true)]
        public string Description { get; set; }

#if NETSTANDARD2_1
        [JsonPropertyName("Amount")]
#endif
        [DataMember(EmitDefaultValue = true, IsRequired = true)]
        public decimal Amount { get; set; }

#if NETSTANDARD2_1
        [JsonPropertyName("ftPayItemCase")]
#endif
        [DataMember(EmitDefaultValue = false, IsRequired = false)]
        public PayItemCase ftPayItemCase { get; set; } = 0;

#if NETSTANDARD2_1
        [JsonPropertyName("ftPayItemCaseData")]
#endif
        [DataMember(EmitDefaultValue = false, IsRequired = false)]
        public object? ftPayItemCaseData { get; set; }

#if NETSTANDARD2_1
        [JsonPropertyName("Moment")]
#endif
        [DataMember(EmitDefaultValue = false, IsRequired = false)]
        public DateTime? Moment { get; set; }

#if NETSTANDARD2_1
        [JsonPropertyName("Position")]
#endif
        [DataMember(EmitDefaultValue = false, IsRequired = false)]
        public decimal Position { get; set; } = 0;

#if NETSTANDARD2_1
        [JsonPropertyName("AccountNumber")]
#endif
        [DataMember(EmitDefaultValue = false, IsRequired = false)]
        public string? AccountNumber { get; set; }

#if NETSTANDARD2_1
        [JsonPropertyName("CostCenter")]
#endif
        [DataMember(EmitDefaultValue = false, IsRequired = false)]
        public string? CostCenter { get; set; }

#if NETSTANDARD2_1
        [JsonPropertyName("MoneyGroup")]
#endif
        [DataMember(EmitDefaultValue = false, IsRequired = false)]
        public string? MoneyGroup { get; set; }

#if NETSTANDARD2_1
        [JsonPropertyName("MoneyNumber")]
#endif
        [DataMember(EmitDefaultValue = false, IsRequired = false)]
        public string? MoneyNumber { get; set; }

#if NETSTANDARD2_1
        [JsonPropertyName("MoneyBarcode")]
#endif
        [DataMember(EmitDefaultValue = false, IsRequired = false)]
        public string? MoneyBarcode { get; set; }

        [Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
#if NETSTANDARD2_1
        [JsonPropertyName("Currency")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
#endif
        [DataMember(Order = 170, EmitDefaultValue = false, IsRequired = false)]
        public Currency Currency { get; set; }

        [Newtonsoft.Json.JsonProperty("DecimalPrecisionMultiplier")]
#if NETSTANDARD2_1
        [JsonPropertyName("DecimalPrecisionMultiplier")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
#endif
        [DataMember(Order = 180, EmitDefaultValue = false, IsRequired = false)]
        public int DecimalPrecisionMultiplierSerialization
        {
            get => DecimalPrecisionMultiplier == 1 ? 0 : DecimalPrecisionMultiplier;
            set => DecimalPrecisionMultiplier = value == 0 ? 1 : value;
        }

        [Newtonsoft.Json.JsonIgnore]
#if NETSTANDARD2_1
        [JsonIgnore]
#endif
        public int DecimalPrecisionMultiplier { get; set; } = 1;
    }
}