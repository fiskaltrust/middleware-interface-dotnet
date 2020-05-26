using fiskaltrust.ifPOS.v1.de;
using fiskaltrust.Middleware.Interface.Client.Common.RetryLogic;
using System.Threading.Tasks;

namespace fiskaltrust.Middleware.Interface.Client.Soap
{
    public static class SoapDESSCDFactory
    {
        public static async Task<IDESSCD> CreateSSCDAsync(ClientOptions options)
        {
            var connectionhandler = new SoapProxyConnectionHandler<IDESSCD>(options);

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
