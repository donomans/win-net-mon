using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RemoteMon_Lib
{
    public static class EnumerableExtensionMethods
    {
        public static IEnumerable<T> Map<T>(this IEnumerable<T> source, Action<T> action)
        {
            if (action == null)
                throw new ArgumentNullException("action");

            foreach (var item in source)
                action(item);

            return source;
        }

        public static IEnumerable<T> ToArray<T>(this IEnumerable<T> source, Func<T, T[]> action)
        {            
            if (action == null)
                throw new ArgumentNullException("action");

            List<T> arr = new List<T>();

            foreach (var item in source)
                arr.AddRange(action(item));

            return arr.ToArray();
        }

        public static IEnumerable<Result> ToSomething<T, Result>(this IEnumerable<T> source, Func<T, IEnumerable<Result>> action)
        {
            if (action == null)
                throw new ArgumentNullException("action");

            List<Result> arr = new List<Result>();
            foreach (var item in source)
                arr.AddRange(action(item));

            return arr;
        }
    }
}
