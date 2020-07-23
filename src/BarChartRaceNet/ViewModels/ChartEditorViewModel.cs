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
    public class ChartEditorViewModel : NotifyPropertyChanged
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
            // this.BarChartViewModel.SubTitle = $"Frame: {positionIndex}";
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