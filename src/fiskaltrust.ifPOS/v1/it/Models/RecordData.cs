using System.Runtime.Serialization;

namespace fiskaltrust.ifPOS.v1.it
{
    /// <summary>
    /// RecordData
    /// </summary>
    [DataContract]
    public class RecordData
    {
        /// <summary>
        /// RecNumber
        /// </summary>
        [DataMember(Order = 10)]
        public int RecNumber { get; set; }

        /// <summary>
        /// ZRecNumber
        /// </summary>
        [DataMember(Order = 20)]
        public int ZRecNumber { get; set; }

        /// <summary>
        /// Data
        /// </summary>
        [DataMember(Order = 30)]
        public string Data { get; set; }
    }
}
