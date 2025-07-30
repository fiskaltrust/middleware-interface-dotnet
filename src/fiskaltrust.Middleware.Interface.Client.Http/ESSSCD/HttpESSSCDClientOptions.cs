#if SYSTEM_TEXT_JSON
using System;

namespace fiskaltrust.Middleware.Interface.Client.Http
{
    /// <summary>
    /// Used to pass client options to the underlying HTTP client
    /// </summary>
    public class HttpESSSCDClientOptions : ClientOptions
    {
        public bool? DisableSslValidation { get; set; }
        public Guid? CashboxId { get; set; }
        public string AccessToken { get; set; }
    }
}
#endif