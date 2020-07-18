namespace BarChartRaceNet.Core
{
    using BarChartRaceNet.Resources;
    using Ookii.Dialogs.Wpf;
    using System;

    /// <summary>
    /// Defines the <see cref="LoadButton" />.
    /// </summary>
    public class LoadButton : AddInButton
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="LoadButton"/> class.
        /// </summary>
        internal LoadButton() : base("Load", Icons.Load)
        {
            this.ToolTip = "Load csv file";
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets or sets the LoadAction.
        /// </summary>
        internal Action<string> LoadAction { get; set; }

        #endregion Properties

        #region Methods

        /// <summary>
        /// The Execute.
        /// </summary>
        /// <param name="obj">The obj<see cref="object"/>.</param>
        protected override void Execute(object obj)
        {
            if (this.LoadAction == null)
            {
                return;
            }

            var dialog = new VistaOpenFileDialog
            {
                Filter = "Csv files (*.csv)|*.csv"
            };
            if ((bool)dialog.ShowDialog())
            {
                this.LoadAction(dialog.FileName);
            }
        }

        #endregion Methods
    }
}