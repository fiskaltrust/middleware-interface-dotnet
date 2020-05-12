using System;

namespace fiskaltrust.Middleware.Interface.Client
{
    public class ClientOptions
    {
        public Uri Url { get; set; }
        public RetryPolicyOptions RetryPolicyOptions { get; set; }
    }
}
