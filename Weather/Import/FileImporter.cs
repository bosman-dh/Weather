using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weather.EF;
using Weather.Models;

namespace Weather.Import
{
    abstract class FileImporter
    {
        public string FilePath { get; private set; }

        public FileImporter(string pathFileImport)
        {
            FilePath = pathFileImport;
        }

        public virtual IEnumerable<string> FileImport()
        {
            using (StreamReader sr = new StreamReader(FilePath))
            {
                string line;

                while ((line = sr.ReadLine()) != null)
                    yield return line;
            }
        }
    }
}