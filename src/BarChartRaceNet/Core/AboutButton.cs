namespace BarChartRaceNet.Core
{
    using BarChartRaceNet.Models;
    using BarChartRaceNet.Resources;

    /// <summary>
    /// Defines the <see cref="AboutButton" />.
    /// </summary>
    public class AboutButton : AddInButton
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="AboutButton"/> class.
        /// </summary>
        /// <param name="globalData">The globalData<see cref="GlobalData"/>.</param>
        internal AboutButton(GlobalData globalData) : base("About", Icons.About)
        {
            this.GlobalData = globalData;
            this.ToolTip = "Show About Cekli Bar Chart Race";
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets the GlobalData.
        /// </summary>
        public GlobalData GlobalData { get; }

        #endregion Properties

        #region Methods

        /// <summary>
        /// The Execute.
        /// </summary>
        /// <param name="obj">The obj<see cref="object"/>.</param>
        protected override void Execute(object obj)
        {
        }

        #endregion Methods
    }
}