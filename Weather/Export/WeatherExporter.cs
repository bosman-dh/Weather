using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weather.EF;

namespace Weather.Export
{
    abstract class WeatherExporter
    {
        public WeatherDB WeatherDbContext { get; private set; }
        public IEnumerable<object> Collection { get; protected set; }   //possible to set in inherited class

        public WeatherExporter()
        {
            WeatherDbContext = new WeatherDB();
        }

        public virtual void DbExport()
        {
            WeatherDbContext.SaveChanges();
        }
    }
}
