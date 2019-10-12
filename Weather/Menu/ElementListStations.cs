using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weather.EF;

namespace Weather.Menu
{
    class ElementListStations : IElement
    {
        public char Key => '2';
        public string Name => "Show all weather stations";
        public string InfoDuringExe => null;
        public bool IsContinueElement => true;

        public void Exec()
        {
            using (WeatherDB weatherDB = new WeatherDB())
            {
                Console.WriteLine("Stations:");
                Console.WriteLine("---------");
                weatherDB.Stations.Select(x => x.Name).OrderBy(x => x).ToList().ForEach(x => Console.WriteLine(x));

                Console.WriteLine("");
                Console.WriteLine("Date range:");
                Console.WriteLine("-----------");
                Console.WriteLine($"{weatherDB.Datas.Min(x => x.Date)} -  {weatherDB.Datas.Max(x => x.Date)}");
            }

            Console.WriteLine("");
            Console.WriteLine("-Press any key-");
            Console.ReadKey();
        }
    }
}
