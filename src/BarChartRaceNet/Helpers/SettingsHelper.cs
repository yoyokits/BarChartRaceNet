namespace BarChartRaceNet.Helpers
{
    using BarChartRaceNet.Common;
    using BarChartRaceNet.Models;
    using BarChartRaceNet.ViewModels;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;
    using System;
    using System.IO;
    using System.Reflection;
    using System.Windows;

    /// <summary>
    /// Defines the <see cref="SettingsHelper" />.
    /// </summary>
    internal static class SettingsHelper
    {
        #region Constructors

        /// <summary>
        /// Initializes static members of the <see cref="SettingsHelper"/> class.
        /// </summary>
        static SettingsHelper()
        {
            var appName = Assembly.GetExecutingAssembly().GetName().Name;
            var userAppDataPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            UserJsonSettingsPath = Path.Combine(userAppDataPath, $@"{appName}\Settings.json");
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets the UserJsonSettingsPath.
        /// </summary>
        public static string UserJsonSettingsPath { get; }

        #endregion Properties

        #region Methods

        /// <summary>
        /// The Load.
        /// </summary>
        /// <returns>The <see cref="SettingsModel"/>.</returns>
        internal static SettingsModel Load()
        {
            Logger.Info($"Loading Browser setting {UserJsonSettingsPath}");
            if (!File.Exists(UserJsonSettingsPath))
            {
                return null;
            }

            try
            {
                using (StreamReader file = File.OpenText(UserJsonSettingsPath))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    var browserSettings = (SettingsModel)serializer.Deserialize(file, typeof(SettingsModel));
                    return browserSettings;
                }
            }
            catch (Exception e)
            {
                Logger.Error($"Error Loading Browser Setting: {e.Message}");
            }

            return null;
        }

        /// <summary>
        /// The Save.
        /// </summary>
        /// <param name="settings">The settings<see cref="SettingsModel"/>.</param>
        /// <param name="window">The window<see cref="Window"/>.</param>
        /// <param name="viewModel">The viewModel<see cref="MainWindowViewModel"/>.</param>
        internal static void Save(SettingsModel settings, Window window, MainWindowViewModel viewModel)
        {
            UpdateSettings(settings, window, viewModel);
            var settingsFolder = Path.GetDirectoryName(UserJsonSettingsPath);
            if (!Directory.Exists(settingsFolder))
            {
                Directory.CreateDirectory(settingsFolder);
            }

            var serializer = new JsonSerializer();
            serializer.Converters.Add(new JavaScriptDateTimeConverter());
            serializer.NullValueHandling = NullValueHandling.Ignore;

            using (var sw = new StreamWriter(UserJsonSettingsPath))
            using (var writer = new JsonTextWriter(sw))
            {
                serializer.Serialize(writer, settings);
            }

            Logger.Info($"Browser setting is saved in {UserJsonSettingsPath}");
        }

        /// <summary>
        /// The UpdateSettings.
        /// </summary>
        /// <param name="settings">The settings<see cref="SettingsModel"/>.</param>
        /// <param name="window">The window<see cref="Window"/>.</param>
        /// <param name="viewModel">The viewModel<see cref="MainWindowViewModel"/>.</param>
        internal static void UpdateSettings(SettingsModel settings, Window window, MainWindowViewModel viewModel)
        {
            settings.WindowPosition = new Point(window.Left, window.Top);
            settings.WindowWidth = window.ActualWidth;
            settings.WindowHeight = window.Height;
            settings.WindowState = window.WindowState;
        }

        #endregion Methods
    }
}