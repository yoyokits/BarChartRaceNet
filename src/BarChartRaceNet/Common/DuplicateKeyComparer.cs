namespace BarChartRaceNet.Common
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Comparer for comparing two keys, handling equality as beeing greater
    /// Use this Comparer e.g. with SortedLists or SortedDictionaries, that don't allow duplicate keys.
    /// </summary>
    /// <typeparam name="TKey">.</typeparam>
    public class DuplicateKeyComparer<TKey> : IComparer<TKey> where TKey : IComparable
    {
        #region Methods

        /// <summary>
        /// The Compare.
        /// </summary>
        /// <param name="x">The x<see cref="TKey"/>.</param>
        /// <param name="y">The y<see cref="TKey"/>.</param>
        /// <returns>The <see cref="int"/>.</returns>
        public int Compare(TKey x, TKey y)
        {
            var result = x.CompareTo(y);

            // Handle equality as beeing greater
            return (result == 0) ? 1 : result;
        }

        #endregion Methods
    }
}