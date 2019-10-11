using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weather.SqlContainers
{
    class BestHolidayPlace
    {
        public string Name { get; set; }
        public MonthName Month { get; set; }
        public double MonthAvgTemp { get; set; }
        public double MonthAvgPrecipitation { get; set; }

        public enum MonthName : int
        {
            January, February, March, April, May, June, July, August, September, October, November, December
        }
    }
}
