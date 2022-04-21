using fiskaltrust.ifPOS.v2.me;
using fiskaltrust.Middleware.Interface.Client.Common.RetryLogic;
using System.Threading.Tasks;

namespace fiskaltrust.Middleware.Interface.Client.Grpc
{
    /// <summary>
    /// Create grpc SSCD.
    /// </summary>
    public static class GrpcMESSCDFactory
    {
        public static async Task<IMESSCD> CreateSSCDAsync(GrpcClientOptions options)
        {
            var connectionhandler = new GrpcProxyConnectionHandler<IMESSCD>(options);

            if (options.RetryPolicyOptions != null)
            {
                var retryPolicyHelper = new RetryPolicyHandler<IMESSCD>(options.RetryPolicyOptions, connectionhandler);
                return new MESSCDRetryProxyClient(retryPolicyHelper);
            }
            else
            {
                return await connectionhandler.GetProxyAsync();
            }
        }
    }
}
