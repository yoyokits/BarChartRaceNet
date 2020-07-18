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