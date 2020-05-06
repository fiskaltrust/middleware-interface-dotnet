using System;

namespace fiskaltrust.Middleware.Interface.Client.Shared.Options
{
    public class RetryPolicyOptions
    {
        public int Retries { get; set; }
        public TimeSpan DelayBetweenRetries { get; set; }
        public TimeSpan ClientTimeout { get; set; }

        public static RetryPolicyOptions Default => new RetryPolicyOptions
        {
            Retries = 3,
            DelayBetweenRetries = TimeSpan.FromSeconds(2),
            ClientTimeout = TimeSpan.FromSeconds(15)
        };
    }
}
