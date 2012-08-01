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

        /// <summary>
        /// Transform a Source enumerable (possibly an enumerable of enumerables) into a different type of enumerable
        /// </summary>
        /// <typeparam name="T">Starting type</typeparam>
        /// <typeparam name="Result">Resultant Enumerable type</typeparam>
        /// <param name="source"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static IEnumerable<Result> Select<T, Result>(this IEnumerable<T> source, Func<T, IEnumerable<Result>> action)
        {
            if (action == null)
                throw new ArgumentNullException("action");

            List<Result> arr = new List<Result>();
            foreach (var item in source)
                arr.AddRange(action(item));

            return arr;
        }

        /// <summary>
        /// Transform a Source enumerable into a different type of enumerable
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="Result"></typeparam>
        /// <param name="source"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static IEnumerable<Result> Select<T, Result>(this IEnumerable<T> source, Func<T, Result> action)
        {
            if (action == null)
                throw new ArgumentNullException("action");

            List<Result> arr = new List<Result>();
            foreach (var item in source)
                arr.Add(action(item));

            return arr;
        }
    }
}
