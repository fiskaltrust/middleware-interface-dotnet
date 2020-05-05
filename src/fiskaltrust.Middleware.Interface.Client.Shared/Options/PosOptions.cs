using fiskaltrust.Middleware.Interface.Client.Shared.Options;
using System;

namespace fiskaltrust.Middleware.Interface.Client.Shared
{
    public class PosOptions
    {
        public Uri Url { get; set; }
        public RetryPolicyOptions RetryPolicyOptions { get; set; }
    }
}
