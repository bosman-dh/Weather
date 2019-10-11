using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weather.Menu
{
    interface IElement
    {
        char Key { get; }
        string Name { get; }
        string InfoDuringExe { get; }
        bool IsContinueElement { get; }

        void Exec();
    }
}
