using System.Runtime.Serialization;
using fiskaltrust.ifPOS.v2.Cases;
using System;


#if !WCF
using System.Text.Json.Serialization;
#endif

namespace fiskaltrust.ifPOS.v2
{
        /// <summary>
        /// Request from the cash register to extract a journal from the local database. 
        /// Type and Timerange can be specified with the properties Journaltype,from and to. 
        /// </summary>
        public class JournalRequest
        {
                [Newtonsoft.Json.JsonProperty("ftJournalType", DefaultValueHandling = Newtonsoft.Json.DefaultValueHandling.Include)]
#if !WCF
                [JsonPropertyName("ftJournalType")]
                [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
#endif
                [DataMember(Order = 1, EmitDefaultValue = true, IsRequired = true)]
                public JournalType ftJournalType { get; set; } = 0;

                [Newtonsoft.Json.JsonProperty("From", DefaultValueHandling = Newtonsoft.Json.DefaultValueHandling.Ignore)]
#if !WCF
                [JsonPropertyName("From")]
                [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
#endif
                [DataMember(Order = 2, EmitDefaultValue = false, IsRequired = false)]
                public long From { get; set; }

                [Newtonsoft.Json.JsonProperty("To", DefaultValueHandling = Newtonsoft.Json.DefaultValueHandling.Ignore)]
#if !WCF
                [JsonPropertyName("To")]
                [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
#endif
                [DataMember(Order = 3, EmitDefaultValue = false, IsRequired = false)]
                public long To { get; set; }
        }
}