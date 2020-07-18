namespace BarChartRaceNet.Core
{
    using BarChartRaceNet.Common;
    using BarChartRaceNet.Extensions;
    using System.Windows.Input;
    using System.Windows.Media;

    /// <summary>
    /// Defines the <see cref="AddInButton" />.
    /// </summary>
    public abstract class AddInButton : NotifyPropertyChanged
    {
        #region Fields

        private Geometry _icon;

        private bool _isEnabled = true;

        private bool _isVisible;

        private string _toolTip;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="AddInButton"/> class.
        /// </summary>
        /// <param name="name">The name<see cref="string"/>.</param>
        /// <param name="icon">The icon<see cref="Geometry"/>.</param>
        protected AddInButton(string name, Geometry icon)
        {
            if (name == null)
            {
                name = this.GetType().Name;
            }

            this.Name = name;
            this.Icon = icon;
            this.Command = new RelayCommand(this.OnExecute, $"{this.Name} add in is executed");
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets the Command.
        /// </summary>
        public ICommand Command { get; }

        /// <summary>
        /// Gets or sets the Icon.
        /// </summary>
        public Geometry Icon { get => _icon; set => this.Set(this.PropertyChangedHandler, ref _icon, value); }

        /// <summary>
        /// Gets or sets a value indicating whether IsEnabled.
        /// </summary>
        public bool IsEnabled { get => _isEnabled; set => this.Set(this.PropertyChangedHandler, ref _isEnabled, value); }

        /// <summary>
        /// Gets or sets a value indicating whether IsVisible.
        /// </summary>
        public bool IsVisible { get => _isVisible; set => this.Set(this.PropertyChangedHandler, ref _isVisible, value); }

        /// <summary>
        /// Gets or sets the Name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the ToolTip.
        /// </summary>
        public string ToolTip { get => _toolTip; set => this.Set(this.PropertyChangedHandler, ref _toolTip, value); }

        #endregion Properties

        #region Methods

        /// <summary>
        /// The Execute.
        /// </summary>
        /// <param name="obj">The obj<see cref="object"/>.</param>
        protected abstract void Execute(object obj);

        /// <summary>
        /// The OnExecute.
        /// </summary>
        /// <param name="obj">The obj<see cref="object"/>.</param>
        private void OnExecute(object obj)
        {
            this.Execute(obj);
        }

        #endregion Methods
    }
}