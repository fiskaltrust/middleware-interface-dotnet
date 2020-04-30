using fiskaltrust.ifPOS.v1;
using System;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace fiskaltrust.Middleware.Interface.Soap
{
    public class SoapPosFactory : IPOSFactory
    {
        private const long MAX_RECEIVED_MESSAGE_SIZE = 16 * 1024 * 1024;

        public IPOS CreatePosAsync(POSOptions options)
        {
            var uri = new Uri(options.Url);
            Binding binding = uri.Scheme switch
            {
                "http" => new BasicHttpBinding(BasicHttpSecurityMode.None) { MaxReceivedMessageSize = MAX_RECEIVED_MESSAGE_SIZE },
                "https" => new BasicHttpBinding(BasicHttpSecurityMode.Transport) { MaxReceivedMessageSize = MAX_RECEIVED_MESSAGE_SIZE },
                "net.pipe" => new NetNamedPipeBinding(NetNamedPipeSecurityMode.None) { MaxReceivedMessageSize = MAX_RECEIVED_MESSAGE_SIZE },
                "net.tcp" => new NetTcpBinding(SecurityMode.None) { MaxReceivedMessageSize = MAX_RECEIVED_MESSAGE_SIZE },
                _ => throw new ArgumentException($"The url {options.Url} is not supported", nameof(options.Url))
            };

            var endpoint = new EndpointAddress(options.Url);
            var factory = new ChannelFactory<IPOS>(binding, endpoint);

            return factory.CreateChannel();
        }
    }
}
