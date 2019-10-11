using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weather.Models;

namespace Weather.Import
{
    class StationFileImporter : FileImporter, IDuplicateDeleter, ICollectionImporter
    {
        public StationFileImporter(string pathFileImport) : base(pathFileImport)
        {

        }

        public List<Station> Stations { get; private set; } = new List<Station>();

        public void DeleteDuplicates()
        {
            Stations = Stations.Distinct().ToList();
        }

        public void CollectionImport()
        {

            foreach (var item in base.FileImport())
            {
                var columnList = item.Replace("\"", "").Split(',').ToList();
                var stationIdCandidate = columnList.ElementAt(0);   //Station.Id candidate

                if (Stations.All(x => x.Id != stationIdCandidate))
                    //^check if this primary key don't exists in stations collection
                    Stations.Add(new Station()
                    {
                        Id = columnList.ElementAt(0),
                        Name = columnList.ElementAt(1),
                        Latitude = float.Parse(columnList.ElementAt(3), CultureInfo.InvariantCulture),
                        Longitude = float.Parse(columnList.ElementAt(4), CultureInfo.InvariantCulture),
                        Elevation = float.Parse(columnList.ElementAt(5), CultureInfo.InvariantCulture)
                    });
            }
        }
    }
}
