using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weather.Models;

namespace Weather.Export
{
    class DataWeatherExporter : WeatherExporter
    {
        //public IEnumerable<Data> Datas { get; private set; }

        public DataWeatherExporter(IEnumerable<Data> dataCollection)
        {
            //Datas = dataCollection;
            Collection = dataCollection;
        }

        public override void DbExport()
        {
            using (WeatherDbContext)
            {
                //WeatherDbContext.Datas.AddRange(Datas);
                WeatherDbContext.Datas.AddRange(Collection as IEnumerable<Data>);
                base.DbExport();
            }
        }
    }
}
