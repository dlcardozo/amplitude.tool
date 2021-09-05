using System.Collections.Generic;
using System.Linq;

namespace amplitude.tool.Utilities
{
    public static class EnumerableExtensions
    {
        public static bool SafeSequenceEqual<T>(this List<T> source, List<T> other) =>
            (source ?? new List<T>()).SequenceEqual(other ?? new List<T>());
    }
}