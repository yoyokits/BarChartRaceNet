namespace BarChartRaceNet.Converters
{
    using BarChartRaceNet.Extensions;
    using System;
    using System.Data;
    using System.Globalization;
    using System.Linq;
    using System.Windows.Data;

    /// <summary>
    /// Defines the <see cref="ArrayToDataViewConverter" />.
    /// </summary>
    public class ArrayToDataViewConverter : IValueConverter
    {
        #region Methods

        /// <summary>
        /// The Convert.
        /// </summary>
        /// <param name="value">The value<see cref="object"/>.</param>
        /// <param name="targetType">The targetType<see cref="Type"/>.</param>
        /// <param name="parameter">The parameter<see cref="object"/>.</param>
        /// <param name="culture">The culture<see cref="CultureInfo"/>.</param>
        /// <returns>The <see cref="object"/>.</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is string[][] array) || array.Length < 2 || !array.First().Any())
            {
                return null;
            }

            var dataTable = new DataTable();
            var firstArray = array.First();

            // Add columns with name "0", "1", "2", ...
            foreach (var header in firstArray)
            {
                var filteredHeader = header.RemoveForbiddenCharacters();
                dataTable.Columns.Add(new DataColumn(filteredHeader, typeof(string)));
            }

            // Add data to DataTable
            for (var row = 1; row < array.Length; row++)
            {
                dataTable.Rows.Add(array[row]);
            }

            return dataTable.DefaultView;
        }

        /// <summary>
        /// The ConvertBack.
        /// </summary>
        /// <param name="value">The value<see cref="object"/>.</param>
        /// <param name="targetType">The targetType<see cref="Type"/>.</param>
        /// <param name="parameter">The parameter<see cref="object"/>.</param>
        /// <param name="culture">The culture<see cref="CultureInfo"/>.</param>
        /// <returns>The <see cref="object"/>.</returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion Methods
    }
}