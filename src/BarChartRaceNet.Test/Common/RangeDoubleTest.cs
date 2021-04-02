namespace BarChartRaceNet.Test.Common
{
    using BarChartRaceNet.Common;
    using FluentAssertions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Defines the <see cref="RangeDoubleTest" />.
    /// </summary>
    [TestClass]
    public class RangeDoubleTest
    {
        #region Methods

        /// <summary>
        /// The Range.
        /// </summary>
        [TestMethod]
        public void Range()
        {
            var range = RangeDouble.Range(0, 1);
            range.Length.Should().Be(2);
            range[0].Should().Be(0);
            range[1].Should().Be(1);

            range = RangeDouble.Range(-1, 2);
            range.Length.Should().Be(4);
            range[0].Should().Be(-1);
            range[1].Should().Be(0);
            range[2].Should().Be(1);
            range[3].Should().Be(2);
        }

        #endregion Methods
    }
}