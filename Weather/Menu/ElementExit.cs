using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weather.Menu
{
    class ElementExit : IElement
    {
        public char Key => 'q';
        public string Name => "Exit";
        public string InfoDuringExe => null;
        public bool IsContinueElement => false;

        public void Exec()
        {

        }
    }
}
