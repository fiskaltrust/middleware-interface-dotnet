using fiskaltrust.ifPOS.v2.es;
using fiskaltrust.Middleware.Interface.Client.Common.RetryLogic;
using System.Threading.Tasks;

namespace fiskaltrust.Middleware.Interface.Client.Http
{
    /// <summary>
    /// Create Http SSCD.
    /// </summary>
    public static class HttpESSSCDFactory
    {
        public static async Task<IESSSCD> CreateSSCDAsync(HttpESSSCDClientOptions options)
        {
            var connectionhandler = new HttpProxyConnectionHandler<IESSSCD>(new HttpESSSCD(options));

            if (options.RetryPolicyOptions != null)
            {
                var retryPolicyHelper = new RetryPolicyHandler<IESSSCD>(options.RetryPolicyOptions, connectionhandler);
                return new ESSSCDRetryProxyClient(retryPolicyHelper);
            }
            else
            {
                return await connectionhandler.GetProxyAsync();
            }
        }
    }
}
