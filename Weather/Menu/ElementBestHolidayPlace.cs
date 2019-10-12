﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weather.EF;
using Weather.SqlContainers;

namespace Weather.Menu
{
    class ElementBestHolidayPlace : IElement
    {
        public char Key => '3';
        public string Name => "Find best holiday place";
        public string InfoDuringExe => null;
        public bool IsContinueElement => true;

        public void Exec()
        {
            BestHolidayPlace relaxingHoliday;
            BestHolidayPlace activeHoliday;

            using (WeatherDB weatherDB = new WeatherDB())
            {
                relaxingHoliday = weatherDB.Database.SqlQuery<BestHolidayPlace>("" +
                    "SELECT S.Name, MONTH(D.Date) AS Month, AVG(D.Tavg) AS MonthAvgTemp, AVG(D.Precipitation) AS MonthAvgPrecipitation " +
                    "FROM Stations AS S " +
                    "JOIN Data AS D " +
                    "ON S.Id = D.StationId " +
                    "GROUP BY S.Name, MONTH(D.Date) " +
                    "HAVING AVG(D.Precipitation) < 2 " +
                    "ORDER BY MonthAvgTemp DESC, MonthAvgPrecipitation "
                    ).FirstOrDefault();

                activeHoliday = weatherDB.Database.SqlQuery<BestHolidayPlace>("" +
                    "SELECT S.Name, MONTH(D.Date) AS Month, AVG(D.Tavg) AS MonthAvgTemp, AVG(D.Precipitation) AS MonthAvgPrecipitation " +
                    "FROM Stations AS S " +
                    "JOIN Data AS D " +
                    "ON S.Id = D.StationId " +
                    "GROUP BY S.Name, MONTH(D.Date) " +
                    "HAVING AVG(D.Tavg) > 10 " +
                    "ORDER BY MonthAvgPrecipitation, MonthAvgTemp DESC"
                    ).FirstOrDefault();
            }

            if (activeHoliday!=null)
            {
                Console.WriteLine($"Best place for active holiday is area weather station {activeHoliday.Name} in {activeHoliday.Month}");
                Console.WriteLine($"    with the smallest avarage daily precipitation {Math.Round(activeHoliday.MonthAvgPrecipitation, 2)}mm");
                Console.WriteLine($"    and >10C average daily temperature ({Math.Round(activeHoliday.MonthAvgTemp, 2)}C)");
                Console.WriteLine("");
            }
            if (relaxingHoliday!=null)
            {
                Console.WriteLine($"Best place for relaxing holiday is area weather station {relaxingHoliday.Name} in {relaxingHoliday.Month}");
                Console.WriteLine($"    with the biggest average daily temperature {Math.Round(relaxingHoliday.MonthAvgTemp, 2)}C");
                Console.WriteLine($"    and <2mm average daily precipitation ({Math.Round(relaxingHoliday.MonthAvgPrecipitation, 2)}mm)");
            }
            Console.WriteLine("");
            Console.WriteLine("-Press any key-");
            Console.ReadKey();

        }
    }
}
