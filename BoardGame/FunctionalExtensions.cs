using System.Collections.Generic;
using System.Linq;

namespace BoardGame
{
    public static class FunctionalExtensions
    {
        public static IEnumerable<IEnumerable<T>> Partition<T>(this IEnumerable<T> l, int sizeOfSubsequences)
        {
            return l.Select((x, i) => new { Group = i / sizeOfSubsequences, Value = x })
                .GroupBy(item => item.Group, g => g.Value)
                .Select(g => g.Where(x => true));
        }
    }
}
