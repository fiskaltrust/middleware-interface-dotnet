#if SYSTEM_TEXT_JSON
using fiskaltrust.ifPOS.v2.pl;
using fiskaltrust.Middleware.Interface.Client.Common.RetryLogic;
using System.Threading.Tasks;

namespace fiskaltrust.Middleware.Interface.Client.Soap
{
    /// <summary>
    /// Create SOAP SSCD.
    /// </summary>
    public static class SoapPLSSCDFactory
    {
        public static async Task<IPLSSCD> CreateSSCDAsync(ClientOptions options)
        {
            var soapClientOptions = new SoapClientOptions
            {
                RetryPolicyOptions = options.RetryPolicyOptions,
                Url = options.Url
            };
            return await CreateSSCDAsync(soapClientOptions);
        }

        public static async Task<IPLSSCD> CreateSSCDAsync(SoapClientOptions options)
        {
            var connectionhandler = new SoapProxyConnectionHandler<IPLSSCD>(options);

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
