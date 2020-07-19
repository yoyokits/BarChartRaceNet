namespace BarChartRaceNet.Models
{
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="BarValuesModel" />.
    /// </summary>
    public class BarValuesModel
    {
        #region Properties

        /// <summary>
        /// Gets the InterpolatedRanks.
        /// </summary>
        public IList<double> InterpolatedRanks { get; } = new List<double>();

        /// <summary>
        /// Gets or sets the Name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets the Ranks.
        /// </summary>
        public IList<double> Ranks { get; } = new List<double>();

        /// <summary>
        /// Gets the Values.
        /// </summary>
        public IList<double> Values { get; } = new List<double>();

        #endregion Properties

        #region Methods

        /// <summary>
        /// The Clear.
        /// </summary>
        public void Clear()
        {
            this.InterpolatedRanks.Clear();
            this.Ranks.Clear();
            this.Values.Clear();
        }

        /// <summary>
        /// The ToString.
        /// </summary>
        /// <returns>The <see cref="string"/>.</returns>
        public override string ToString()
        {
            var message = $"BarValuesModel {Name}:Count:{this.Values.Count}";
            return message;
        }

        #endregion Methods
    }
}