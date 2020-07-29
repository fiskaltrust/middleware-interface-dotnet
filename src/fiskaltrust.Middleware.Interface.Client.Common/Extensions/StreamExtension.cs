using fiskaltrust.ifPOS.v1;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace fiskaltrust.Middleware.Interface.Client.Extensions
{
    public static class StreamExtensions
    {
        public static async IAsyncEnumerable<JournalResponse> ToAsyncEnumerable(this Stream stream)
        {
            var chunkSize = 4096;
            var buffer = new byte[chunkSize];
            while ((_ = await stream.ReadAsync(buffer, 0, buffer.Length)) > 0)
            {
                yield return new JournalResponse
                {
                    Chunk = buffer.ToList()
                };
                buffer = new byte[chunkSize];
            }
            stream.Dispose();
            yield break;
        }
    }
}
