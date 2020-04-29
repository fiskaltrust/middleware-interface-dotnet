using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace fiskaltrust.ifPOS.v1.de
{
    [DataContract]
    public class ExportDataResponse
    {
        [DataMember(Order = 10)]
        StartExportSessionResponse Session { get; set; }

        [DataMember(Order = 20)]
        IEnumerable<string> TarFileByteJunkBase64 { get; set; }
    }
}
