using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fiskaltrust.Middleware.Interface.Client.Shared
{
    public class RetryTimeoutException : TimeoutException
    {
        public RetryTimeoutException(string message) : base(message) { }

        public RetryTimeoutException(string message, Exception innerException) : base(message, innerException) { }

        public RetryTimeoutException() : base() { }
    }
}
