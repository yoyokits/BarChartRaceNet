namespace BarChartRaceNet.Helpers
{
    using BarChartRaceNet.Common;
    using BarChartRaceNet.Extensions;
    using BarChartRaceNet.Models;
    using BarChartRaceNet.Tools;
    using BarChartRaceNet.ViewModels;
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
        /// The AdjustArray.
        /// </summary>
        /// <param name="barValuesModels">The barValuesModels<see cref="IEnumerable{BarValuesModel}"/>.</param>
        public static void AdjustArray(this IEnumerable<BarValuesModel> barValuesModels)
        {
            foreach (var model in barValuesModels)
            {
                model.Ranks = model.Ranks.ExtendBorderArray();
                model.Times = model.Times.ExtendBorderArray();
                model.Values = model.Values.ExtendBorderArray();
            }
        }

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
        /// <typeparam name="T">.</typeparam>
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
        /// The ExtendBorderArray.
        /// </summary>
        /// <typeparam name="T">.</typeparam>
        /// <param name="array">The array<see cref="T[]"/>.</param>
        /// <returns>The <see cref="T[]"/>.</returns>
        public static T[] ExtendBorderArray<T>(this T[] array)
        {
            var extendedArray = new T[array.Length + 2];
            for (var i = 0; i < array.Length; i++)
            {
                extendedArray[i + 1] = array[i];
            }

            extendedArray[0] = array.First();
            extendedArray[extendedArray.Length - 1] = array.Last();
            return extendedArray;
        }

        /// <summary>
        /// The ExtendSingleLinearInterpolatedArray.
        /// </summary>
        /// <param name="array">The array<see cref="double[]"/>.</param>
        /// <returns>The <see cref="double[]"/>.</returns>
        public static double[] ExtendSingleLinearInterpolatedArray(this double[] array)
        {
            if (array.Length < 2)
            {
                throw new ArgumentException("Array length must be greater than 1");
            }

            var extendedArray = array.ExtendArray(2);
            for (var i = 1; i < extendedArray.Length - 1; i += 2)
            {
                extendedArray[i] = (extendedArray[i - 1] + extendedArray[i + 1]) * 0.5;
            }

            return extendedArray;
        }

        /// <summary>
        /// The Interpolate.
        /// </summary>
        /// <param name="models">The models<see cref="IList{BarValuesModel}"/>.</param>
        /// <param name="resolution">The resolution<see cref="int"/>.</param>
        public static void Interpolate(this IList<BarValuesModel> models, int resolution)
        {
            var xs = RangeDouble.Range(0, (models.First().Values.Length * 2) - 1);
            foreach (var valuesModel in models)
            {
                var stepRanks = valuesModel.Ranks.ExtendArray(2);
                var ranksForInterpolation = valuesModel.Ranks.ExtendSingleLinearInterpolatedArray();
                valuesModel.Times = valuesModel.Times.ExtendArray(2);
                valuesModel.Values = valuesModel.Values.ExtendSingleLinearInterpolatedArray();

                var ranksSpline = new NaturalSpline(xs, ranksForInterpolation, resolution);
                var valuesSpline = new NaturalSpline(xs, valuesModel.Values, resolution);
                valuesModel.RanksInterpolated = ranksSpline.interpolatedYs;
                valuesModel.RankSteps = stepRanks.ExtendArray(resolution);
                valuesModel.Times = valuesModel.Times.ExtendArray(resolution);
                valuesModel.ValuesInterpolated = valuesSpline.interpolatedYs;
            }
        }

        /// <summary>
        /// The UpdateBarModels.
        /// </summary>
        /// <param name="barModels">The barModels<see cref="ObservableCollection{BarModel}"/>.</param>
        /// <param name="barValuesModels">The barValuesModels<see cref="IList{BarValuesModel}"/>.</param>
        /// <param name="stringToImageUrlDictionary">The stringToImageUrlDictionary<see cref="Dictionary{string, string}"/>.</param>
        public static void UpdateBarModels(this IList<BarModel> barModels, IList<BarValuesModel> barValuesModels, Dictionary<string, string> stringToImageUrlDictionary)
        {
            barModels.Clear();
            foreach (var valuesModel in barValuesModels)
            {
                var barModel = new BarModel { Index = valuesModel.RankSteps[0], Name = valuesModel.Name, Value = valuesModel.Values[0] };
                var icon = stringToImageUrlDictionary.GetImageUrl(barModel.Name);
                if (!string.IsNullOrEmpty(icon))
                {
                    barModel.Icon = icon;
                }

                barModels.Add(barModel);
            }
        }

        /// <summary>
        /// The UpdateBarModelsData.
        /// </summary>
        /// <param name="barChartModel">The barChartModel<see cref="BarChartViewModel"/>.</param>
        /// <param name="barValuesModels">The barValuesModels<see cref="IList{BarValuesModel}"/>.</param>
        /// <param name="positionIndex">The positionIndex<see cref="int"/>.</param>
        public static void UpdateBarModelsData(this BarChartViewModel barChartModel, IList<BarValuesModel> barValuesModels, int positionIndex)
        {
            var barModels = barChartModel.BarModels;
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
                var offset = barChartModel.SortDirection == System.ComponentModel.ListSortDirection.Descending ? barModel.Index - valuesModel.RanksInterpolated[positionIndex] : valuesModel.RanksInterpolated[positionIndex] - barModel.Index;
                var indexOffset = offset * barModel.BarContainerHeight;

                /// Prevent the first winner is drawn outside the area.
                if (barModel.Index == barModels.Count - 1 && indexOffset < 0)
                {
                    indexOffset = 0;
                }

                barModel.IndexOffset = indexOffset;
                barModel.BarOpacity = 1 - (Math.Sin(offset.Abs() * Math.PI) * 0.7);
                var value = valuesModel.ValuesInterpolated[positionIndex];
                if (value < 0)
                {
                    value = 0;
                }

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

            if (min >= max || barChartModel.IsVisibleRangeFromZero)
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