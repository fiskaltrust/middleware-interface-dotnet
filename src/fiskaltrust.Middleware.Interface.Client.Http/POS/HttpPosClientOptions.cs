using System;

namespace fiskaltrust.Middleware.Interface.Client.Http
{
    /// <summary>
    /// Set Client Options Communication type,Url, CashboxId and AccessToken.
    /// </summary>
    public class HttpPosClientOptions : ClientOptions
    {
        public HttpCommunicationType CommunicationType { get; set; }
        public bool UseUnversionedLegacyUrls { get; set; } = false;
        public Guid? CashboxId { get; set; }
        public string AccessToken { get; set; }
    }

    public enum HttpCommunicationType
    {
        Json,
        Xml
    }
}