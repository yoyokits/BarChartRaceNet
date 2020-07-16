namespace BarChartRaceNet.Common
{
    using System;

    /// <summary>
    /// Defines the <see cref="RangeDouble" />.
    /// </summary>
    public class RangeDouble : Range<double>
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="RangeDouble"/> class.
        /// </summary>
        /// <param name="start">The start<see cref="double"/>.</param>
        /// <param name="end">The end<see cref="double"/>.</param>
        /// <param name="allowInvert">The allowInvert<see cref="bool"/>.</param>
        public RangeDouble(double start, double end, bool allowInvert = false) : base(start, end)
        {
            if (!allowInvert && start >= end)
            {
                throw new ArgumentException($"{nameof(start)} is equal or greater than {nameof(end)}");
            }
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets the Empty.
        /// </summary>
        public static RangeDouble Empty { get; } = new RangeDouble(0, 0);

        #endregion Properties
    }
}