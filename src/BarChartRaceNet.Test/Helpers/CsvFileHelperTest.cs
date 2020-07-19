namespace BarChartRaceNet.Test.Helpers
{
    using BarChartRaceNet.Helpers;
    using BarChartRaceNet.Test.TestHelpers;
    using FluentAssertions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Linq;

    /// <summary>
    /// Defines the <see cref="CsvFileHelperTest" />.
    /// </summary>
    [TestClass]
    public class CsvFileHelperTest
    {
        #region Methods

        /// <summary>
        /// The Load.
        /// </summary>
        [TestMethod]
        public void Load()
        {
            var records = CsvFileHelper.Load(TestData.CountryTestCsv);
            records.Should().NotBeNull();
            records.Length.Should().Be(7);
            records.First().Length.Should().Be(5);
        }

        #endregion Methods
    }
}