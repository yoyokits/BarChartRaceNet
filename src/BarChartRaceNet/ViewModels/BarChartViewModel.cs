namespace BarChartRaceNet.ViewModels
{
    using BarChartRaceNet.Common;
    using BarChartRaceNet.Extensions;
    using BarChartRaceNet.Models;
    using System.Collections.ObjectModel;
    using System.Windows;

    /// <summary>
    /// Defines the <see cref="BarChartViewModel" />.
    /// </summary>
    public class BarChartViewModel : NotifyPropertyChanged
    {
        #region Fields

        private Thickness _barMargin;

        private double _barSpace = 4;

        private double _barWidth = 30;

        private RangeDouble _rangeX = new RangeDouble(0, 5);

        private RangeDouble _rangeY = new RangeDouble(0, 10);

        private string _subTitle;

        private string _title = "Cekli Bar Chart Race";

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="BarChartViewModel"/> class.
        /// </summary>
        public BarChartViewModel()
        {
            this.BarModels = new ObservableCollection<BarModel>();
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets the BarMargin.
        /// </summary>
        public Thickness BarMargin { get => _barMargin; private set => this.Set(this.PropertyChangedHandler, ref _barMargin, value); }

        /// <summary>
        /// Gets the BarModels.
        /// </summary>
        public ObservableCollection<BarModel> BarModels { get; }

        /// <summary>
        /// Gets or sets the BarSpace.
        /// </summary>
        public double BarSpace
        {
            get => _barSpace; set
            {
                if (!this.Set(this.PropertyChangedHandler, ref _barSpace, value, new RangeDouble(10, 10000)))
                {
                    return;
                }

                var margin = this.BarSpace / 2;
                this.BarMargin = new Thickness(0, margin, 0, margin);
            }
        }

        /// <summary>
        /// Gets or sets the BarWidth.
        /// </summary>
        public double BarWidth { get => _barWidth; set => this.Set(this.PropertyChangedHandler, ref _barWidth, value, new RangeDouble(10, 10000)); }

        /// <summary>
        /// Gets or sets the RangeX.
        /// </summary>
        public RangeDouble RangeX { get => _rangeX; set => this.Set(this.PropertyChangedHandler, ref _rangeX, value); }

        /// <summary>
        /// Gets or sets the RangeY.
        /// </summary>
        public RangeDouble RangeY { get => _rangeY; set => this.Set(this.PropertyChangedHandler, ref _rangeY, value); }

        /// <summary>
        /// Gets or sets the SubTitle.
        /// </summary>
        public string SubTitle { get => _subTitle; set => this.Set(this.PropertyChangedHandler, ref _subTitle, value); }

        /// <summary>
        /// Gets or sets the Title.
        /// </summary>
        public string Title { get => _title; set => this.Set(this.PropertyChangedHandler, ref _title, value); }

        #endregion Properties
    }
}