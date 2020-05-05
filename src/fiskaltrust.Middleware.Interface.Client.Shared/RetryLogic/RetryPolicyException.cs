using System;

namespace fiskaltrust.Middleware.Interface.Client.Shared
{
    public class RetryPolicyException : Exception
    {
        public RetryPolicyException(string message) : base(message) { }

        public RetryPolicyException(string message, Exception innerException) : base(message, innerException) { }

        public RetryPolicyException() : base() { }
    }
}
