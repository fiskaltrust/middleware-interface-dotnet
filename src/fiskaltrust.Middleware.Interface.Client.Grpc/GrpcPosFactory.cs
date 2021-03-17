using fiskaltrust.ifPOS.v1;
using fiskaltrust.Middleware.Interface.Client.Common.RetryLogic;
using System.Threading.Tasks;

namespace fiskaltrust.Middleware.Interface.Client.Grpc
{
    public static class GrpcPosFactory
    {
        /// <summary>
        /// Create grpc POS Client.
        /// </summary>
        /// <param name="options"></param>
        /// <returns>proxy</returns>
        public static async Task<IPOS> CreatePosAsync(GrpcClientOptions options)
        {
            var connectionhandler = new GrpcProxyConnectionHandler<IPOS>(options);

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
