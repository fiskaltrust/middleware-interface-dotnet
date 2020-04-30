using fiskaltrust.ifPOS.v1;
using Grpc.Core;
using ProtoBuf.Grpc.Client;
using System;

namespace fiskaltrust.Middleware.Interface.Grpc
{
    public class GrpcPosFactory : IPOSFactory
    {
        public IPOS CreatePosAsync(POSOptions options)
        {
            var uri = new Uri(options.Url);
            var channel = new Channel(uri.Host, uri.Port, ChannelCredentials.Insecure);
            
            return channel.CreateGrpcService<IPOS>();
        }
    }
}
