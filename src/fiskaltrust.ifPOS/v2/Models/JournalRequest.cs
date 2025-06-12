using System.Runtime.Serialization;

#if NETSTANDARD2_1
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
#if NETSTANDARD2_1
        [JsonPropertyName("ftJournalType")]
#endif
        [DataMember(Order = 1, EmitDefaultValue = true, IsRequired = true)]
        public long ftJournalType { get; set; }

#if NETSTANDARD2_1
        [JsonPropertyName("From")]
#endif
        [DataMember(Order = 2, EmitDefaultValue = true, IsRequired = true)]
        public long From { get; set; }

#if NETSTANDARD2_1
        [JsonPropertyName("To")]
#endif
        [DataMember(Order = 3, EmitDefaultValue = true, IsRequired = true)]
        public long To { get; set; }

        private int _maxChunkSize = 4096;

#if NETSTANDARD2_1
        [JsonPropertyName("MaxChunkSize")]
#endif
        [DataMember(Order = 4, EmitDefaultValue = false, IsRequired = false)]
        public int MaxChunkSize
        {
            get => _maxChunkSize;
            set => _maxChunkSize = value;
        }
    }
}