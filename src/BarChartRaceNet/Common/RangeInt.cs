namespace BarChartRaceNet.Common
{
    using System;

    /// <summary>
    /// Defines the <see cref="RangeInt" />.
    /// </summary>
    public class RangeInt : Range<int>
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="RangeInt"/> class.
        /// </summary>
        /// <param name="start">The start<see cref="int"/>.</param>
        /// <param name="end">The end<see cref="int"/>.</param>
        /// <param name="allowInvert">The allowInvert<see cref="bool"/>.</param>
        public RangeInt(int start, int end, bool allowInvert = false) : base(start, end)
        {
            if (!allowInvert && start >= end)
            {
                throw new ArgumentException($"{nameof(start)} is equal or greater than {nameof(end)}");
            }
        }

        #endregion Constructors
    }
}