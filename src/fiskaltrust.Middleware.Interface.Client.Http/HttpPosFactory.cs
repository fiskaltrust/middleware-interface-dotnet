using fiskaltrust.ifPOS.v1;
using System.Threading.Tasks;

namespace fiskaltrust.Middleware.Interface.Client.Http
{
    public static class HttpPosFactory
    {
        public static async Task<IPOS> CreatePosAsync(HttpPosOptions options) => await Task.FromResult(new HttpPos(options));
    }
}
