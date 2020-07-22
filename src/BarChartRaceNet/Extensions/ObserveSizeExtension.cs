namespace BarChartRaceNet.Extensions
{
    using System.Windows;

    /// <summary>
    /// Defines the <see cref="ObserveSizeExtension" />.
    /// </summary>
    public static class ObserveSizeExtension
    {
        #region Fields

        public static readonly DependencyProperty HeightProperty =
            DependencyProperty.RegisterAttached(nameof(HeightProperty).Name(), typeof(double), typeof(ObserveSizeExtension), new PropertyMetadata(0.0));

        public static readonly DependencyProperty IsObservedProperty =
            DependencyProperty.RegisterAttached(nameof(IsObservedProperty).Name(), typeof(bool), typeof(ObserveSizeExtension), new PropertyMetadata(false, OnIsObserved));

        #endregion Fields

        #region Methods

        /// <summary>
        /// The GetHeight.
        /// </summary>
        /// <param name="obj">The obj<see cref="DependencyObject"/>.</param>
        /// <returns>The <see cref="double"/>.</returns>
        public static double GetHeight(DependencyObject obj)
        {
            return (double)obj.GetValue(HeightProperty);
        }

        /// <summary>
        /// The GetIsObserved.
        /// </summary>
        /// <param name="obj">The obj<see cref="DependencyObject"/>.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public static bool GetIsObserved(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsObservedProperty);
        }

        /// <summary>
        /// The SetHeight.
        /// </summary>
        /// <param name="obj">The obj<see cref="DependencyObject"/>.</param>
        /// <param name="value">The value<see cref="double"/>.</param>
        public static void SetHeight(DependencyObject obj, double value)
        {
            obj.SetValue(HeightProperty, value);
        }

        /// <summary>
        /// The SetIsObserved.
        /// </summary>
        /// <param name="obj">The obj<see cref="DependencyObject"/>.</param>
        /// <param name="value">The value<see cref="bool"/>.</param>
        public static void SetIsObserved(DependencyObject obj, bool value)
        {
            obj.SetValue(IsObservedProperty, value);
        }

        /// <summary>
        /// The OnElement_SizeChanged.
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/>.</param>
        /// <param name="e">The e<see cref="SizeChangedEventArgs"/>.</param>
        private static void OnElement_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            var element = (FrameworkElement)sender;
            SetHeight(element, e.NewSize.Height);
        }

        /// <summary>
        /// The OnIsObserved.
        /// </summary>
        /// <param name="d">The d<see cref="DependencyObject"/>.</param>
        /// <param name="e">The e<see cref="DependencyPropertyChangedEventArgs"/>.</param>
        private static void OnIsObserved(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var element = (FrameworkElement)d;
            if ((bool)e.NewValue)
            {
                element.SizeChanged += OnElement_SizeChanged;
            }
            else
            {
                element.SizeChanged -= OnElement_SizeChanged;
            }
        }

        #endregion Methods
    }
}