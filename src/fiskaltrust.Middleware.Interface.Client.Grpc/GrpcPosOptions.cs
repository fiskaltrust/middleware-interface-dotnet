using Grpc.Core;

namespace fiskaltrust.Middleware.Interface.Client.Grpc
{
    public class GrpcPosOptions : PosOptions
    {
        public ChannelCredentials ChannelCredentials { get; set; } = ChannelCredentials.Insecure;
    }
}
