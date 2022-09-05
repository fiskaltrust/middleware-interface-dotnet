using System;

namespace fiskaltrust.Middleware.Interface.Client.Common.Helpers
{
    public static class RuntimeHelpers
    {
        public static bool IsMono => Type.GetType("Mono.Runtime") != null;
    }
}
