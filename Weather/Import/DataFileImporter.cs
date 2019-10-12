using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weather.Models;

namespace Weather.Import
{
    class DataFileImporter : FileImporter, IDuplicateDeleter, ICollectionImporter
    {
        public DataFileImporter(string pathFileImport) : base(pathFileImport)
        {

        }

        public List<Data> Datas { get; private set; } = new List<Data>();

        public void CollectionImport()
        {
            foreach (var item in base.FileImport())
            {
                var columnList = item.Replace("\"", "").Split(',').ToList();
                var stationId_DataCandidate = columnList.ElementAt(0) + columnList.ElementAt(6); //Station.Id candidate + WeatherData.Data

                //if (Datas.All(x => x.StationId + x.Date != stationId_DataCandidate))  //to long operation

                Data newData = new Data();

                newData.Date = DateTime.Parse(columnList.ElementAt(6));
                if (!String.IsNullOrEmpty(columnList.ElementAt(7).ToString()))
                    newData.Precipitation = float.Parse(columnList.ElementAt(7), CultureInfo.InvariantCulture);
                if (!String.IsNullOrEmpty(columnList.ElementAt(8).ToString()))
                    newData.Snow = float.Parse(columnList.ElementAt(8), CultureInfo.InvariantCulture);
                if (!String.IsNullOrEmpty(columnList.ElementAt(9).ToString()))
                    newData.Tavg = float.Parse(columnList.ElementAt(9), CultureInfo.InvariantCulture);
                if (!String.IsNullOrEmpty(columnList.ElementAt(10).ToString()))
                    newData.Tmax = float.Parse(columnList.ElementAt(10), CultureInfo.InvariantCulture);
                if (!String.IsNullOrEmpty(columnList.ElementAt(11).ToString()))
                    newData.Tmin = float.Parse(columnList.ElementAt(11), CultureInfo.InvariantCulture);
                newData.StationId = columnList.ElementAt(0);

                Datas.Add(newData);

                #region with 0 instead of null
                //Datas.Add(new Data()
                //{
                //Date = DateTime.Parse(columnList.ElementAt(6)),
                //Precipitation = String.IsNullOrEmpty(columnList.ElementAt(7).ToString()) ? 0 : float.Parse(columnList.ElementAt(7), CultureInfo.InvariantCulture),
                //Snow = String.IsNullOrEmpty(columnList.ElementAt(8).ToString()) ? 0 : float.Parse(columnList.ElementAt(8), CultureInfo.InvariantCulture),
                //Tavg = String.IsNullOrEmpty(columnList.ElementAt(9).ToString()) ? 0 : float.Parse(columnList.ElementAt(9), CultureInfo.InvariantCulture),
                //Tmax = String.IsNullOrEmpty(columnList.ElementAt(10).ToString()) ? 0 : float.Parse(columnList.ElementAt(10), CultureInfo.InvariantCulture),
                //Tmin = String.IsNullOrEmpty(columnList.ElementAt(11).ToString()) ? 0 : float.Parse(columnList.ElementAt(11), CultureInfo.InvariantCulture),
                //StationId = columnList.ElementAt(0)
                //});
                #endregion
            }
        }

        public void DeleteDuplicates()
        {
            Datas = Datas.Distinct().ToList();
        }
    }
}
