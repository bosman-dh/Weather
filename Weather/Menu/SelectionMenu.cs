using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weather.Menu
{
    class SelectionMenu
    {
        private bool continueLoop = true;

        public IEnumerable<IElement> Elements { get; }
        private string infoBadNumberChoice { get; } = "This option doesn't exist in the menu - press any key";

        public SelectionMenu(IEnumerable<IElement> elements)
        {
            Elements = elements;
        }

        public void Show()
        {
            while (continueLoop)
            {
                //show options
                Console.Clear();
                foreach (var item in Elements)
                {
                    Console.WriteLine($"[{item.Key}] {item.Name}");
                }

                //read user choice
                var userChoice = Console.ReadKey(true);

                //execute user choice
                Console.Clear();

                IElement selected = Elements.Where(x => x.Key == userChoice.KeyChar).FirstOrDefault();
                if (selected != null)
                {
                    if (!String.IsNullOrEmpty(selected.InfoDuringExe))   //part of elements have not InfoDuringExe, ex. if it writing sth else
                        Console.WriteLine(selected.InfoDuringExe);
                    selected.Exec();
                    continueLoop = selected.IsContinueElement;
                }
                else
                {
                    Console.WriteLine(infoBadNumberChoice);
                    Console.ReadKey();
                }


                #region Alternative on foreach
                //foreach (var item in Elements)
                //{
                //    if (item.Key == userChoice.KeyChar)
                //    {
                //        if (!String.IsNullOrEmpty(item.InfoDuringExe))   //part of elements have not InfoDuringExe, ex. if it writing sth else
                //            Console.WriteLine(item.InfoDuringExe);
                //        item.Exec();
                //        continueLoop = item.IsContinueElement;
                //        break;
                //    }
                //}
                #endregion

            }
        }
    }
}
