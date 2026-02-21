using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDF.Classes
{
    public class clsUtils
    {
        public static bool éNuloOuVazio(object ob)
        {
            if (ob == null)
            {
                return true;
            }

            if (ob.ToString().Length == 0)
            {
                return true;
            }

            return false;
        }
    }
}
