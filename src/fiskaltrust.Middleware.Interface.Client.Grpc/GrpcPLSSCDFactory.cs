#if SYSTEM_TEXT_JSON
using fiskaltrust.ifPOS.v2.pl;
using fiskaltrust.Middleware.Interface.Client.Common.RetryLogic;
using System.Threading.Tasks;

namespace fiskaltrust.Middleware.Interface.Client.Grpc
{
    /// <summary>
    /// A factory to create a gRPC-based IPLSSCD client instance for communicating with a Polish SCU package.
    /// </summary>
    public static class GrpcPLSSCDFactory
    {
        public static async Task<IPLSSCD> CreateSSCDAsync(GrpcClientOptions options)
        {
#if NET6_0_OR_GREATER
            var connectionhandler = new GrpcProxyConnectionHandler<IPLSSCD>(options);
#else
            var connectionhandler = new NativeGrpcProxyConnectionHandler<IPLSSCD>(options);
#endif

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
