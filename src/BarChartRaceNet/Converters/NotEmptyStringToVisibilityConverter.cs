namespace BarChartRaceNet.Converters
{
    using System;
    using System.Windows;

    /// <summary>
    /// Defines the <see cref="NotEmptySTringToVisibilityConverter{T}" />.
    /// </summary>
    public class NotEmptyStringToVisibilityConverter : ValueConverter
    {
        #region Methods

        /// <summary>
        /// The Convert.
        /// </summary>
        /// <param name="value">The value<see cref="object"/>.</param>
        /// <param name="targetType">The targetType<see cref="Type"/>.</param>
        /// <param name="parameter">The parameter<see cref="object"/>.</param>
        /// <param name="culture">The culture<see cref="System.Globalization.CultureInfo"/>.</param>
        /// <returns>The <see cref="object"/>.</returns>
        public override object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var text = value as string;
            return string.IsNullOrEmpty(text) ? Visibility.Collapsed : (object)Visibility.Visible;
        }

        /// <summary>
        /// The ConvertBack.
        /// </summary>
        /// <param name="value">The value<see cref="object"/>.</param>
        /// <param name="targetType">The targetType<see cref="Type"/>.</param>
        /// <param name="parameter">The parameter<see cref="object"/>.</param>
        /// <param name="culture">The culture<see cref="System.Globalization.CultureInfo"/>.</param>
        /// <returns>The <see cref="object"/>.</returns>
        public override object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion Methods
    }
}