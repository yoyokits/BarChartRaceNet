namespace BarChartRaceNet.Models
{
    using BarChartRaceNet.Common;
    using BarChartRaceNet.Extensions;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Defines the <see cref="BarAnimationModel" />.
    /// </summary>
    public class BarAnimationModel : NotifyPropertyChanged
    {
        #region Fields

        private int _currentIndex;

        private double _framePerSecond = 24;

        private double _height = 800;

        private double _width = 600;

        #endregion Fields

        #region Properties

        /// <summary>
        /// Gets the BarValuesInterpolatedModels.
        /// </summary>
        public IList<BarValuesModel> BarValuesInterpolatedModels { get; } = new List<BarValuesModel>();

        /// <summary>
        /// Gets the BarValuesModels.
        /// </summary>
        public IList<BarValuesModel> BarValuesModels { get; } = new List<BarValuesModel>();

        /// <summary>
        /// Gets or sets the CurrentIndex.
        /// </summary>
        public int CurrentIndex { get => _currentIndex; set => this.Set(this.PropertyChangedHandler, ref _currentIndex, value); }

        /// <summary>
        /// Gets the FrameCount.
        /// </summary>
        public int FrameCount => this.BarValuesInterpolatedModels.Any() ? this.BarValuesInterpolatedModels.First().Values.Count : 0;

        /// <summary>
        /// Gets or sets the FramePerSecond.
        /// </summary>
        public double FramePerSecond { get => _framePerSecond; set => this.Set(this.PropertyChangedHandler, ref _framePerSecond, value); }

        /// <summary>
        /// Gets or sets the Height.
        /// </summary>
        public double Height { get => _height; set => this.Set(this.PropertyChangedHandler, ref _height, value); }

        /// <summary>
        /// Gets or sets the Width.
        /// </summary>
        public double Width { get => _width; set => this.Set(this.PropertyChangedHandler, ref _width, value); }

        #endregion Properties
    }
}