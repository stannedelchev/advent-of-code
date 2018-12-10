using System.Collections.Generic;

namespace AdventOfCode2018.Common
{
    internal static class IEnumerableExtensions
    {
        public static IEnumerable<T> RepeatForever<T>(this IEnumerable<T> source)
        {
            while (true)
            {
                foreach (var item in source)
                {
                    yield return item;
                }
            }
        }

        public static T ProcessAndMove<T>(this IEnumerator<T> enumerator)
        {
            var result = enumerator.Current;
            enumerator.MoveNext();
            return result;
        }
    }
}
