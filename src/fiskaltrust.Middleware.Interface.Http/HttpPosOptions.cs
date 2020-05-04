using System;

namespace fiskaltrust.Middleware.Interface.Http
{
    public class HttpPosOptions
    {
        public string Url { get; set; }
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