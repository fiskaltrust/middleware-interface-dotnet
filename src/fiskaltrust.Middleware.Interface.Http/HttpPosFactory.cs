using fiskaltrust.ifPOS.v1;
using System;

namespace fiskaltrust.Middleware.Interface.Http
{
    public class HttpPosFactory
    {
        public IPOS CreatePosAsync(HttpPosOptions options) => new HttpPos(options);
    }
}
