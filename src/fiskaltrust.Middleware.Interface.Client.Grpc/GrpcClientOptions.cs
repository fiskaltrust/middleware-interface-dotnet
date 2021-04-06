using Grpc.Core;
using System.Collections.Generic;

namespace fiskaltrust.Middleware.Interface.Client.Grpc
{
    /// <summary>
    /// Client Options ChannelCredentials and ChannelOptions.
    /// </summary>
    public class GrpcClientOptions : ClientOptions
    {
        public ChannelCredentials ChannelCredentials { get; set; } = ChannelCredentials.Insecure;
        public List<ChannelOption> ChannelOptions { get; set; } = new List<ChannelOption>();
    }
}
