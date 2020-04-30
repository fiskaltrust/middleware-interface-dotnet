using fiskaltrust.ifPOS.v1;
using System;

namespace fiskaltrust.Middleware.Interface.Http
{
    public class HttpPosFactory : IPOSFactory
    {
        public IPOS CreatePosAsync(POSOptions options) => new HttpPos(options);
    }
}
