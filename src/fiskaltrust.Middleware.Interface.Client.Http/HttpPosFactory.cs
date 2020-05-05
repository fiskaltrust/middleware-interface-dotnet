using fiskaltrust.ifPOS.v1;
using System;

namespace fiskaltrust.Middleware.Interface.Client.Http
{
    public static class HttpPosFactory
    {
        public static IPOS CreatePosAsync(HttpPosOptions options) => new HttpPos(options);
    }
}
