namespace BarChartRaceNet.ViewModels
{
    using BarChartRaceNet.Common;
    using BarChartRaceNet.Core;
    using BarChartRaceNet.Extensions;
    using BarChartRaceNet.Helpers;
    using BarChartRaceNet.Models;
    using BarChartRaceNet.Tools;
    using System;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.ComponentModel;
    using System.Linq;
    using System.Windows;
    using System.Windows.Input;
    using System.Windows.Media;

    /// <summary>
    /// Defines the <see cref="BarChartViewModel" />.
    /// </summary>
    public class BarChartViewModel : NotifyPropertyChanged
    {
        #region Fields

        private Brush _background = new SolidColorBrush(Colors.White);

        private string _backgroundImage;

        private double _backgroundImageOpacity = 1;

        private double _backgroundImageWidth = 800;

        private Thickness _barMargin;

        private double _barNameFontSize = 18;

        private double _barSpace;

        private double _barThickness = 64;

        private int _decimalPlaces;

        private double _height = 900;

        private RangeDouble _rangeX = new RangeDouble(0, 5);

        private RangeDouble _rangeY = new RangeDouble(0, 10);

        private BarModel _selectedBarModel;

        private StatisticsMethod _statisticsMethod = StatisticsMethod.Total;

        private string _statisticsOutputValue;

        private string _subtitle = "Chart sub title";

        private double _subtitleFontSize = 24;

        private string _time = "2020";

        private string _title = "Cekli Bar Chart Race";

        private double _titleFontSize = 32;

