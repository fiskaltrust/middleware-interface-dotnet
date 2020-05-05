using fiskaltrust.Middleware.Interface.Client.Shared.Options;

namespace fiskaltrust.Middleware.Interface.Client.Shared
{
    public class PosOptions
    {
        public string Url { get; set; }
        public RetryPolicyOptions RetryPolicyOptions { get; set; }
    }
}
