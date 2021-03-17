using System;

namespace fiskaltrust.Middleware.Interface.Client
{
    public class ClientOptions
    {
        /// <summary>
        /// Common options to configure on client side.
        /// </summary>
        public Uri Url { get; set; }
        public RetryPolicyOptions RetryPolicyOptions { get; set; }
    }
}
