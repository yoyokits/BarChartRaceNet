namespace BarChartRaceNetTestApp.Tests
{
    using BarChartRaceNet.Models;
    using BarChartRaceNet.ViewModels;
    using BarChartRaceNet.Views;
    using BarChartRaceNetTestApp.TestHelpers;
    using MahApps.Metro.Controls;
    using System.Windows;

    /// <summary>
    /// Defines the <see cref="DatasetViewTest" />.
    /// </summary>
    public class DatasetViewTest : TestBase
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DatasetViewTest"/> class.
        /// </summary>
        public DatasetViewTest() : base(nameof(DatasetViewTest))
        {
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// The Test.
        /// </summary>
        /// <param name="testWindow">The testWindow<see cref="Window"/>.</param>
        protected override void Test(MetroWindow testWindow)
        {
            var globalData = new GlobalData { MainWindow = testWindow };
            var viewModel = new DatasetViewModel(globalData) { CsvFilePath = TestData.CountryTestCsv };
            var view = new DatasetView { DataContext = viewModel };
            WindowFactory.CreateAndShow(view, testWindow);
        }

        #endregion Methods
    }
}