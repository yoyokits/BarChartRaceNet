namespace BarChartRaceNet.Test.Tools
{
    using BarChartRaceNet.Core;
    using BarChartRaceNet.Tools;
    using FluentAssertions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="StatisticsTest" />.
    /// </summary>
    [TestClass]
    public class StatisticsTest
    {
        #region Methods

        /// <summary>
        /// The se.
        /// </summary>
        [TestMethod]
        public void Calculate()
        {
            var list = new List<double> { 1, 2, 3 };
            var output = list.Calculate(StatisticsMethod.Average, 1);
            output.Should().Be("Average: 2.0");
            output = list.Calculate(StatisticsMethod.PercentageAverage, 2);
            output.Should().Be("Average: 2.00%");
            output = list.Calculate(StatisticsMethod.Total, 2);
            output.Should().Be("Total: 6.00");
        }

        #endregion Methods
    }
}