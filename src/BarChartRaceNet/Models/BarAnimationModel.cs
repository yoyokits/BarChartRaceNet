namespace BarChartRaceNet.Models
{
    using BarChartRaceNet.Common;
    using BarChartRaceNet.Extensions;
    using BarChartRaceNet.Helpers;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Defines the <see cref="BarAnimationModel" />.
    /// </summary>
    public class BarAnimationModel : NotifyPropertyChanged
    {
        #region Fields

        private IList<BarValuesModel> _barValuesModels;

        private double _durationPerSampleInSeconds = 1;

        private double _framePerSecond = 25;

        private double _height = 800;

        private int _positionIndex;

        private double _width = 600;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="BarAnimationModel"/> class.
        /// </summary>
        internal BarAnimationModel()
        {
            this.PropertyChanged += this.OnPropertyChanged;
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets or sets the BarValuesModels.
        /// </summary>
        public IList<BarValuesModel> BarValuesModels { get => _barValuesModels; set => this.Set(this.PropertyChangedHandler, ref _barValuesModels, value); }

        /// <summary>
        /// Gets or sets the DurationPerSampleInSeconds.
        /// </summary>
        public double DurationPerSampleInSeconds { get => _durationPerSampleInSeconds; set => this.Set(this.PropertyChangedHandler, ref _durationPerSampleInSeconds, value); }

        /// <summary>
        /// Gets the FrameCount.
        /// </summary>
        public int FrameCount => this.BarValuesModels.Any() ? this.BarValuesModels.First().ValuesInterpolated.Length : 0;

        /// <summary>
        /// Gets or sets the FramePerSecond.
        /// </summary>
        public double FramePerSecond { get => _framePerSecond; set => this.Set(this.PropertyChangedHandler, ref _framePerSecond, value); }

        /// <summary>
        /// Gets or sets the Height.
        /// </summary>
        public double Height { get => _height; set => this.Set(this.PropertyChangedHandler, ref _height, value); }

        /// <summary>
        /// Gets the MaxPositionIndex.
        /// </summary>
        public int MaxPositionIndex => this.BarValuesModels == null || !this.BarValuesModels.Any() || this.BarValuesModels.First().ValuesInterpolated == null || !this.BarValuesModels.First().ValuesInterpolated.Any() ?
            0 : this.BarValuesModels.First().ValuesInterpolated.Length - 1;

        /// <summary>
        /// Gets or sets the PositionIndex.
        /// </summary>
        public int PositionIndex
        {
            get => _positionIndex;
            set
            {
                if (_positionIndex == value || _positionIndex < 0 || _positionIndex > this.MaxPositionIndex)
                {
                    this.Warn($"Set {nameof(this.PositionIndex)} Error: {value} is outside range {0}-{this.MaxPositionIndex}");
                    return;
                }

                _positionIndex = value;
                this.OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the Width.
        /// </summary>
        public double Width { get => _width; set => this.Set(this.PropertyChangedHandler, ref _width, value); }

        #endregion Properties

        #region Methods

        /// <summary>
        /// The OnPropertyChanged.
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/>.</param>
        /// <param name="e">The e<see cref="System.ComponentModel.PropertyChangedEventArgs"/>.</param>
        private void OnPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(this.BarValuesModels):
                    this.BarValuesModels.Interpolate((int)(this.FramePerSecond * this.DurationPerSampleInSeconds).Round());
                    break;

                default:
                    break;
            }
        }

        #endregion Methods
    }
}