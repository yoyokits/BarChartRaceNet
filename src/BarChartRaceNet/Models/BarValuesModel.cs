namespace BarChartRaceNet.Models
{
    /// <summary>
    /// Defines the <see cref="BarValuesModel" />.
    /// </summary>
    public class BarValuesModel
    {
        #region Properties

        /// <summary>
        /// Gets or sets the Name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the Ranks.
        /// </summary>
        public double[] Ranks { get; set; }

        /// <summary>
        /// Gets or sets the RanksInterpolated.
        /// </summary>
        public double[] RanksInterpolated { get; set; }

        /// <summary>
        /// Gets or sets the RankSteps.
        /// </summary>
        public double[] RankSteps { get; set; }

        /// <summary>
        /// Gets or sets the Times.
        /// </summary>
        public string[] Times { get; set; }

        /// <summary>
        /// Gets or sets the Values.
        /// </summary>
        public double[] Values { get; set; }

        /// <summary>
        /// Gets or sets the ValuesInterpolated.
        /// </summary>
        public double[] ValuesInterpolated { get; set; }

        #endregion Properties

        #region Methods

        /// <summary>
        /// The ToString.
        /// </summary>
        /// <returns>The <see cref="string"/>.</returns>
        public override string ToString()
        {
            var message = $"BarValuesModel {Name}:Count:{this.Values.Length}";
            return message;
        }

        #endregion Methods
    }
}