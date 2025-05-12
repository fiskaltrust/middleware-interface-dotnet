using System;
using System.Runtime.Serialization;
using fiskaltrust.Middleware.Localization.v2.Models.ifPOS.v2.Cases;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace fiskaltrust.Middleware.ifPOS.v2.Models
{
    public class PayItem
    {
        [JsonProperty("ftPayItemId")]
        [DataMember(EmitDefaultValue = false, IsRequired = false)]
        public Guid? ftPayItemId { get; set; }

        [JsonProperty("Quantity")]
        [DataMember(EmitDefaultValue = true, IsRequired = true)]
        public decimal? QuantitySerialization
        {
            get => Quantity == 1 ? null : Quantity;
            set => Quantity = value ?? 1;
        }

        [JsonIgnore]
        public decimal Quantity { get; set; } = 1;

        [JsonProperty("Description")]
        [DataMember(EmitDefaultValue = true, IsRequired = true)]
        public string Description { get; set; }

        [JsonProperty("Amount")]
        [DataMember(EmitDefaultValue = true, IsRequired = true)]
        public decimal Amount { get; set; }

        [JsonProperty("ftPayItemCase")]
        [DataMember(EmitDefaultValue = false, IsRequired = false)]
        public PayItemCase  ftPayItemCase { get; set; } = 0;

        [JsonProperty("ftPayItemCaseData")]
        [DataMember(EmitDefaultValue = false, IsRequired = false)]
        public object? ftPayItemCaseData { get; set; }

        [JsonProperty("Moment")]
        [DataMember(EmitDefaultValue = false, IsRequired = false)]
        public DateTime? Moment { get; set; }

        [JsonProperty("Position")]
        [DataMember(EmitDefaultValue = false, IsRequired = false)]
        public decimal Position { get; set; } = 0;

        [JsonProperty("AccountNumber")]
        [DataMember(EmitDefaultValue = false, IsRequired = false)]
        public string? AccountNumber { get; set; }

        [JsonProperty("CostCenter")]
        [DataMember(EmitDefaultValue = false, IsRequired = false)]
        public string? CostCenter { get; set; }

        [JsonProperty("MoneyGroup")]
        [DataMember(EmitDefaultValue = false, IsRequired = false)]
        public string? MoneyGroup { get; set; }

        [JsonProperty("MoneyNumber")]
        [DataMember(EmitDefaultValue = false, IsRequired = false)]
        public string? MoneyNumber { get; set; }

        [JsonProperty("MoneyBarcode")]
        [DataMember(EmitDefaultValue = false, IsRequired = false)]
        public string? MoneyBarcode { get; set; }

        [JsonProperty("Currency")]
        [JsonConverter(typeof(StringEnumConverter))]
        [DataMember(Order = 170, EmitDefaultValue = false, IsRequired = false)]
        public Currency Currency { get; set; }

        [JsonProperty("DecimalPrecisionMultiplier")]
        [DataMember(Order = 180, EmitDefaultValue = false, IsRequired = false)]
        public int DecimalPrecisionMultiplierSerialization
        {
            get => DecimalPrecisionMultiplier == 1 ? 0 : DecimalPrecisionMultiplier;
            set => DecimalPrecisionMultiplier = value == 0 ? 1 : value;
        }

        [JsonIgnore]
        public int DecimalPrecisionMultiplier { get; set; } = 1;
    }
}