        private double _width = 1200;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="BarChartViewModel"/> class.
        /// </summary>
        /// <param name="globalData">The globalData<see cref="GlobalData"/>.</param>
        internal BarChartViewModel(GlobalData globalData)
        {
            this.GlobalData = globalData;
            this.BarModels = new ObservableCollection<BarModel>();
            this.BarModels.CollectionChanged += this.OnBarModels_CollectionChanged;
            this.SelectBarCommand = new RelayCommand(this.OnSelectBar, nameof(this.SelectBarCommand));
            this.BarSpace = 10;
            this.PropertyChanged += this.OnPropertyChanged;
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets or sets the BackgroundImage.
        /// </summary>
        public string BackgroundImage { get => _backgroundImage; set => this.Set(this.PropertyChangedHandler, ref _backgroundImage, value); }

        /// <summary>
        /// Gets or sets the BackgroundImageOpacity.
        /// </summary>
        public double BackgroundImageOpacity { get => _backgroundImageOpacity; set => this.Set(this.PropertyChangedHandler, ref _backgroundImageOpacity, value); }

        /// <summary>
        /// Gets or sets the BackgroundImageWidth.
        /// </summary>
        public double BackgroundImageWidth { get => _backgroundImageWidth; set => this.Set(this.PropertyChangedHandler, ref _backgroundImageWidth, value); }

        /// <summary>
        /// Gets the BarMargin.
        /// </summary>
        public Thickness BarMargin { get => _barMargin; private set => this.Set(this.PropertyChangedHandler, ref _barMargin, value); }

        /// <summary>
        /// Gets the BarModels.
        /// </summary>
        public ObservableCollection<BarModel> BarModels { get; }

        /// <summary>
        /// Gets or sets the BarNameFontSize.
        /// </summary>
        public double BarNameFontSize { get => _barNameFontSize; set => this.Set(this.PropertyChangedHandler, ref _barNameFontSize, value); }

        /// <summary>
        /// Gets or sets the BarSpace.
        /// </summary>
        public double BarSpace
        {
            get => _barSpace;
            set
            {
                if (!this.Set(this.PropertyChangedHandler, ref _barSpace, value, new RangeDouble(2, 10000)))
                {
                    return;
                }

                var margin = this.BarSpace / 2;
                this.BarMargin = new Thickness(0, margin, 0, margin);
            }
        }

        /// <summary>
        /// Gets or sets the BarThickness.
        /// </summary>
        public double BarThickness { get => _barThickness; set => this.Set(this.PropertyChangedHandler, ref _barThickness, value, new RangeDouble(10, 10000)); }

        /// <summary>
        /// Gets or sets the DecimalPlaces.
        /// </summary>
        public int DecimalPlaces { get => _decimalPlaces; set => this.Set(this.PropertyChangedHandler, ref _decimalPlaces, value); }

        /// <summary>
        /// Gets the GlobalData.
        /// </summary>
        public GlobalData GlobalData { get; }

        /// <summary>
        /// Gets or sets the Height.
        /// </summary>
        public double Height { get => _height; set => this.Set(this.PropertyChangedHandler, ref _height, value); }

        /// <summary>
        /// Gets or sets the RangeX.
        /// </summary>
        public RangeDouble RangeX { get => _rangeX; set => this.Set(this.PropertyChangedHandler, ref _rangeX, value); }

        /// <summary>
        /// Gets or sets the RangeY.
        /// </summary>
        public RangeDouble RangeY { get => _rangeY; set => this.Set(this.PropertyChangedHandler, ref _rangeY, value); }

        /// <summary>
        /// Gets the SelectBarCommand.
        /// </summary>
        public ICommand SelectBarCommand { get; }

        /// <summary>
        /// Gets or sets the SelectedBarModel.
        /// </summary>
        public BarModel SelectedBarModel { get => _selectedBarModel; set => this.Set(this.PropertyChangedHandler, ref _selectedBarModel, value); }

        /// <summary>
        /// Gets or sets the StatisticsMethod.
        /// </summary>
        public StatisticsMethod StatisticsMethod { get => _statisticsMethod; set => this.Set(this.PropertyChangedHandler, ref _statisticsMethod, value); }

        /// <summary>
        /// Gets or sets the StatisticsOutputValue.
        /// </summary>
        public string StatisticsOutputValue { get => _statisticsOutputValue; set => this.Set(this.PropertyChangedHandler, ref _statisticsOutputValue, value); }

        /// <summary>
        /// Gets or sets the Subtitle.
        /// </summary>
        public string Subtitle { get => _subtitle; set => this.Set(this.PropertyChangedHandler, ref _subtitle, value); }

        /// <summary>
        /// Gets or sets the SubtitleFontSize.
        /// </summary>
        public double SubtitleFontSize { get => _subtitleFontSize; set => this.Set(this.PropertyChangedHandler, ref _subtitleFontSize, value); }

        /// <summary>
        /// Gets or sets the Time.
        /// </summary>
        public string Time { get => _time; set => this.Set(this.PropertyChangedHandler, ref _time, value); }

        /// <summary>
        /// Gets or sets the Title.
        /// </summary>
        public string Title { get => _title; set => this.Set(this.PropertyChangedHandler, ref _title, value); }

        /// <summary>
        /// Gets or sets the TitleFontSize.
        /// </summary>
        public double TitleFontSize { get => _titleFontSize; set => this.Set(this.PropertyChangedHandler, ref _titleFontSize, value); }

        /// <summary>
        /// Gets or sets the Width.
        /// </summary>
        public double Width { get => _width; set => this.Set(this.PropertyChangedHandler, ref _width, value); }

        #endregion Properties

        #region Methods

        /// <summary>
        /// The CalculateStatisticsOutputValue.
        /// </summary>
        private void CalculateStatisticsOutputValue()
        {
            var value = this.BarModels.Select(model => model.Value).ToList();
            this.StatisticsOutputValue = value.Calculate(this.StatisticsMethod, this.DecimalPlaces);
        }

        /// <summary>
        /// The GetValue.
        /// </summary>
        /// <param name="value">The value<see cref="double"/>.</param>
        /// <returns>The <see cref="string"/>.</returns>
        private string GetValue(double value)
        {
            var isPercentage = this.StatisticsMethod == StatisticsMethod.Percentage || this.StatisticsMethod == StatisticsMethod.PercentageAverage;
            var percentChar = isPercentage ? "%" : string.Empty;
            var stringValue = $"{value.Format(this.DecimalPlaces)}{percentChar}";
            return stringValue;
        }

        /// <summary>
        /// The OnBarModels_CollectionChanged.
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/>.</param>
        /// <param name="e">The e<see cref="NotifyCollectionChangedEventArgs"/>.</param>
        private void OnBarModels_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.OldItems != null)
            {
                foreach (BarModel barModel in e.OldItems)
                {
                    barModel.PropertyChanged -= OnItem_PropertyChanged;
                    barModel.GetValueFunc = this.GetValue;
                }
            }
            if (e.NewItems != null)
            {
                foreach (BarModel barModel in e.NewItems)
                {
                    barModel.PropertyChanged += OnItem_PropertyChanged;
                    barModel.GetValueFunc = this.GetValue;
                }
            }
        }

        /// <summary>
        /// The OnItem_PropertyChanged.
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/>.</param>
        /// <param name="e">The e<see cref="PropertyChangedEventArgs"/>.</param>
        private void OnItem_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            var stringToImageUrlDict = this.GlobalData.SettingsModel.StringToImageUrlDictionary;
            var barModel = sender as BarModel;
            switch (e.PropertyName)
            {
                case nameof(BarModel.Name):
                    var url = stringToImageUrlDict.GetImageUrl(barModel.Name);
                    if (!string.IsNullOrEmpty(url))
                    {
                        barModel.Icon = url;
                    }

                    break;

                case nameof(BarModel.Value):
                    this.CalculateStatisticsOutputValue();
                    break;

                default:
                    break;
            }
        }

        /// <summary>
        /// The OnPropertyChanged.
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/>.</param>
        /// <param name="e">The e<see cref="PropertyChangedEventArgs"/>.</param>
        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(this.DecimalPlaces) || e.PropertyName == nameof(this.StatisticsMethod))
            {
                this.CalculateStatisticsOutputValue();
            }
        }

        /// <summary>
        /// The OnSelectBar.
        /// </summary>
        /// <param name="obj">The obj<see cref="object"/>.</param>
        private void OnSelectBar(object obj)
        {
            var (_, _, commandParameter) = (ValueTuple<object, EventArgs, object>)obj;
            var barModel = commandParameter as BarModel;
            this.SelectedBarModel = barModel;
        }

        #endregion Methods
    }
}