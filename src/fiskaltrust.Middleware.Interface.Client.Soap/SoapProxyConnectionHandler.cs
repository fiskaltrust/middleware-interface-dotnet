using fiskaltrust.Middleware.Interface.Client.Shared;
using fiskaltrust.Middleware.Interface.Client.Shared.RetryLogic.Interfaces;
using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Threading.Tasks;

namespace fiskaltrust.Middleware.Interface.Client.Soap
{
    public class SoapProxyConnectionHandler<T> : IProxyConnectionHandler<T> where T : class
    {
        private const long MAX_RECEIVED_MESSAGE_SIZE = 16 * 1024 * 1024;
        private const int SEND_TIMEOUT_SEC = 15;
        private const int RECEIVE_TIMEOUT_DAYS = 14;

        private T _proxy;
        private readonly PosOptions _options;

        public SoapProxyConnectionHandler(PosOptions options)
        {
            _options = options;
        }

        public Task ReconnectAsync()
        {
            if (_proxy != null && (_proxy as ICommunicationObject).State == CommunicationState.Opened)
            {
                return Task.CompletedTask;
            }

            var binding = ConfigureBinding();

            var factory = new ChannelFactory<T>(binding, new EndpointAddress(_options.Url));
            _proxy = factory.CreateChannel();

            return Task.CompletedTask;
        }

        public Task ForceReconnectAsync()
        {
            try
            {
                CloseWcfConnection();
            }
            catch
            {
                // We can ignore the case when shutdown failed
            }
            var binding = ConfigureBinding();
            var factory = new ChannelFactory<T>(binding, new EndpointAddress(_options.Url));
            _proxy = factory.CreateChannel();

            return Task.CompletedTask;
        }

        private void CloseWcfConnection()
        {
            if (_proxy != null)
            {
                try
                {
                    if ((_proxy as ICommunicationObject).State == CommunicationState.Opened)
                    {
                        (_proxy as ICommunicationObject).Close();
                    }
                }
                finally
                {
                    if ((_proxy as ICommunicationObject).State != CommunicationState.Closed)
                    {
                        (_proxy as ICommunicationObject).Abort();
                    }
                }
            }
        }

        private Binding ConfigureBinding()
        {
            var sendTimeout = TimeSpan.FromSeconds(SEND_TIMEOUT_SEC);
            var receiveTimeout = TimeSpan.FromDays(RECEIVE_TIMEOUT_DAYS);

            return _options.Url.Scheme switch
            {
                "http" => new BasicHttpBinding(BasicHttpSecurityMode.None) { MaxReceivedMessageSize = MAX_RECEIVED_MESSAGE_SIZE, SendTimeout = sendTimeout, ReceiveTimeout = receiveTimeout },
                "https" => new BasicHttpBinding(BasicHttpSecurityMode.Transport) { MaxReceivedMessageSize = MAX_RECEIVED_MESSAGE_SIZE, SendTimeout = sendTimeout, ReceiveTimeout = receiveTimeout },
                "net.pipe" => new NetNamedPipeBinding(NetNamedPipeSecurityMode.None) { MaxReceivedMessageSize = MAX_RECEIVED_MESSAGE_SIZE, SendTimeout = sendTimeout, ReceiveTimeout = receiveTimeout },
                "net.tcp" => new NetTcpBinding(SecurityMode.None) { MaxReceivedMessageSize = MAX_RECEIVED_MESSAGE_SIZE, SendTimeout = sendTimeout, ReceiveTimeout = receiveTimeout },
                _ => throw new ArgumentException($"The url {_options.Url} is not supported.", nameof(_options.Url))
            };
        }

        public async Task<T> GetProxyAsync()
        {
            await ReconnectAsync();
            return await Task.FromResult(_proxy);
        }
    }
}
