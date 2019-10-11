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
            //List<FileImporter> lfi = new List<FileImporter>() { sfi, dfi };   //cannot use List<FileImporter> to iteration, because FileImport return ICollection<string>, but StationFileImporter:FileImport and DataFileImporter:FileImport return void, so override is impossible; Other side ICollection is needed due to yield return 
            List<IDuplicateDeleter> duplicateDeleters = new List<IDuplicateDeleter>() { stationFileImporter, dataFileImporter };
            List<ICollectionImporter> collectionImporters = new List<ICollectionImporter>() { stationFileImporter, dataFileImporter };
            #endregion

            #region Export fields

            DataWeatherExporter dataWeatherExporter = new DataWeatherExporter(dataFileImporter.Datas);
            StationWeatherExporter stationWeatherExporter = new StationWeatherExporter(stationFileImporter.Stations);
            List<WeatherExporter> weatherExporters = new List<WeatherExporter> { stationWeatherExporter, dataWeatherExporter };

            #endregion

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

            //swe.DbExport();
            //dwe.DbExport();

            //adds collections to Db
            foreach (var item in weatherExporters)
            {
                item.DbExport();
            }

            base.Seed(context);
        }
    }
}
