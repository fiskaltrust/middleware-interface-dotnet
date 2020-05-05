using fiskaltrust.Middleware.Interface.Client.Shared;
using Grpc.Core;
using ProtoBuf.Grpc.Client;
using System;
using System.Threading.Tasks;

namespace fiskaltrust.Middleware.Interface.Client.Grpc
{
    public class GrpcProxyConnectionHandler<T> : IProxyConnectionHandler<T> where T : class
    {
        private T _proxy;
        private Channel _channel;
        private readonly GrpcPosOptions _options;

        public GrpcProxyConnectionHandler(GrpcPosOptions options)
        {
            _options = options;
        }

        public Task ReconnectAsync()
        {
            if (_proxy != null && _channel.State == ChannelState.Ready)
            {
                return Task.CompletedTask;
            }
            var uri = new Uri(_options.Url);
            _channel = new Channel(uri.Host, uri.Port, _options.ChannelCredentials);
            _proxy = _channel.CreateGrpcService<T>();

            return Task.CompletedTask;
        }

        public async Task ForceReconnectAsync()
        {
            try
            {
                await _channel.ShutdownAsync();
            }
            catch
            {
                // We can ignore the case when shutdown failed
            }
            _channel = null;
            var uri = new Uri(_options.Url);
            _channel = new Channel(uri.Host, uri.Port, ChannelCredentials.Insecure);
            _proxy = _channel.CreateGrpcService<T>();
        }

        public async Task<T> GetProxyAsync()
        {
            if (_proxy == null || _channel.State != ChannelState.Ready)
            {
                await ReconnectAsync();
            }

            return _proxy;
        }
    }
}
