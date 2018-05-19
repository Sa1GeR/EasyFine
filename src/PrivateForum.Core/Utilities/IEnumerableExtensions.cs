using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivateForum.Core.Utilities
{
    public static class IEnumerableExtensions
    {
        public static IEnumerable<IGrouping<int, TSource>> GroupBy<TSource>
              (this IEnumerable<TSource> source, int itemsPerGroup)
        {
            return source.Zip(Enumerable.Range(0, source.Count()),
                              (s, r) => new { Group = r / itemsPerGroup, Item = s })
                        .GroupBy(i => i.Group, g => g.Item)
                        .ToList();
        }

        public static ICollection<T> AddRange<T>(this ICollection<T> collection, ICollection<T> itemsToAdd)
        {
            foreach (var item in itemsToAdd)
            {
                if (!collection.Contains(item))
                {
                    collection.Add(item);
                }
            }
            return collection;
        }
    }
}
