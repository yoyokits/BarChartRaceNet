namespace BarChartRaceNet.Test.TestHelpers
{
    using System.IO;

    /// <summary>
    /// Defines the <see cref="TestData" />.
    /// </summary>
    public static class TestData
    {
        #region Constructors

        /// <summary>
        /// Initializes static members of the <see cref="TestData"/> class.
        /// </summary>
        static TestData()
        {
            CountryTestCsv = Path.Combine(TestDataFolder, "CountryTest.csv");
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets the CountryTestCsv.
        /// </summary>
        public static string CountryTestCsv { get; }

        /// <summary>
        /// Gets the TestDataFolder.
        /// </summary>
        public static string TestDataFolder { get; } = "TestData";

        #endregion Properties
    }
}