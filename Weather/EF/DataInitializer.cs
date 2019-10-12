using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weather.Export;
using Weather.Import;

namespace Weather.EF
{
    ////class DataInitializer : CreateDatabaseIfNotExists<WeatherDB>
    class DataInitializer : DropCreateDatabaseIfModelChanges<WeatherDB>
    //class DataInitializer : DropCreateDatabaseAlways<WeatherDB>
    {
        protected override void Seed(WeatherDB context)
        {
            #region Import fields
            StationFileImporter stationFileImporter = new StationFileImporter(@"..\..\..\import.csv");
            DataFileImporter dataFileImporter = new DataFileImporter(@"..\..\..\import.csv");
            List<IDuplicateDeleter> duplicateDeleters = new List<IDuplicateDeleter>() { stationFileImporter, dataFileImporter };
            List<ICollectionImporter> collectionImporters = new List<ICollectionImporter>() { stationFileImporter, dataFileImporter };
            #endregion

            #region Export fields

            DataWeatherExporter dataWeatherExporter = new DataWeatherExporter(dataFileImporter.Datas);
            StationWeatherExporter stationWeatherExporter = new StationWeatherExporter(stationFileImporter.Stations);
            List<WeatherExporter> weatherExporters = new List<WeatherExporter> { stationWeatherExporter, dataWeatherExporter };

            #endregion

            #region Import
            //imports from file to collections
            foreach (var item in collectionImporters)
            {
                item.CollectionImport();
            }

            //deletes duplicate from collections
            foreach (var item in duplicateDeleters)
            {
                item.DeleteDuplicates();
            }
            #endregion

            #region Export
            //adds collections to Db
            foreach (var item in weatherExporters)
            {
                item.DbExport();
            }
            #endregion


            base.Seed(context);
        }
    }
}
