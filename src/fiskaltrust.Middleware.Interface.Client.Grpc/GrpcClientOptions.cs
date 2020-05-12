using Grpc.Core;

namespace fiskaltrust.Middleware.Interface.Client.Grpc
{
    public class GrpcClientOptions : ClientOptions
    {
        public ChannelCredentials ChannelCredentials { get; set; } = ChannelCredentials.Insecure;
    }
}
