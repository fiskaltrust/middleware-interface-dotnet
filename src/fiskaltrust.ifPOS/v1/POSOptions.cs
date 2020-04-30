using System;

namespace fiskaltrust.ifPOS.v1
{
    public class POSOptions
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