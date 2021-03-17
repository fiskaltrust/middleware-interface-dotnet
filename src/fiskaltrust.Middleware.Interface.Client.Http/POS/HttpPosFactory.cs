using fiskaltrust.ifPOS.v1;
using fiskaltrust.Middleware.Interface.Client.Common.RetryLogic;
using fiskaltrust.Middleware.Interface.Client.Http.Helpers;
using System.Threading.Tasks;

namespace fiskaltrust.Middleware.Interface.Client.Http
{
    public static class HttpPosFactory
    {
        /// <summary>
        /// Create HTTP POS Client
        /// </summary>
        /// <param name="options"></param>
        /// <returns>proxy</returns>
        public static async Task<IPOS> CreatePosAsync(HttpPosClientOptions options)
        {
            var connectionhandler = new HttpProxyConnectionHandler<IPOS>(new AsyncJournalPOSHelper(new HttpPos(options)));

            if (options.RetryPolicyOptions != null)
            {
                var retryPolicyHelper = new RetryPolicyHandler<IPOS>(options.RetryPolicyOptions, connectionhandler);
                return new PosRetryProxyClient(retryPolicyHelper);
            }
            else
            {
                return await connectionhandler.GetProxyAsync();
            }
        }
    }
}
