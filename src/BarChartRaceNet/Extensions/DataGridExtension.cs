namespace BarChartRaceNet.Extensions
{
    using System.Windows;
    using System.Windows.Controls;

    /// <summary>
    /// Defines the <see cref="DataGridExtension" />.
    /// </summary>
    public static class DataGridExtension
    {
        #region Fields

        public static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.RegisterAttached(nameof(ItemsSourceProperty).Name(), typeof(object), typeof(DataGridExtension), new PropertyMetadata(null, OnItemsSourceChanged));

        #endregion Fields

        #region Methods

        /// <summary>
        /// The GetItemsSource.
        /// </summary>
        /// <param name="obj">The obj<see cref="DependencyObject"/>.</param>
        /// <returns>The <see cref="object"/>.</returns>
        public static object GetItemsSource(DependencyObject obj)
        {
            return obj.GetValue(ItemsSourceProperty);
        }

        /// <summary>
        /// The SetItemsSource.
        /// </summary>
        /// <param name="obj">The obj<see cref="DependencyObject"/>.</param>
        /// <param name="value">The value<see cref="object"/>.</param>
        public static void SetItemsSource(DependencyObject obj, object value)
        {
            obj.SetValue(ItemsSourceProperty, value);
        }

        /// <summary>
        /// The OnItemsSourceChanged.
        /// </summary>
        /// <param name="d">The d<see cref="DependencyObject"/>.</param>
        /// <param name="e">The e<see cref="DependencyPropertyChangedEventArgs"/>.</param>
        private static void OnItemsSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var dataGrid = (DataGrid)d;
        }

        #endregion Methods
    }
}