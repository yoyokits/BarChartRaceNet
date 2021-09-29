﻿namespace BarChartRaceNet.ViewModels
{
    using BarChartRaceNet.Common;
    using BarChartRaceNet.Core;
    using BarChartRaceNet.Helpers;
    using BarChartRaceNet.Models;
    using MahApps.Metro.Controls;
    using System;
    using System.Collections.ObjectModel;
    using System.IO;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Input;

    /// <summary>
    /// Defines the <see cref="MainWindowViewModel" />.
    /// </summary>
    public class MainWindowViewModel : IDisposable
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindowViewModel"/> class.
        /// </summary>
        public MainWindowViewModel()
        {
            this.DatasetViewModel = new DatasetViewModel(this.GlobalData);
            this.ChartEditorViewModel = new ChartEditorViewModel(this.GlobalData);
            this.ChartEditorViewModel.BarAnimationModel.ReloadDataAction = this.ReloadData;
            this.CancelRenderingButton = new CancelRenderingButton { CancelRenderingAction = this.OnCancelRendering };
            this.ToolBarButtons = new ObservableCollection<AddInButton>
            {
                new LoadButton { InitialDirectory = this.GlobalData.SettingsModel.InitialDirectory, LoadAction = this.OnLoadCsvFile },
                new ExportChartButton { ExportChartAction = this.OnExportChart },
                this.CancelRenderingButton,
                new AboutButton(this.GlobalData)
            };
            this.ClosingCommand = new RelayCommand(this.OnClosing);
            this.LoadedCommand = new RelayCommand(this.OnLoaded, nameof(this.LoadedCommand));
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets the CancelRenderingButton.
        /// </summary>
        public CancelRenderingButton CancelRenderingButton { get; }

        /// <summary>
        /// Gets the ChartEditorViewModel.
        /// </summary>
        public ChartEditorViewModel ChartEditorViewModel { get; }

        /// <summary>
        /// Gets the ClosingCommand.
        /// </summary>
        public ICommand ClosingCommand { get; }

        /// <summary>
        /// Gets the DatasetViewModel.
        /// </summary>
        public DatasetViewModel DatasetViewModel { get; }

        /// <summary>
        /// Gets the GlobalData.
        /// </summary>
        public GlobalData GlobalData { get; } = new GlobalData();

        /// <summary>
        /// Gets the LoadedCommand.
        /// </summary>
        public ICommand LoadedCommand { get; }

        /// <summary>
        /// Gets the ToolBarButtons.
        /// </summary>
        public ObservableCollection<AddInButton> ToolBarButtons { get; }

        #endregion Properties

        #region Methods

        /// <summary>
        /// The Dispose.
        /// </summary>
        public void Dispose()
        {
            this.ChartEditorViewModel.Dispose();
            this.SaveSettings();
        }

        /// <summary>
        /// The OnCancelRendering.
        /// </summary>
        private void OnCancelRendering()
        {
            this.ChartEditorViewModel.CancelRendering();
            this.CancelRenderingButton.IsEnabled = true;
        }

        /// <summary>
        /// The OnClosing.
        /// </summary>
        /// <param name="obj">The obj<see cref="object"/>.</param>
        private void OnClosing(object obj)
        {
            this.Dispose();
        }

        /// <summary>
        /// The OnExportChart.
        /// </summary>
        private void OnExportChart()
        {
            Task.Run(() =>
            {
                this.CancelRenderingButton.IsEnabled = true;
                this.ChartEditorViewModel.ExportChart();
            });
        }

        /// <summary>
        /// The OnLoadCsvFile.
        /// </summary>
        /// <param name="csvFilePath">The csvFilePath<see cref="string"/>.</param>
        private void OnLoadCsvFile(string csvFilePath)
        {
            this.DatasetViewModel.CsvFilePath = csvFilePath;
            this.ChartEditorViewModel.BarChartViewModel.Title = Path.GetFileName(csvFilePath);
        }

        /// <summary>
        /// The OnLoaded.
        /// </summary>
        /// <param name="obj">The obj<see cref="object"/>.</param>
        private void OnLoaded(object obj)
        {
            var (_, _, commandParameter) = (ValueTuple<object, EventArgs, object>)obj;
            var settings = this.GlobalData.SettingsModel;
            var window = (MetroWindow)commandParameter;
            window.Left = settings.WindowPosition.X;
            window.Top = settings.WindowPosition.Y;
            window.Width = settings.WindowWidth;
            window.Height = settings.WindowHeight;
            window.WindowState = settings.WindowState;
            this.GlobalData.MainWindow = window;
            window.Dispatcher.BeginInvoke((Action)(() => this.DatasetViewModel.CsvFilePath = this.GlobalData.SettingsModel.LastOpenedCsvFile));
        }

        /// <summary>
        /// The ReloadData.
        /// </summary>
        private void ReloadData()
        {
            this.SaveSettings();
            this.DatasetViewModel?.ParseStringArray();
        }

        /// <summary>
        /// The SaveSettings.
        /// </summary>
        private void SaveSettings()
        {
            this.ChartEditorViewModel.UpdateSettings();
            UIThreadHelper.Invoke(() =>
            {
                var window = this.GlobalData.MainWindow;
                var settings = this.GlobalData.SettingsModel;
                settings.WindowPosition = new Point(window.Left, window.Top);
                settings.WindowWidth = window.ActualWidth;
                settings.WindowHeight = window.Height;
                settings.WindowState = window.WindowState;
                SettingsHelper.Save(settings, window);
            });
        }

        #endregion Methods
    }
}