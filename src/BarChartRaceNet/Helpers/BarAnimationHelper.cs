namespace BarChartRaceNet.Helpers
{
    using BarChartRaceNet.Common;
    using BarChartRaceNet.Extensions;
    using BarChartRaceNet.Models;
    using BarChartRaceNet.Tools;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
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
        /// <returns>The <see cref="IList{BarValuesModel}"/>.</returns>
        public static IList<BarValuesModel> DatasetToBarValuesModels(this string[][] dataset)
        {
            var models = new List<BarValuesModel>();
            var doubleDataSet = dataset.DatasetToDoubleArray();
            var rowCount = doubleDataSet.Length;
            var columnCount = doubleDataSet.First().Length;

            // Create BarValuesModel and fill the Values properties.
            for (var column = 0; column < columnCount; column++)
            {
                var valuesModel = new BarValuesModel { Name = dataset[0][column + 1] };
                valuesModel.Name = dataset[0][column + 1];
                valuesModel.Ranks = new double[rowCount];
                valuesModel.Times = new string[rowCount];
                valuesModel.Values = new double[rowCount];
                models.Add(valuesModel);
                for (var row = 0; row < rowCount; row++)
                {
                    var cellValue = doubleDataSet[row][column];
                    valuesModel.Values[row] = cellValue;
                    var time = dataset[row + 1][0];
                    valuesModel.Times[row] = time;
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
                    valuesModel.Ranks[row] = rank;
                    rank++;
                }
            }

            return models;
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

        /// <summary>
        /// The ExtendArray.
        /// </summary>
        /// <param name="array">The array<see cref="double[]"/>.</param>
        /// <param name="resolution">The resolution<see cref="int"/>.</param>
        /// <returns>The <see cref="double[]"/>.</returns>
        public static T[] ExtendArray<T>(this T[] array, int resolution)
        {
            if (array == null || !array.Any() || resolution < 2)
            {
                var message = $"Error {nameof(ExtendArray)}: {nameof(array)} is null or empty or resolution is less than 2";
                throw new ArgumentException(message);
            }

            var n = array.Length * resolution;
            var extendedArray = new T[n];
            var index = 0;
            for (var i = 0; i < array.Length; i++)
            {
                for (int j = 0; j < resolution; j++)
                {
                    extendedArray[index++] = array[i];
                }
            }

            return extendedArray;
        }

        /// <summary>
        /// The Interpolate.
        /// </summary>
        /// <param name="models">The models<see cref="IList{BarValuesModel}"/>.</param>
        /// <param name="resolution">The resolution<see cref="int"/>.</param>
        /// <returns>The <see cref="IList{BarValuesModel}"/>.</returns>
        public static void Interpolate(this IList<BarValuesModel> models, int resolution)
        {
            var xs = RangeDouble.Range(0, models.First().Values.Length - 1);
            foreach (var valuesModel in models)
            {
                var valuesSpline = new NaturalSpline(xs, valuesModel.Values, resolution);
                var ranksSpline = new NaturalSpline(xs, valuesModel.Ranks, resolution);
                valuesModel.RanksInterpolated = ranksSpline.interpolatedYs;
                valuesModel.RankSteps = valuesModel.Ranks.ExtendArray(resolution);
                valuesModel.Times = valuesModel.Times.ExtendArray(resolution);
                valuesModel.ValuesInterpolated = valuesSpline.interpolatedYs;
            }
        }

        /// <summary>
        /// The UpdateBarModels.
        /// </summary>
        /// <param name="barModels">The barModels<see cref="ObservableCollection{BarModel}"/>.</param>
        /// <param name="barValuesModels">The barValuesModels<see cref="IList{BarValuesModel}"/>.</param>
        public static void UpdateBarModels(this ObservableCollection<BarModel> barModels, IList<BarValuesModel> barValuesModels)
        {
            barModels.Clear();
            foreach (var valuesModel in barValuesModels)
            {
                var barModel = new BarModel { Index = valuesModel.RankSteps[0], Name = valuesModel.Name, Value = valuesModel.Values[0] };
                barModels.Add(barModel);
            }

            UpdateBarModelsData(barModels, barValuesModels, 0);
        }

        /// <summary>
        /// The UpdateBarModelsData.
        /// </summary>
        /// <param name="barModels">The barModels<see cref="ObservableCollection{BarModel}"/>.</param>
        /// <param name="barValuesModels">The barValuesModels<see cref="IList{BarValuesModel}"/>.</param>
        /// <param name="positionIndex">The positionIndex<see cref="int"/>.</param>
        public static void UpdateBarModelsData(this ObservableCollection<BarModel> barModels, IList<BarValuesModel> barValuesModels, int positionIndex)
        {
            if (barModels.Count != barValuesModels.Count)
            {
                throw new ArgumentException($"{nameof(UpdateBarModelsData)} Error: {nameof(barModels)} and {nameof(barValuesModels)} count is not equal.");
            }
            var min = double.MaxValue;
            var max = double.MinValue;
            for (var i = 0; i < barModels.Count; i++)
            {
                var barModel = barModels[i];
                var valuesModel = barValuesModels[i];
                barModel.IsSuspended = true;
                barModel.Index = valuesModel.RankSteps[positionIndex];
                var offset = (barModel.Index - valuesModel.RanksInterpolated[positionIndex]);
                barModel.BarOpacity = 1 - (Math.Sin(offset.Abs() * 0.7 * Math.PI) * 0.7);
                barModel.IndexOffset = offset * barModel.BarContainerHeight;
                barModel.Value = valuesModel.ValuesInterpolated[positionIndex];
                if (min > barModel.Value)
                {
                    min = barModel.Value;
                }

                if (max < barModel.Value)
                {
                    max = barModel.Value;
                }
            }

            if (min > 0)
            {
                min = 0;
            }

            var visibleRange = new RangeDouble(min, max);
            for (var i = 0; i < barModels.Count; i++)
            {
                barModels[i].VisibleRange = visibleRange;
                barModels[i].IsSuspended = false;
            }
        }

        #endregion Methods
    }
}