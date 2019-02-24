using System;
using System.Collections.Generic;
using System.Linq;

namespace Sorting
{
    public static class InsertionSortExtension
    {
        public static IEnumerable<T> SortIns<T>(this IEnumerable<T> source) where T : IComparable<T>
        {
            if (source == null)
            {
                throw new ArgumentNullException();
            }

            var buff = source as T[] ?? source.ToArray();

            if (buff.Length == 0)
            {
                return buff;
            }

            for (var j = 1; j < buff.Length; j++)
            {
                var key = buff[j];
                var i = j - 1;
                while (i >= 0 && buff[i].CompareTo(key) > 0)
                {
                    buff[i + 1] = buff[i];
                    i = i - 1;
                }

                buff[i + 1] = key;
            }

            return buff;
        }
    }
}