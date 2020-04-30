using fiskaltrust.ifPOS.v1;
using System;

namespace fiskaltrust.Middleware.Interface.Http
{
    public class HttpPosFactory : IPOSFactory
    {
        public IPOS CreatePosAsync(POSOptions options)
        {
            if (!(options is HttpPosOptions httpOptions))
            {
                throw new ArgumentException($"Parameter {nameof(options)} must be of type {nameof(HttpPosOptions)}.");
            }

            return new HttpPos(httpOptions);
        }
    }
}
