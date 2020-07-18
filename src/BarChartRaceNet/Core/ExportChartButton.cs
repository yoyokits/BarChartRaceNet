namespace BarChartRaceNet.Core
{
    using BarChartRaceNet.Resources;
    using Ookii.Dialogs.Wpf;
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
        internal Action<string> ExportChartAction { get; set; }

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

            var dialog = new VistaSaveFileDialog
            {
                Filter = "MPEG4 files (*.mp4)|*.mp4"
            };
            if ((bool)dialog.ShowDialog())
            {
                this.ExportChartAction(dialog.FileName);
            }
        }

        #endregion Methods
    }
}