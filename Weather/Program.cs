using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weather.EF;
using Weather.Export;
using Weather.Import;
using Weather.Menu;

namespace Weather
{
    class Program
    {
        static void Main(string[] args)
        {
            SelectionMenu homeMenu = new SelectionMenu(new List<IElement>() { new ElementImportDb(), new ElementListStations(), new ElementBestHolidayPlace(), new ElementExit() });

            homeMenu.Show();
        }
    }
}
