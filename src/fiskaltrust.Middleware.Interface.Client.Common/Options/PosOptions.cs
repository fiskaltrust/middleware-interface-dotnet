using System;

namespace fiskaltrust.Middleware.Interface.Client
{
    public class PosOptions
    {
        public Uri Url { get; set; }
        public RetryPolicyOptions RetryPolicyOptions { get; set; }
    }
}
