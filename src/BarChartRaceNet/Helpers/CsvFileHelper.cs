namespace BarChartRaceNet.Helpers
{
    using BarChartRaceNet.Common;
    using CsvHelper;
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
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    var records = csv.GetRecords<dynamic>();
                    array2DString = ConvertToList(records.ToList());
                }

                return array2DString;
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
            var stringList2D = new List<string[]>();
            var headers = new List<string>();
            for (var i = 0; i < records.Count; i++)
            {
                var record = (dynamic)records[i];
                var list = new List<string>();
                var recordDict = record as IDictionary<string, object>;
                foreach (var item in recordDict)
                {
                    if (i == 0)
                    {
                        headers.Add(item.Key);
                    }

                    list.Add(item.Value.ToString());
                }

                if (i == 0)
                {
                    stringList2D.Add(headers.ToArray());
                }

                stringList2D.Add(list.ToArray());
            }

            return stringList2D.ToArray();
        }

        #endregion Methods
    }
}