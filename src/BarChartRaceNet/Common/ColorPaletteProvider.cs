namespace BarChartRaceNet.Common
{
    using System.Collections.Generic;
    using System.Reflection;
    using System.Windows.Media;

    /// <summary>
    /// Defines the <see cref="ColorPaletteProvider" />.
    /// </summary>
    public static class ColorPaletteProvider
    {
        #region Constructors

        /// <summary>
        /// Initializes static members of the <see cref="ColorPaletteProvider"/> class.
        /// </summary>
        static ColorPaletteProvider()
        {
            var colors = new List<Color>();
            var type = typeof(Colors);
            var colorProperties = type.GetProperties(BindingFlags.Public | BindingFlags.Static);
            foreach (var colorProperty in colorProperties)
            {
                var color = (Color)colorProperty.GetValue(null, null);
                colors.Add(color);
            }

            Colors = colors.ToArray();
            Count = Colors.Length;
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets the Colors.
        /// </summary>
        private static Color[] Colors { get; }

        /// <summary>
        /// Gets the Count.
        /// </summary>
        private static int Count { get; }

        /// <summary>
        /// Gets or sets the CurrentIndex.
        /// </summary>
        private static int CurrentIndex { get; set; }

        #endregion Properties

        #region Methods

        /// <summary>
        /// The GetNext.
        /// </summary>
        /// <returns>The <see cref="Color"/>.</returns>
        public static Color GetNext()
        {
            var color = Colors[CurrentIndex++];
            if (CurrentIndex >= Count)
            {
                CurrentIndex = 0;
            }

            return color;
        }

        #endregion Methods
    }
}