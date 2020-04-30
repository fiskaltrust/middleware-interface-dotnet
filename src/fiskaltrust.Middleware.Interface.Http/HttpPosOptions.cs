using fiskaltrust.ifPOS.v1;

namespace fiskaltrust.Middleware.Interface.Http
{
    public class HttpPosOptions : POSOptions
    {
        public HttpCommunicationType CommunicationType { get; set; }
    }

    public enum HttpCommunicationType
    {
        Json,
        Xml
    }
}
