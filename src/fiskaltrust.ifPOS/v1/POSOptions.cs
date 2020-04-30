namespace fiskaltrust.ifPOS.v1
{
    public class POSOptions
    {
        public string Url { get; set; }
        public HttpCommunicationType CommunicationType { get; set; }
    }

    public enum HttpCommunicationType
    {
        Json,
        Xml
    }
}