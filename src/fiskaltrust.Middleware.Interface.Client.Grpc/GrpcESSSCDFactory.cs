#if SYSTEM_TEXT_JSON
using fiskaltrust.ifPOS.v2.es;
using fiskaltrust.Middleware.Interface.Client.Common.RetryLogic;
using System.Threading.Tasks;

namespace fiskaltrust.Middleware.Interface.Client.Grpc
{
    /// <summary>
    /// A factory to create a gRPC-based IDESSCD client instance for communicating with a Montenegrin SCU package.
    /// </summary>
    public static class GrpcESSSCDFactory
    {
        public static async Task<IESSSCD> CreateSSCDAsync(GrpcClientOptions options)
        {
#if NET6_0_OR_GREATER
            var connectionhandler = new GrpcProxyConnectionHandler<IESSSCD>(options);
#else
            var connectionhandler = new NativeGrpcProxyConnectionHandler<IESSSCD>(options);
#endif

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