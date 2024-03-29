﻿namespace BarChartRaceNet.Test.Helpers
{
    using BarChartRaceNet.Helpers;
    using BarChartRaceNet.Test.TestHelpers;
    using FluentAssertions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Linq;

    /// <summary>
    /// Defines the <see cref="BarAnimationHelperTest" />.
    /// </summary>
    [TestClass]
    public class BarAnimationHelperTest
    {
        #region Methods

        /// <summary>
        /// The Test.
        /// </summary>
        [TestMethod]
        public void DatasetToBarValuesModels()
        {
            var array2D = CsvFileHelper.Load(TestData.CountryTestCsv);
            var models = array2D.DatasetToBarValuesModels();
            models.Should().NotBeNull();
            models.Count.Should().Be(4);
            foreach (var model in models)
            {
                model.Ranks.Length.Should().Be(6);
                model.Values.Length.Should().Be(6);
            }
        }

        /// <summary>
        /// The DatasetToDoubleArray.
        /// </summary>
        [TestMethod]
        public void DatasetToDoubleArray()
        {
            var array2D = CsvFileHelper.Load(TestData.CountryTestCsv);
            var doubleArray2D = array2D.DatasetToDoubleArray();
            doubleArray2D.Should().NotBeNull();
            doubleArray2D.Length.Should().Be(6);
            doubleArray2D.First().Length.Should().Be(4);
        }

        #endregion Methods
    }
}