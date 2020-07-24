namespace BarChartRaceNet.ViewModels
{
    using BarChartRaceNet.Common;
    using BarChartRaceNet.Helpers;
    using BarChartRaceNet.Models;
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Input;

    /// <summary>
    /// Defines the <see cref="ChartEditorViewModel" />.
    /// </summary>
    public class ChartEditorViewModel : NotifyPropertyChanged, IDisposable
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ChartEditorViewModel"/> class.
        /// </summary>
        /// <param name="globalData">The globalData<see cref="GlobalData"/>.</param>
        internal ChartEditorViewModel(GlobalData globalData)
        {
            this.GlobalData = globalData;
            this.GlobalData.PropertyChanged += this.OnGlobalData_PropertyChanged;
            this.BarAnimationModel.PropertyChanged += this.OnBarAnimationModel_PropertyChanged;
            this.BarChartViewModel = new BarChartViewModel(this.GlobalData);
            this.BarChartViewLoadedCommand = new RelayCommand(this.OnBarChartViewLoaded, nameof(this.BarChartViewLoadedCommand));
            this.Initialize();
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets the BarAnimationModel.
        /// </summary>
        public BarAnimationModel BarAnimationModel { get; } = new BarAnimationModel();

        /// <summary>
        /// Gets or sets the BarChartView.
        /// </summary>
        public FrameworkElement BarChartView { get; set; }

        /// <summary>
        /// Gets the BarChartViewLoadedCommand.
        /// </summary>
        public ICommand BarChartViewLoadedCommand { get; }

        /// <summary>
        /// Gets the BarChartViewModel.
        /// </summary>
        public BarChartViewModel BarChartViewModel { get; }

        /// <summary>
        /// Gets the GlobalData.
        /// </summary>
        public GlobalData GlobalData { get; }

        #endregion Properties

        #region Methods

        /// <summary>
        /// The Dispose.
        /// </summary>
        public void Dispose()
        {
            var settings = this.GlobalData.SettingsModel;
            settings.BarNameFontSize = this.BarChartViewModel.BarNameFontSize;
            settings.BarSpace = this.BarChartViewModel.BarSpace;
            settings.BarThickness = this.BarChartViewModel.BarThickness;
            settings.ChartHeight = this.BarChartViewModel.Height;
            settings.ChartWidth = this.BarChartViewModel.Width;
            settings.Subtitle = this.BarChartViewModel.Subtitle;
            settings.SubtitleFontSize = this.BarChartViewModel.SubtitleFontSize;
            settings.Title = this.BarChartViewModel.Title;
            settings.TitleFontSize = this.BarChartViewModel.TitleFontSize;
        }

        /// <summary>
        /// The OnExportChart.
        /// </summary>
        /// <param name="filePath">The filePath<see cref="string"/>.</param>
        internal void OnExportChart(string filePath)
        {
            if (!filePath.ToLower().Contains(".mp4"))
            {
                filePath += ".mp4";
            }

            Task.Run(() =>
            {
                try
                {
                    FFMpegHelper.Record(filePath, this.BarChartView, this.OnDrawChart, this.BarAnimationModel.FrameCount);
                }
                catch (Exception e)
                {
                    this.GlobalData.ShowMessageAsync($"Export Failed", $"Error: {e.Message}");
                }
            });
        }

        /// <summary>
        /// The Initialize.
        /// </summary>
        private void Initialize()
        {
            var settings = this.GlobalData.SettingsModel;
            this.BarChartViewModel.BarNameFontSize = settings.BarNameFontSize;
            this.BarChartViewModel.BarSpace = settings.BarSpace;
            this.BarChartViewModel.BarThickness = settings.BarThickness;
            this.BarChartViewModel.Height = settings.ChartHeight;
            this.BarChartViewModel.Subtitle = settings.Subtitle;
            this.BarChartViewModel.SubtitleFontSize = settings.SubtitleFontSize;
            this.BarChartViewModel.Title = settings.Title;
            this.BarChartViewModel.TitleFontSize = settings.TitleFontSize;
            this.BarChartViewModel.Width = settings.ChartWidth;
        }

        /// <summary>
        /// The OnBarAnimationModel_PropertyChanged.
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/>.</param>
        /// <param name="e">The e<see cref="System.ComponentModel.PropertyChangedEventArgs"/>.</param>
        private void OnBarAnimationModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(this.BarAnimationModel.BarValuesModels):
                    this.BarChartViewModel.BarModels.UpdateBarModels(this.BarAnimationModel.BarValuesModels);
                    this.BarChartViewModel.SelectedBarModel = this.BarChartViewModel.BarModels.First();
                    this.BarAnimationModel.PositionIndex = 0;
                    break;

                case nameof(this.BarAnimationModel.PositionIndex):
                    this.BarChartViewModel.BarModels.UpdateBarModelsData(this.BarAnimationModel.BarValuesModels, this.BarAnimationModel.PositionIndex);
                    this.BarChartViewModel.Time = this.BarAnimationModel.BarValuesModels.First().Times[this.BarAnimationModel.PositionIndex];
                    break;

                default:
                    break;
            }
        }

        /// <summary>
        /// The OnBarChartViewLoaded.
        /// </summary>
        /// <param name="obj">The obj<see cref="object"/>.</param>
        private void OnBarChartViewLoaded(object obj)
        {
            var (view, _, _) = (ValueTuple<object, EventArgs, object>)obj;
            this.BarChartView = view as FrameworkElement;
        }

        /// <summary>
        /// The OnDrawChart.
        /// </summary>
        /// <param name="positionIndex">The positionIndex<see cref="int"/>.</param>
        private void OnDrawChart(int positionIndex)
        {
            // this.BarChartViewModel.Subtitle = $"Frame: {positionIndex}";
            this.BarAnimationModel.PositionIndex = positionIndex;
            this.BarChartView.UpdateLayout();
        }

        /// <summary>
        /// The OnGlobalData_PropertyChanged.
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/>.</param>
        /// <param name="e">The e<see cref="System.ComponentModel.PropertyChangedEventArgs"/>.</param>
        private void OnGlobalData_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(this.GlobalData.BarValuesModels):
                    this.BarAnimationModel.BarValuesModels = this.GlobalData.BarValuesModels;
                    break;

                default:
                    break;
            }
        }

        #endregion Methods
    }
}