using Grpc.Core;
using System.Collections.Generic;

namespace fiskaltrust.Middleware.Interface.Client.Grpc
{
    public class GrpcClientOptions : ClientOptions
    {
        /// <summary>
        /// Client Options ChannelCredentials and ChannelOptions.
        /// </summary>
        public ChannelCredentials ChannelCredentials { get; set; } = ChannelCredentials.Insecure;
        public List<ChannelOption> ChannelOptions { get; set; } = new List<ChannelOption>();
    }
}
