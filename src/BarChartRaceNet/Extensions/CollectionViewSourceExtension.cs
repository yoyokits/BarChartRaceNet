namespace BarChartRaceNet.Extensions
{
    using System.ComponentModel;
    using System.Windows;
    using System.Windows.Data;

    /// <summary>
    /// Defines the <see cref="CollectionViewSourceExtension" />.
    /// </summary>
    public static class CollectionViewSourceExtension
    {
        #region Fields

        public static readonly DependencyProperty PropertyNameProperty =
            DependencyProperty.RegisterAttached(nameof(PropertyNameProperty).Name(), typeof(string), typeof(CollectionViewSourceExtension), new PropertyMetadata(null, OnPropertyNameChanged));

        public static readonly DependencyProperty SortDirectionProperty =
            DependencyProperty.RegisterAttached(nameof(SortDirectionProperty).Name(), typeof(ListSortDirection), typeof(CollectionViewSourceExtension), new PropertyMetadata(ListSortDirection.Descending, OnSortDirectionChanged));

        #endregion Fields

        #region Methods

        /// <summary>
        /// The GetPropertyName.
        /// </summary>
        /// <param name="obj">The obj<see cref="DependencyObject"/>.</param>
        /// <returns>The <see cref="string"/>.</returns>
        public static string GetPropertyName(DependencyObject obj)
        {
            return (string)obj.GetValue(PropertyNameProperty);
        }

        /// <summary>
        /// The GetSortDirection.
        /// </summary>
        /// <param name="obj">The obj<see cref="DependencyObject"/>.</param>
        /// <returns>The <see cref="ListSortDirection"/>.</returns>
        public static ListSortDirection GetSortDirection(DependencyObject obj) => (ListSortDirection)obj.GetValue(SortDirectionProperty);

        /// <summary>
        /// The SetPropertyName.
        /// </summary>
        /// <param name="obj">The obj<see cref="DependencyObject"/>.</param>
        /// <param name="value">The value<see cref="string"/>.</param>
        public static void SetPropertyName(DependencyObject obj, string value)
        {
            obj.SetValue(PropertyNameProperty, value);
        }

        /// <summary>
        /// The SetSortDirection.
        /// </summary>
        /// <param name="obj">The obj<see cref="DependencyObject"/>.</param>
        /// <param name="value">The value<see cref="ListSortDirection"/>.</param>
        public static void SetSortDirection(DependencyObject obj, ListSortDirection value) => obj.SetValue(SortDirectionProperty, value);

        /// <summary>
        /// The OnPropertyNameChanged.
        /// </summary>
        /// <param name="d">The d<see cref="DependencyObject"/>.</param>
        /// <param name="e">The e<see cref="DependencyPropertyChangedEventArgs"/>.</param>
        private static void OnPropertyNameChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var viewSource = (CollectionViewSource)d;
            UpdateSortDirection(viewSource);
        }

        /// <summary>
        /// The OnSortDirectionChanged.
        /// </summary>
        /// <param name="d">The d<see cref="DependencyObject"/>.</param>
        /// <param name="e">The e<see cref="DependencyPropertyChangedEventArgs"/>.</param>
        private static void OnSortDirectionChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var viewSource = (CollectionViewSource)d;
            UpdateSortDirection(viewSource);
        }

        /// <summary>
        /// The UpdateSortDirection.
        /// </summary>
        /// <param name="viewSource">The viewSource<see cref="CollectionViewSource"/>.</param>
        private static void UpdateSortDirection(CollectionViewSource viewSource)
        {
            var propertyName = GetPropertyName(viewSource);
            var direction = GetSortDirection(viewSource);
            if (string.IsNullOrEmpty(propertyName))
            {
                return;
            }

            viewSource.SortDescriptions.Clear();
            viewSource.SortDescriptions.Add(new SortDescription { Direction = direction, PropertyName = propertyName });
        }

        #endregion Methods
    }
}