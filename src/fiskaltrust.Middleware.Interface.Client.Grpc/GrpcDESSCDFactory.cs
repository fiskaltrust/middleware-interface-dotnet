using fiskaltrust.ifPOS.v1.de;
using fiskaltrust.Middleware.Interface.Client.Common.RetryLogic;
using System.Threading.Tasks;

namespace fiskaltrust.Middleware.Interface.Client.Grpc
{
    public static class GrpcDESSCDFactory
    {
        /// <summary>
        /// Create grpc SSCD.
        /// </summary>
        /// <param name="options"></param>
        /// <returns>proxy</returns>
        public static async Task<IDESSCD> CreateSSCDAsync(GrpcClientOptions options)
        {
            var connectionhandler = new GrpcProxyConnectionHandler<IDESSCD>(options);

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
