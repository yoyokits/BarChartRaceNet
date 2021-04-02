namespace BarChartRaceNetTestApp.Tests
{
    using BarChartRaceNet.Common;
    using BarChartRaceNet.Extensions;
    using BarChartRaceNet.Models;
    using BarChartRaceNet.ViewModels;
    using BarChartRaceNet.Views;
    using BarChartRaceNetTestApp.TestHelpers;
    using MahApps.Metro.Controls;
    using System.Windows;

    /// <summary>
    /// Defines the <see cref="BarChartViewTest" />.
    /// </summary>
    public class BarChartViewTest : TestBase
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="BarChartViewTest"/> class.
        /// </summary>
        public BarChartViewTest() : base(nameof(BarChartViewTest))
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
            var viewModel = new BarChartViewModel(globalData);
            var view = new BarChartView { DataContext = viewModel };
            this.GenerateDummyData(viewModel);
            WindowFactory.CreateAndShow(view, testWindow);
        }

        /// <summary>
        /// The GenerateDummyData.
        /// </summary>
        /// <param name="viewModel">The viewModel<see cref="BarChartViewModel"/>.</param>
        private void GenerateDummyData(BarChartViewModel viewModel)
        {
            viewModel.Title = "Bar Chart Test App";
            viewModel.Subtitle = "Show Bar Chart with Dummy Data";
            var n = 5;
            var range = new RangeDouble(0, 1000);
            for (var i = 0; i < n; i++)
            {
                var model = new BarModel { Index = i, Name = $"Bar Test {i}", Value = (n - i) * (range.Length() / n), VisibleRange = range };
                viewModel.BarModels.Add(model);
            }
        }

        #endregion Methods
    }
}