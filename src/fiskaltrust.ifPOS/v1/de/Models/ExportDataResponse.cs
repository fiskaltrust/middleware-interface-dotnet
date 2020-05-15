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

        /// <summary>
        /// Total size of TAR-file to be exported in current session.
        /// If the total size is less than 0, the server did not finish to prepare the complete download.
        /// ExportData can be called again to get next junk.
        /// EndExportSession will throw Exception as long as TotalTarFileSize cannot be served.
        /// </summary>
        [DataMember(Order = 30)]
        public long TotalTarFileSize { get; set; } = -1;
    }
}
