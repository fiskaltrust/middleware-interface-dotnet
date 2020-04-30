using System.Collections.Generic;
using System.Runtime.Serialization;

namespace fiskaltrust.ifPOS.v1.de
{
    [DataContract]
    public class ExportDataResponse
    {
        [DataMember(Order = 10)]
        public string TokenId { get; set; }

        [DataMember(Order = 20)]
        public IEnumerable<string> TarFileByteJunksBase64 { get; set; }
    }
}
