namespace BarChartRaceNet.ViewModels
{
    using BarChartRaceNet.Common;
    using BarChartRaceNet.Helpers;
    using BarChartRaceNet.Models;
    using Ookii.Dialogs.Wpf;
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
            this.RegenerateBarColorCommand = new RelayCommand(this.OnRegenerateBarColor, nameof(this.RegenerateBarColorCommand));
            this.InitialDirectory = this.GlobalData.SettingsModel.InitialDirectory;
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

        /// <summary>
        /// Gets the RegenerateBarColorCommand.
        /// </summary>
        public RelayCommand RegenerateBarColorCommand { get; }

        /// <summary>
        /// Gets or sets the InitialDirectory.
        /// </summary>
        internal string InitialDirectory { get; set; } = AppEnvironment.UserDocumentsFolder;

        #endregion Properties

        #region Methods

        /// <summary>
        /// The Dispose.
        /// </summary>
        public void Dispose()
        {
        }

        /// <summary>
        /// The OnExportChart.
        /// </summary>
        internal void OnExportChart()
        {
            var fileName = $"{this.BarChartViewModel.Title}.mp4";
            var dialog = new VistaSaveFileDialog
            {
                InitialDirectory = this.InitialDirectory,
                FileName = fileName,
                Filter = "MPEG4 files (*.mp4)|*.mp4"
            };
            if (!(bool)dialog.ShowDialog())
            {
                return;
            }

            var filePath = dialog.FileName;
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
        /// The UpdateBarModelData.
        /// </summary>
        internal void UpdateBarModelData()
        {
            this.BarChartViewModel.UpdateBarModelsData(this.BarAnimationModel.BarValuesModels, this.BarAnimationModel.PositionIndex);
        }

        /// <summary>
        /// The UpdateSettings.
        /// </summary>
        internal void UpdateSettings()
        {
            var settings = this.GlobalData.SettingsModel;
            settings.BackgroundImage = this.BarChartViewModel.BackgroundImage;
            settings.BackgroundImageOpacity = this.BarChartViewModel.BackgroundImageOpacity;
            settings.BackgroundImageWidth = this.BarChartViewModel.BackgroundImageWidth;
            settings.BarNameFontSize = this.BarChartViewModel.BarNameFontSize;
            settings.BarSpace = this.BarChartViewModel.BarSpace;
            settings.BarThickness = this.BarChartViewModel.BarThickness;
            settings.ChartHeight = this.BarChartViewModel.Height;
            settings.ChartWidth = this.BarChartViewModel.Width;
            settings.DecimalPlaces = this.BarChartViewModel.DecimalPlaces;
            settings.IsVisibleRangeFromZero = this.BarChartViewModel.IsVisibleRangeFromZero;
            settings.SortDirection = this.BarChartViewModel.SortDirection;
            settings.StatisticsMethod = this.BarChartViewModel.StatisticsMethod;
            settings.Subtitle = this.BarChartViewModel.Subtitle;
            settings.SubtitleFontSize = this.BarChartViewModel.SubtitleFontSize;
            settings.Title = this.BarChartViewModel.Title;
            settings.TitleFontSize = this.BarChartViewModel.TitleFontSize;
            var dict = settings.StringToImageUrlDictionary;
            foreach (var barModel in this.BarChartViewModel.BarModels)
            {
                var icon = barModel.Icon;
                if (icon != null && icon.Contains("http"))
                {
                    dict[barModel.Name] = icon;
                }
            }
        }

        /// <summary>
        /// The Initialize.
        /// </summary>
        private void Initialize()
        {
            var settings = this.GlobalData.SettingsModel;
            this.BarChartViewModel.BackgroundImage = settings.BackgroundImage;
            this.BarChartViewModel.BackgroundImageOpacity = settings.BackgroundImageOpacity;
            this.BarChartViewModel.BackgroundImageWidth = settings.BackgroundImageWidth;
            this.BarChartViewModel.BarNameFontSize = settings.BarNameFontSize;
            this.BarChartViewModel.BarSpace = settings.BarSpace;
            this.BarChartViewModel.BarThickness = settings.BarThickness;
            this.BarChartViewModel.DecimalPlaces = settings.DecimalPlaces;
            this.BarChartViewModel.IsVisibleRangeFromZero = settings.IsVisibleRangeFromZero;
            this.BarChartViewModel.Height = settings.ChartHeight;
            this.BarChartViewModel.SortDirection = settings.SortDirection;
            this.BarChartViewModel.StatisticsMethod = settings.StatisticsMethod;
            this.BarChartViewModel.Subtitle = settings.Subtitle;
            this.BarChartViewModel.SubtitleFontSize = settings.SubtitleFontSize;
            this.BarChartViewModel.Title = settings.Title;
            this.BarChartViewModel.TitleFontSize = settings.TitleFontSize;
            this.BarChartViewModel.Width = settings.ChartWidth;

            this.BarChartViewModel.PropertyChanged += this.OnBarChartViewModel_PropertyChanged;
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
                    this.BarChartViewModel.BarModels.UpdateBarModels(this.BarAnimationModel.BarValuesModels, this.GlobalData.SettingsModel.StringToImageUrlDictionary);
                    this.UpdateBarModelData();
                    this.BarChartViewModel.SelectedBarModel = this.BarChartViewModel.BarModels.First();
                    this.BarAnimationModel.PositionIndex = 0;
                    break;

                case nameof(this.BarAnimationModel.PositionIndex):
                    this.UpdateBarModelData();
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
        /// The OnBarChartViewModel_PropertyChanged.
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/>.</param>
        /// <param name="e">The e<see cref="System.ComponentModel.PropertyChangedEventArgs"/>.</param>
        private void OnBarChartViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(this.BarChartViewModel.IsVisibleRangeFromZero):
                    this.UpdateBarModelData();
                    break;

                default:
                    break;
            }
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
                    this.BarAnimationModel.PositionIndex = 0;
                    this.BarAnimationModel.BarValuesModels = this.GlobalData.BarValuesModels;
                    break;

                default:
                    break;
            }
        }

        /// <summary>
        /// The OnRegenerateBarColor.
        /// </summary>
        /// <param name="obj">The obj<see cref="object"/>.</param>
        private void OnRegenerateBarColor(object obj)
        {
            foreach (var barModel in this.BarChartViewModel.BarModels)
            {
                barModel.Color = ColorPaletteProvider.GetNext();
            }
        }

        #endregion Methods
    }
}