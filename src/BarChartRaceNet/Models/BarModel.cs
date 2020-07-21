namespace BarChartRaceNet.Models
{
    using BarChartRaceNet.Common;
    using BarChartRaceNet.Extensions;
    using System.ComponentModel;
    using System.Windows;
    using System.Windows.Media;

    /// <summary>
    /// Defines the <see cref="BarModel" />.
    /// </summary>
    public class BarModel : NotifyPropertyChanged
    {
        #region Fields

        private SolidColorBrush _brush;

        private Color _color;

        private string _icon;

        private double _index;

        private bool _isSuspended;

        private GridLength _lowerGridLength;

        private string _name;

        private GridLength _upperGridLength;

        private double _value;

        private string _valueText = "0";

        private RangeDouble _visibleRange;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="BarModel"/> class.
        /// </summary>
        public BarModel()
        {
            this.Color = ColorPaletteProvider.GetNext();
            this.PropertyChanged += this.OnPropertyChanged;
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets or sets the Brush.
        /// </summary>
        public SolidColorBrush Brush { get => _brush; set => this.Set(this.PropertyChangedHandler, ref _brush, value); }

        /// <summary>
        /// Gets or sets the Color.
        /// </summary>
        public Color Color
        {
            get => _color;
            set
            {
                if (!this.Set(this.PropertyChangedHandler, ref _color, value))
                {
                    return;
                }

                this.Brush = new SolidColorBrush(this.Color);
            }
        }

        /// <summary>
        /// Gets or sets the Icon.
        /// </summary>
        public string Icon { get => _icon; set => this.Set(this.PropertyChangedHandler, ref _icon, value); }

        /// <summary>
        /// Gets or sets the Index.
        /// </summary>
        public double Index { get => _index; set => this.Set(this.PropertyChangedHandler, ref _index, value); }

        /// <summary>
        /// Gets or sets a value indicating whether IsSuspended.
        /// </summary>
        public bool IsSuspended { get => _isSuspended; set => this.Set(this.PropertyChangedHandler, ref _isSuspended, value); }

        /// <summary>
        /// Gets the LowerGridLength.
        /// </summary>
        public GridLength LowerGridLength { get => _lowerGridLength; private set => this.Set(this.PropertyChangedHandler, ref _lowerGridLength, value); }

        /// <summary>
        /// Gets or sets the Name.
        /// </summary>
        public string Name { get => _name; set => this.Set(this.PropertyChangedHandler, ref _name, value); }

        /// <summary>
        /// Gets the UpperGridLength.
        /// </summary>
        public GridLength UpperGridLength { get => _upperGridLength; private set => this.Set(this.PropertyChangedHandler, ref _upperGridLength, value); }

        /// <summary>
        /// Gets or sets the Value.
        /// </summary>
        public double Value
        {
            get => _value;
            set
            {
                if (!this.Set(this.PropertyChangedHandler, ref _value, value))
                {
                    return;
                }

                this.ValueText = $"{this.Value:0.##}";
            }
        }

        /// <summary>
        /// Gets or sets the ValueText.
        /// </summary>
        public string ValueText { get => _valueText; set => this.Set(this.PropertyChangedHandler, ref _valueText, value); }

        /// <summary>
        /// Gets or sets the VisibleRange.
        /// </summary>
        public RangeDouble VisibleRange { get => _visibleRange; set => this.Set(this.PropertyChangedHandler, ref _visibleRange, value); }

        #endregion Properties

        #region Methods

        /// <summary>
        /// The ToString.
        /// </summary>
        /// <returns>The <see cref="string"/>.</returns>
        public override string ToString()
        {
            var message = $"Bar Model {this.Index:0.##}:Name{this.Name};{this.Value};";
            return message;
        }

        /// <summary>
        /// The CalculateGridLength.
        /// </summary>
        private void CalculateGridLength()
        {
            if (this.VisibleRange.IsEmpty() || this.IsSuspended)
            {
                return;
            }

            var value = this.Value > this.VisibleRange.Start ? this.Value : this.VisibleRange.Start;
            if (value > this.VisibleRange.End)
            {
                value = this.VisibleRange.End;
            }

            var lower = value - this.VisibleRange.Start;
            var upper = this.VisibleRange.End - value;
            this.LowerGridLength = new GridLength(lower, GridUnitType.Star);
            this.UpperGridLength = new GridLength(upper, GridUnitType.Star);
        }

        /// <summary>
        /// The OnPropertyChanged.
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/>.</param>
        /// <param name="e">The e<see cref="PropertyChangedEventArgs"/>.</param>
        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(this.Value):
                    this.CalculateGridLength();
                    break;

                case nameof(this.VisibleRange):
                    this.CalculateGridLength();
                    break;

                case nameof(this.IsSuspended):
                    if (!this.IsSuspended)
                    {
                        this.CalculateGridLength();
                    }

                    break;

                default:
                    break;
            }
        }

        #endregion Methods
    }
}