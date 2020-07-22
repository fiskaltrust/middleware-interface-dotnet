using fiskaltrust.ifPOS.v1;
using fiskaltrust.Middleware.Interface.Client.Common.RetryLogic;
using  fiskaltrust.Middleware.Interface.Client.Soap.Helpers;
using System.Threading.Tasks;

namespace fiskaltrust.Middleware.Interface.Client.Soap
{
    public static class SoapPosFactory
    {
        public static async Task<IPOS> CreatePosAsync(ClientOptions options)
        {
            var connectionhandler = new SoapProxyConnectionHandler<IPOS>(options);

            if (options.RetryPolicyOptions != null)
            {
                var retryPolicyHelper = new RetryPolicyHandler<IPOS>(options.RetryPolicyOptions, connectionhandler);
                return new AsyncPOSHelper(new PosRetryProxyClient(retryPolicyHelper));
            }
            else
            {
                return new AsyncPOSHelper(await connectionhandler.GetProxyAsync());
            }
        }
    }
}
