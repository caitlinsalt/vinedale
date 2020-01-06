using System;

namespace Tests.Utility.Providers
{
    public static class RandomProvider
    {
        public static Random Default { get; } = new Random();
    }
}
