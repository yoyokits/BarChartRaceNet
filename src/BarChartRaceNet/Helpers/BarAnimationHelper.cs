namespace BarChartRaceNet.Helpers
{
    using BarChartRaceNet.Common;
    using BarChartRaceNet.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Defines the <see cref="BarAnimationHelper" />.
    /// </summary>
    public static class BarAnimationHelper
    {
        #region Methods

        /// <summary>
        /// The DatasetToBarValuesModels.
        /// </summary>
        /// <param name="dataset">The dataset<see cref="string[][]"/>.</param>
        /// <param name="models">The models<see cref="IList{BarValuesModel}"/>.</param>
        public static void DatasetToBarValuesModels(this string[][] dataset, IList<BarValuesModel> models)
        {
            models.Clear();
            var doubleDataSet = dataset.DatasetToDoubleArray();
            var rowCount = doubleDataSet.Length;
            var columnCount = doubleDataSet.First().Length;

            // Create BarValuesModel and fill the Values properties.
            for (var column = 0; column < columnCount; column++)
            {
                var valuesModel = new BarValuesModel { Name = dataset[0][column + 1] };
                models.Add(valuesModel);
                for (var row = 0; row < rowCount; row++)
                {
                    var cellValue = doubleDataSet[row][column];
                    valuesModel.Values.Add(cellValue);
                }
            }

            // Sort by the Values and calculate the ranks.
            var valueToColumnPairs = new SortedDictionary<double, int>(new DuplicateKeyComparer<double>());
            for (var row = 0; row < rowCount; row++)
            {
                // Insert the Values to SortedDictionary to calculate the ranks.
                valueToColumnPairs.Clear();
                for (var column = 0; column < columnCount; column++)
                {
                    var valuesModel = models[column];
                    valueToColumnPairs.Add(valuesModel.Values[row], column);
                }

                // From the SortedDictionary the ranks can be determined.
                var rank = 0;
                foreach (var pair in valueToColumnPairs)
                {
                    var column = pair.Value;
                    var valuesModel = models[column];
                    valuesModel.Ranks.Add(rank);
                    valuesModel.InterpolatedRanks.Add(rank);
                    rank++;
                }
            }
        }

        /// <summary>
        /// The DatasetToDoubleArray.
        /// </summary>
        /// <param name="dataset">The dataset<see cref="string[][]"/>.</param>
        /// <returns>The <see cref="double[][]"/>.</returns>
        public static double[][] DatasetToDoubleArray(this string[][] dataset)
        {
            var doubleArray = new double[dataset.Length - 1][];
            for (var i = 0; i < doubleArray.Length; i++)
            {
                doubleArray[i] = new double[dataset[0].Length - 1];
            }

            var rowCount = dataset.Length;
            var columnCount = dataset[0].Length;
            for (var row = 1; row < rowCount; row++)
            {
                for (var column = 1; column < columnCount; column++)
                {
                    if (double.TryParse(dataset[row][column], out var doubleValue))
                    {
                        doubleArray[row - 1][column - 1] = doubleValue;
                    }
                    else
                    {
                        var errorValue = dataset[row][column];
                        var headerMessage = $"{nameof(BarAnimationHelper)}.{nameof(DatasetToDoubleArray)} Error";
                        var contentMessage = $@"value ""{errorValue}"" in row {row} and column {column} is not a double.";
                        throw new InvalidCastException($"{headerMessage}: {contentMessage}");
                    }
                }
            }

            return doubleArray;
        }

        #endregion Methods
    }
}