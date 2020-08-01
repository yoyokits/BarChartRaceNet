namespace BarChartRaceNet.Models
{
    using BarChartRaceNet.Core;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Windows;

    /// <summary>
    /// Defines the <see cref="SettingsModel" />.
    /// </summary>
    public class SettingsModel
    {
        #region Properties

        /// <summary>
        /// Gets or sets the BackgroundImage.
        /// </summary>
        public string BackgroundImage { get; set; }

        /// <summary>
        /// Gets or sets the BackgroundImageOpacity.
        /// </summary>
        public double BackgroundImageOpacity { get; set; } = 1;

        /// <summary>
        /// Gets or sets the BackgroundImageWidth.
        /// </summary>
        public double BackgroundImageWidth { get; set; } = 800;

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
        /// Gets or sets the DecimalPlaces.
        /// </summary>
        public int DecimalPlaces { get; set; }

        /// <summary>
        /// Gets or sets the InitialDirectory.
        /// </summary>
        public string InitialDirectory { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether IsVisibleRangeFromZero.
        /// </summary>
        public bool IsVisibleRangeFromZero { get; set; } = true;

        /// <summary>
        /// Gets or sets the LastOpenedCsvFile.
        /// </summary>
        public string LastOpenedCsvFile { get; set; }

        /// <summary>
        /// Gets or sets the SortDirection.
        /// </summary>
        public ListSortDirection SortDirection { get; set; }

        /// <summary>
        /// Gets or sets the StatisticsMethod.
        /// </summary>
        public StatisticsMethod StatisticsMethod { get; set; } = StatisticsMethod.Total;

        /// <summary>
        /// Gets or sets the StringToImageUrlDictionary.
        /// </summary>
        public Dictionary<string, string> StringToImageUrlDictionary { get; set; } = new Dictionary<string, string>();

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