using fiskaltrust.Middleware.Interface.Client.Shared;
using System;

namespace fiskaltrust.Middleware.Interface.Client.Http
{
    public class HttpPosOptions : PosOptions
    {
        public HttpCommunicationType CommunicationType { get; set; }
        public Guid? CashboxId { get; set; }
        public string AccessToken { get; set; }
    }

    public enum HttpCommunicationType
    {
        Json,
        Xml
    }
}