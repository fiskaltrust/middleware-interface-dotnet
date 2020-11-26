using System;

namespace fiskaltrust.Middleware.Interface.Client.Soap
{
    public class SoapClientOptions : ClientOptions
    {
        public long MaxReceivedMessageSize { get; set; } = 16 * 1024 * 1024;
        public TimeSpan ReceiveTimeout { get; set; } = TimeSpan.FromDays(14);
    }
}