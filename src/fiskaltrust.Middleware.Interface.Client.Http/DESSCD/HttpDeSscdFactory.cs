using fiskaltrust.ifPOS.v1.de;
using fiskaltrust.Middleware.Interface.Client.Common.RetryLogic;
using System.Threading.Tasks;

namespace fiskaltrust.Middleware.Interface.Client.Http
{
    public static class HttpDeSscdFactory
    {
        public static async Task<IDESSCD> CreateSscdAsync(ClientOptions options)
        {
            var connectionhandler = new HttpProxyConnectionHandler<IDESSCD>(new HttpDeSscd(options));

            if (options.RetryPolicyOptions != null)
            {
                var retryPolicyHelper = new RetryPolicyHandler<IDESSCD>(options.RetryPolicyOptions, connectionhandler);
                return new DeSscdRetryProxyClient(retryPolicyHelper);
            }
            else
            {
                return await connectionhandler.GetProxyAsync();
            }
        }
    }
}
