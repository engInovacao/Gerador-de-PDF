using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Atualizador
{
    static class Program
    {
        public static string vNova = "";
        public static string FTP_USER = null;
        public static string FTP_PASSWOR = null;
        public static string FTP_SERVER = null;
        public static string FTP_PORT = null;
        public static string FTP_PATH = null;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmPrincipal());

        }
    }
}
