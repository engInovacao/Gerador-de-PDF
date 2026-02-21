using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorDeLicenças
{
    public class Utils
    {
        public static string path()
        {
            var p = System.Reflection.Assembly.GetExecutingAssembly().Location;
            FileInfo fi = new FileInfo(p);
            return fi.DirectoryName;
        }
    }
}

