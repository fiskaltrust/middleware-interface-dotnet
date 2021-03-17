using fiskaltrust.ifPOS.v1.de;
using fiskaltrust.Middleware.Interface.Client.Common.RetryLogic;
using System.Threading.Tasks;

namespace fiskaltrust.Middleware.Interface.Client.Http
{
    public static class HttpDESSCDFactory
    {
        /// <summary>
        /// Create Http SSCD.
        /// </summary>
        /// <param name="options"></param>
        /// <returns>proxy</returns>
        public static async Task<IDESSCD> CreateSSCDAsync(ClientOptions options)
        {
            var connectionhandler = new HttpProxyConnectionHandler<IDESSCD>(new HttpDESSCD(options));

            if (options.RetryPolicyOptions != null)
            {
                var retryPolicyHelper = new RetryPolicyHandler<IDESSCD>(options.RetryPolicyOptions, connectionhandler);
                return new DESSCDRetryProxyClient(retryPolicyHelper);
            }
            else
            {
                return await connectionhandler.GetProxyAsync();
            }
        }
    }
}
