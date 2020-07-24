namespace BarChartRaceNet.Core
{
    using BarChartRaceNet.Resources;
    using System;

    /// <summary>
    /// Defines the <see cref="ExportChartButton" />.
    /// </summary>
    public class ExportChartButton : AddInButton
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ExportChartButton"/> class.
        /// </summary>
        internal ExportChartButton() : base("Export Chart", Icons.ExportChart)
        {
            this.ToolTip = "Export chart to video";
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets or sets the ExportChartAction.
        /// </summary>
        internal Action ExportChartAction { get; set; }

        #endregion Properties

        #region Methods

        /// <summary>
        /// The Execute.
        /// </summary>
        /// <param name="obj">The obj<see cref="object"/>.</param>
        protected override void Execute(object obj)
        {
            if (this.ExportChartAction == null)
            {
                return;
            }

            this.ExportChartAction();
        }

        #endregion Methods
    }
}