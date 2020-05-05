using fiskaltrust.ifPOS.v1;
using fiskaltrust.Middleware.Interface.Client.Shared;

namespace fiskaltrust.Middleware.Interface.Client.Grpc
{
    public static class GrpcPosFactory
    {
        public static IPOS CreatePosAsync(GrpcPosOptions options)
        {
            var connectionhandler = new GrpcProxyConnectionHandler<IPOS>(options);
            var retryPolicyHelper = new RetryPolicyHandler<IPOS>(options.RetryPolicyOptions, connectionhandler);
            return new PosRetryProxyClient(retryPolicyHelper);
        }
    }
}
