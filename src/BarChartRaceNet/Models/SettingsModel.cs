namespace BarChartRaceNet.Models
{
    using System.Windows;

    /// <summary>
    /// Defines the <see cref="SettingsModel" />.
    /// </summary>
    public class SettingsModel
    {
        #region Properties

        /// <summary>
        /// Gets or sets the BarNameFontSize.
        /// </summary>
        public double BarNameFontSize { get; set; } = 18;

        /// <summary>
        /// Gets or sets the BarSpace.
        /// </summary>
        public double BarSpace { get; set; } = 10;

        /// <summary>
        /// Gets or sets the BarThickness.
        /// </summary>
        public double BarThickness { get; set; } = 64;

        /// <summary>
        /// Gets or sets the ChartHeight.
        /// </summary>
        public double ChartHeight { get; set; } = 1080;

        /// <summary>
        /// Gets or sets the ChartWidth.
        /// </summary>
        public double ChartWidth { get; set; } = 1920;

        /// <summary>
        /// Gets or sets the InitialDirectory.
        /// </summary>
        public string InitialDirectory { get; set; }

        /// <summary>
        /// Gets or sets the LastOpenedCsvFile.
        /// </summary>
        public string LastOpenedCsvFile { get; set; }

        /// <summary>
        /// Gets or sets the Subtitle.
        /// </summary>
        public string Subtitle { get; set; } = "Cekli chart subtitle";

        /// <summary>
        /// Gets or sets the SubtitleFontSize.
        /// </summary>
        public double SubtitleFontSize { get; set; } = 24;

        /// <summary>
        /// Gets or sets the Title.
        /// </summary>
        public string Title { get; set; } = "Cekli Bar Chart Title";

        /// <summary>
        /// Gets or sets the TitleFontSize.
        /// </summary>
        public double TitleFontSize { get; set; } = 32;

        /// <summary>
        /// Gets or sets the WindowHeight.
        /// </summary>
        public double WindowHeight { get; set; } = 1080;

        /// <summary>
        /// Gets or sets the WindowPosition.
        /// </summary>
        public Point WindowPosition { get; set; }

        /// <summary>
        /// Gets or sets the WindowState.
        /// </summary>
        public WindowState WindowState { get; set; } = WindowState.Normal;

        /// <summary>
        /// Gets or sets the WindowWidth.
        /// </summary>
        public double WindowWidth { get; set; } = 1920;

        /// <summary>
        /// Gets or sets the WorkingPath.
        /// </summary>
        public string WorkingPath { get; set; } = AppEnvironment.UserDocumentsFolder;

        #endregion Properties
    }
}