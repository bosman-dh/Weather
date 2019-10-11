using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weather.EF;

namespace Weather.Menu
{
    class ElementImportDb : IElement
    {
        public char Key => '1';
        public string Name => "Import Database";
        public string InfoDuringExe => "Database is importing...";
        public bool IsContinueElement => true;

        public void Exec()
        {
            using (var context = new WeatherDB())
            {
                context.Database.Initialize(false);
            }
        }
    }
}
