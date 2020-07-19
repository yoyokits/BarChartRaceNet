namespace BarChartRaceNet.Helpers
{
    using BarChartRaceNet.Common;
    using CsvHelper;
    using CsvHelper.Configuration;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Linq;

    /// <summary>
    /// Defines the <see cref="CsvHelper" />.
    /// </summary>
    public static class CsvFileHelper
    {
        #region Properties

        /// <summary>
        /// Gets the CsvConfiguration.
        /// </summary>
        private static CsvConfiguration CsvConfiguration { get; } = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            HasHeaderRecord = true,
            Delimiter = ","
        };

        /// <summary>
        /// Gets the CsvConfiguration.
        /// </summary>
        private static CsvConfiguration CsvConfigurationNoHeader { get; } = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            HasHeaderRecord = false,
            Delimiter = ","
        };

        #endregion Properties

        #region Methods

        /// <summary>
        /// The Load.
        /// </summary>
        /// <param name="csvFilePath">The csvFilePath<see cref="string"/>.</param>
        /// <returns>The <see cref="object"/>.</returns>
        public static string[][] Load(string csvFilePath)
        {
            if (!File.Exists(csvFilePath))
            {
                return null;
            }

            try
            {
                string[][] array2DString = null;
                using (var reader = new StreamReader(csvFilePath))
                using (var csv = new CsvReader(reader, CsvConfigurationNoHeader))
                {
                    var records = csv.GetRecords<dynamic>();
                    array2DString = ConvertToList(records.ToList());
                }

                using (var reader = new StreamReader(csvFilePath))
                using (var csv = new CsvReader(reader, CsvConfiguration))
                {
                    var records = csv.GetRecords<dynamic>();
                    var array2DCorrectHeader = ConvertToList(records.ToList());
                    var headerRecord = array2DCorrectHeader.First();
                    for (var i = 0; i < headerRecord.Length; i++)
                    {
                        array2DString[0][i] = headerRecord[i];
                    }

                    return array2DString;
                }
            }
            catch (Exception e)
            {
                Logger.Info($"CsvFileHelper Exception: {e.Message}");
            }

            return null;
        }

        /// <summary>
        /// The ConvertToList.
        /// </summary>
        /// <param name="records">The records<see cref="IList{dynamic}"/>.</param>
        /// <returns>The <see cref="string[][]"/>.</returns>
        internal static string[][] ConvertToList(IList<dynamic> records)
        {
            var list2D = new List<string[]>();
            for (var i = 0; i < records.Count; i++)
            {
                dynamic record = (dynamic)records[i];
                var list = new List<string>();
                var recordDict = record as IDictionary<string, object>;
                foreach (var item in recordDict)
                {
                    var row = i == 0 ? item.Key : item.Value.ToString();
                    list.Add(row);
                }

                list2D.Add(list.ToArray());
            }

            return list2D.ToArray();
        }

        #endregion Methods
    }
}