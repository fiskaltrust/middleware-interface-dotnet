#if SYSTEM_TEXT_JSON
using fiskaltrust.ifPOS.v2.pl;
using fiskaltrust.Middleware.Interface.Client.Common.RetryLogic;
using System.Threading.Tasks;

namespace fiskaltrust.Middleware.Interface.Client.Http
{
    /// <summary>
    /// Create Http SSCD.
    /// </summary>
    public static class HttpPLSSCDFactory
    {
        public static async Task<IPLSSCD> CreateSSCDAsync(HttpPLSSCDClientOptions options)
        {
            var connectionhandler = new HttpProxyConnectionHandler<IPLSSCD>(new HttpPLSSCD(options));

            if (options.RetryPolicyOptions != null)
            {
                var retryPolicyHelper = new RetryPolicyHandler<IPLSSCD>(options.RetryPolicyOptions, connectionhandler);
                return new PLSSCDRetryProxyClient(retryPolicyHelper);
            }
            else
            {
                return await connectionhandler.GetProxyAsync();
            }
        }
    }
}
#endif
