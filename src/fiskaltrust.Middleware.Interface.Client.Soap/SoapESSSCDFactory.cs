#if SYSTEM_TEXT_JSON
using fiskaltrust.ifPOS.v2.es;
using fiskaltrust.Middleware.Interface.Client.Common.RetryLogic;
using System.Threading.Tasks;

namespace fiskaltrust.Middleware.Interface.Client.Soap
{
    /// <summary>
    /// Create SOAP SSCD.
    /// </summary>
    public static class SoapESSSCDFactory
    {
        public static async Task<IESSSCD> CreateSSCDAsync(ClientOptions options)
        {
            var soapClientOptions = new SoapClientOptions
            {
                RetryPolicyOptions = options.RetryPolicyOptions,
                Url = options.Url
            };
            return await CreateSSCDAsync(soapClientOptions);
        }

        public static async Task<IESSSCD> CreateSSCDAsync(SoapClientOptions options)
        {
            var connectionhandler = new SoapProxyConnectionHandler<IESSSCD>(options);

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
#endif