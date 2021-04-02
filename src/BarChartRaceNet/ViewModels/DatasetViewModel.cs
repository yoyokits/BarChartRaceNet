namespace BarChartRaceNet.ViewModels
{
    using BarChartRaceNet.Common;
    using BarChartRaceNet.Extensions;
    using BarChartRaceNet.Helpers;
    using BarChartRaceNet.Models;
    using System;
    using System.IO;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines the <see cref="DatasetViewModel" />.
    /// </summary>
    public class DatasetViewModel : NotifyPropertyChanged
    {
        #region Fields

        private string _csvFilePath;

        private object _itemsSource;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DatasetViewModel"/> class.
        /// </summary>
        /// <param name="globalData">The globalData<see cref="GlobalData"/>.</param>
        internal DatasetViewModel(GlobalData globalData)
        {
            this.GlobalData = globalData;
            this.PropertyChanged += this.OnPropertyChanged;
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets or sets the CsvFilePath.
        /// </summary>
        public string CsvFilePath { get => _csvFilePath; set => this.Set(this.PropertyChangedHandler, ref _csvFilePath, value); }

        /// <summary>
        /// Gets the DatasetStringArray.
        /// </summary>
        public string[][] DatasetStringArray { get; private set; }

        /// <summary>
        /// Gets the GlobalData.
        /// </summary>
        public GlobalData GlobalData { get; }

        /// <summary>
        /// Gets or sets the ItemsSource.
        /// </summary>
        public object ItemsSource { get => _itemsSource; set => this.Set(this.PropertyChangedHandler, ref _itemsSource, value); }

        #endregion Properties

        #region Methods

        /// <summary>
        /// The ParseStringArray.
        /// </summary>
        internal void ParseStringArray()
        {
            if (this.DatasetStringArray == null)
            {
                return;
            }

            var barValuesModels = this.DatasetStringArray.DatasetToBarValuesModels();
            barValuesModels.AdjustArray();
            UIThreadHelper.InvokeAsync(() => this.GlobalData.BarValuesModels = barValuesModels);
        }

        /// <summary>
        /// The LoadFile.
        /// </summary>
        private void LoadFile()
        {
            if (File.Exists(this.CsvFilePath))
            {
                this.GlobalData.SettingsModel.LastOpenedCsvFile = this.CsvFilePath;
                this.GlobalData.SettingsModel.InitialDirectory = Path.GetDirectoryName(this.CsvFilePath);
                try
                {
                    this.DatasetStringArray = CsvFileHelper.Load(this.CsvFilePath);
                    if (this.DatasetStringArray != null)
                    {
                        this.ParseStringArray();
                    }

                    this.ItemsSource = this.DatasetStringArray;
                }
                catch (Exception exception)
                {
                    Logger.Info($"CsvFileHelper Exception: {exception.Message}");
                    var fileName = Path.GetFileName(this.CsvFilePath);
                    var message = $"Error opening {fileName}: {exception.Message}";
                    this.GlobalData.ShowMessageAsync("Loading File Failed", message);
                }
            }
            else
            {
                Logger.Error($"Error: Cannot load {this.CsvFilePath}, the file doesn't exist");
            }
        }

        /// <summary>
        /// The OnPropertyChanged.
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/>.</param>
        /// <param name="e">The e<see cref="System.ComponentModel.PropertyChangedEventArgs"/>.</param>
        private void OnPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(this.CsvFilePath):
                    Task.Run(() => this.LoadFile());
                    break;

                default:
                    break;
            }
        }

        #endregion Methods
    }
}