using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weather.Models;

namespace Weather.Export
{
    class StationWeatherExporter : WeatherExporter
    {
        //public IEnumerable<Station> Stations { get; private set; }

        public StationWeatherExporter(IEnumerable<Station> stationCollection)
        {
            //Stations = stationCollection;
            Collection = stationCollection;
        }

        public override void DbExport()
        {
            using (WeatherDbContext)
            {
                //WeatherDbContext.Stations.AddRange(Stations);
                WeatherDbContext.Stations.AddRange(Collection as IEnumerable<Station>);
                base.DbExport();
            }
        }
    }
}
