using System;

namespace fiskaltrust.Middleware.Interface.Client.Http
{
    /// <summary>
    /// Set Client Options Communication type,Url, CashboxId and AccessToken.
    /// </summary>
    public class HttpPosClientOptions : ClientOptions
    {
        public Guid? CashboxId { get; set; }
        public string AccessToken { get; set; }
    }
}