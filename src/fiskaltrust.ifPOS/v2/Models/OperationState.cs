using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace fiskaltrust.Middleware.ifPOS.v2.Models{
    public static class OperationState
    {
        public const string PENDING = "pending";
        public const string PROCESSING = "processing";
        public const string DONE = "done";
        public const string FAILED = "failed";
        public const string UNKNOWN = "unknown";
    }
}