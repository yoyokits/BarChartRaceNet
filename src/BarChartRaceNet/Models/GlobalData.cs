namespace BarChartRaceNet.Models
{
    using BarChartRaceNet.Common;
    using BarChartRaceNet.Extensions;
    using BarChartRaceNet.Helpers;
    using MahApps.Metro.Controls;
    using MahApps.Metro.Controls.Dialogs;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines the <see cref="GlobalData" />.
    /// </summary>
    public class GlobalData : NotifyPropertyChanged
    {
        #region Fields

        private IList<BarValuesModel> _barValuesModels;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="GlobalData"/> class.
        /// </summary>
        internal GlobalData()
        {
            this.SettingsModel = SettingsHelper.Load();
            if (this.SettingsModel == null)
            {
                this.SettingsModel = new SettingsModel();
            }
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets or sets the BarValuesModels.
        /// </summary>
        public IList<BarValuesModel> BarValuesModels { get => _barValuesModels; set => this.Set(this.PropertyChangedHandler, ref _barValuesModels, value); }

        /// <summary>
        /// Gets or sets the MainWindow.
        /// </summary>
        public MetroWindow MainWindow { get; internal set; }

        /// <summary>
        /// Gets the SettingsModel.
        /// </summary>
        public SettingsModel SettingsModel { get; }

        #endregion Properties

        #region Methods

        /// <summary>
        /// The ShowMessage.
        /// </summary>
        /// <param name="title">The title<see cref="string"/>.</param>
        /// <param name="message">The message<see cref="string"/>.</param>
        /// <param name="style">The style<see cref="MessageDialogStyle"/>.</param>
        /// <returns>The <see cref="Task{MessageDialogResult}"/>.</returns>
        internal async Task<MessageDialogResult> ShowMessageAsync(string title, string message, MessageDialogStyle style = MessageDialogStyle.Affirmative)
        {
            var dispatcher = this.MainWindow.Dispatcher;
            Task<MessageDialogResult> result = null;
            if (dispatcher.CheckAccess())
            {
                return this.MainWindow.ShowMessageAsync(title, message, style).Result;
            }
            else
            {
                await dispatcher.BeginInvoke((Action)(() => result = this.MainWindow.ShowMessageAsync(title, message, style)));
            }

            await result;
            return result.Result;
        }

        #endregion Methods
    }
}