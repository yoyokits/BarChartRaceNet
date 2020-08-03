namespace BarChartRaceNet.Core
{
    using BarChartRaceNet.Resources;
    using System;

    /// <summary>
    /// Defines the <see cref="CancelRenderingButton" />.
    /// </summary>
    public class CancelRenderingButton : AddInButton
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="CancelRenderingButton"/> class.
        /// </summary>
        internal CancelRenderingButton() : base("Cancel Rendering", Icons.Cancel)
        {
            this.ToolTip = "Cancel rendering chart to video";
            this.IsEnabled = false;
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets or sets the CancelRenderingAction.
        /// </summary>
        internal Action CancelRenderingAction { get; set; }

        #endregion Properties

        #region Methods

        /// <summary>
        /// The Execute.
        /// </summary>
        /// <param name="obj">The obj<see cref="object"/>.</param>
        protected override void Execute(object obj)
        {
            if (this.CancelRenderingAction == null)
            {
                return;
            }

            this.CancelRenderingAction();
        }

        #endregion Methods
    }
}