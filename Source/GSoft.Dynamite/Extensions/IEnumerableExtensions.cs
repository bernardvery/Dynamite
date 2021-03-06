﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace GSoft.Dynamite.Extensions
{
    /// <summary>
    /// IEnumerable extension methods.
    /// </summary>
    public static class EnumerableExtensions
    {
        /// <summary>
        /// Distinct results by property value.
        /// </summary>
        /// <typeparam name="TSource">The type of the source.</typeparam>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="keySelector">The key selector.</param>
        /// <returns>Distinct results based on property value.</returns>
        public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            var seenKeys = new HashSet<TKey>();
            foreach (TSource element in source)
            {
                if (seenKeys.Add(keySelector(element)))
                {
                    yield return element;
                }
            }
        }
    }
}
