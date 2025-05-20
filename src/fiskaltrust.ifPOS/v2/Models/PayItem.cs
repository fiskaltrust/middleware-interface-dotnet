using System;
using System.Runtime.Serialization;
using fiskaltrust.Middleware.Localization.v2.Models.ifPOS.v2.Cases;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

#if NETSTANDARD2_1
using System.Text.Json.Serialization;
#endif

namespace fiskaltrust.Middleware.ifPOS.v2.Models
{
    public class PayItem
    {
        [JsonProperty("ftPayItemId")]
#if NETSTANDARD2_1
        [JsonPropertyName("ftPayItemId")]
#endif
        [DataMember(EmitDefaultValue = false, IsRequired = false)]
        public Guid? ftPayItemId { get; set; }

        private decimal _quantity = 1;

        [JsonProperty("Quantity")]
#if NETSTANDARD2_1
        [JsonPropertyName("Quantity")]
#endif
        [DataMember(EmitDefaultValue = true, IsRequired = true)]
        public decimal? Quantity
        {
            get => _quantity == 1 ? null : _quantity;
            set => _quantity = value ?? 1;
        }

        [JsonProperty("Description")]
#if NETSTANDARD2_1
        [JsonPropertyName("Description")]
#endif
        [DataMember(EmitDefaultValue = true, IsRequired = true)]
        public string Description { get; set; }

        [JsonProperty("Amount")]
#if NETSTANDARD2_1
        [JsonPropertyName("Amount")]
#endif
        [DataMember(EmitDefaultValue = true, IsRequired = true)]
        public decimal Amount { get; set; }

        [JsonProperty("ftPayItemCase")]
#if NETSTANDARD2_1
        [JsonPropertyName("ftPayItemCase")]
#endif
        [DataMember(EmitDefaultValue = false, IsRequired = false)]
        public PayItemCase ftPayItemCase { get; set; } = 0;

        [JsonProperty("ftPayItemCaseData")]
#if NETSTANDARD2_1
        [JsonPropertyName("ftPayItemCaseData")]
#endif
        [DataMember(EmitDefaultValue = false, IsRequired = false)]
        public object? ftPayItemCaseData { get; set; }

        [JsonProperty("Moment")]
#if NETSTANDARD2_1
        [JsonPropertyName("Moment")]
#endif
        [DataMember(EmitDefaultValue = false, IsRequired = false)]
        public DateTime? Moment { get; set; }

        [JsonProperty("Position")]
#if NETSTANDARD2_1
        [JsonPropertyName("Position")]
#endif
        [DataMember(EmitDefaultValue = false, IsRequired = false)]
        public decimal Position { get; set; } = 0;

        [JsonProperty("AccountNumber")]
#if NETSTANDARD2_1
        [JsonPropertyName("AccountNumber")]
#endif
        [DataMember(EmitDefaultValue = false, IsRequired = false)]
        public string? AccountNumber { get; set; }

        [JsonProperty("CostCenter")]
#if NETSTANDARD2_1
        [JsonPropertyName("CostCenter")]
#endif
        [DataMember(EmitDefaultValue = false, IsRequired = false)]
        public string? CostCenter { get; set; }

        [JsonProperty("MoneyGroup")]
#if NETSTANDARD2_1
        [JsonPropertyName("MoneyGroup")]
#endif
        [DataMember(EmitDefaultValue = false, IsRequired = false)]
        public string? MoneyGroup { get; set; }

        [JsonProperty("MoneyNumber")]
#if NETSTANDARD2_1
        [JsonPropertyName("MoneyNumber")]
#endif
        [DataMember(EmitDefaultValue = false, IsRequired = false)]
        public string? MoneyNumber { get; set; }

        [JsonProperty("MoneyBarcode")]
#if NETSTANDARD2_1
        [JsonPropertyName("MoneyBarcode")]
#endif
        [DataMember(EmitDefaultValue = false, IsRequired = false)]
        public string? MoneyBarcode { get; set; }

        [JsonProperty("Currency")]
        [Newtonsoft.Json.JsonConverter(typeof(StringEnumConverter))]
#if NETSTANDARD2_1
        [JsonPropertyName("Currency")]
        [System.Text.Json.Serialization.JsonConverter(typeof(System.Text.Json.Serialization.JsonStringEnumConverter))]
#endif
        [DataMember(Order = 170, EmitDefaultValue = false, IsRequired = false)]
        public Currency Currency { get; set; }

        private int _decimalPrecisionMultiplier = 1;

        [JsonProperty("DecimalPrecisionMultiplier")]
#if NETSTANDARD2_1
        [JsonPropertyName("DecimalPrecisionMultiplier")]
#endif
        [DataMember(Order = 180, EmitDefaultValue = false, IsRequired = false)]
        public int DecimalPrecisionMultiplier
        {
            get => _decimalPrecisionMultiplier == 1 ? 0 : _decimalPrecisionMultiplier;
            set => _decimalPrecisionMultiplier = value == 0 ? 1 : value;
        }
    }
}