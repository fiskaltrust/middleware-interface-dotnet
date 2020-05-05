using fiskaltrust.ifPOS.v1;
using fiskaltrust.Middleware.Interface.Client.Shared;
using System.Threading.Tasks;

namespace fiskaltrust.Middleware.Interface.Client.Grpc
{
    public static class GrpcPosFactory
    {
        public static async Task<IPOS> CreatePosAsync(GrpcPosOptions options)
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
