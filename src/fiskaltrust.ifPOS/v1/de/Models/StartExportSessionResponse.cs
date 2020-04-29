using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace fiskaltrust.ifPOS.v1.de
{
    [DataContract]
    public class StartExportSessionResponse
    {
        [DataMember(Order = 10)]
        public string TokenId { get; set; }

        [DataMember(Order = 20)]
        public string TseSerialNumberOctet { get; set; }
    }
}
