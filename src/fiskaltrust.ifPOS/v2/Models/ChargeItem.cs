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
    public class ChargeItem
    {
        [JsonProperty("ftChargeItemId")]
#if NETSTANDARD2_1
        [JsonPropertyName("ftChargeItemId")]
#endif
        [DataMember(EmitDefaultValue = false, IsRequired = false)]
        public Guid? ftChargeItemId { get; set; }

        [JsonProperty("Quantity")]
#if NETSTANDARD2_1
        [JsonPropertyName("Quantity")]
#endif
        [DataMember(EmitDefaultValue = true, IsRequired = true)]
        public decimal Quantity { get; set; } = 1m;

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
        public decimal Amount { get; set; } = 0;

        [JsonProperty("VATRate")]
#if NETSTANDARD2_1
        [JsonPropertyName("VATRate")]
#endif
        [DataMember(EmitDefaultValue = true, IsRequired = true)]
        public decimal VATRate { get; set; } = 0;

        [JsonProperty("ftChargeItemCase")]
#if NETSTANDARD2_1
        [JsonPropertyName("ftChargeItemCase")]
#endif
        [DataMember(EmitDefaultValue = false, IsRequired = false)]
        public ChargeItemCase ftChargeItemCase { get; set; } = 0;

        [JsonProperty("ftChargeItemCaseData")]
#if NETSTANDARD2_1
        [JsonPropertyName("ftChargeItemCaseData")]
#endif
        [DataMember(EmitDefaultValue = false, IsRequired = false)]
        public object? ftChargeItemCaseData { get; set; }

        [JsonProperty("VATAmount")]
#if NETSTANDARD2_1
        [JsonPropertyName("VATAmount")]
#endif
        [DataMember(EmitDefaultValue = false, IsRequired = false)]
        public decimal? VATAmount { get; set; }

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

        [JsonProperty("ProductGroup")]
#if NETSTANDARD2_1
        [JsonPropertyName("ProductGroup")]
#endif
        [DataMember(EmitDefaultValue = false, IsRequired = false)]
        public string? ProductGroup { get; set; }

        [JsonProperty("ProductNumber")]
#if NETSTANDARD2_1
        [JsonPropertyName("ProductNumber")]
#endif
        [DataMember(EmitDefaultValue = false, IsRequired = false)]
        public string? ProductNumber { get; set; }

        [JsonProperty("ProductBarcode")]
#if NETSTANDARD2_1
        [JsonPropertyName("ProductBarcode")]
#endif
        [DataMember(EmitDefaultValue = false, IsRequired = false)]
        public string? ProductBarcode { get; set; }

        [JsonProperty("Unit")]
#if NETSTANDARD2_1
        [JsonPropertyName("Unit")]
#endif
        [DataMember(EmitDefaultValue = false, IsRequired = false)]
        public string? Unit { get; set; }

        [JsonProperty("UnitQuantity")]
#if NETSTANDARD2_1
        [JsonPropertyName("UnitQuantity")]
#endif
        [DataMember(EmitDefaultValue = false, IsRequired = false)]
        public decimal? UnitQuantity { get; set; }

        [JsonProperty("UnitPrice")]
#if NETSTANDARD2_1
        [JsonPropertyName("UnitPrice")]
#endif
        [DataMember(EmitDefaultValue = false, IsRequired = false)]
        public decimal? UnitPrice { get; set; }

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