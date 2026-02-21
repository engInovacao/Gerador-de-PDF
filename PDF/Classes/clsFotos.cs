

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Net.Sockets;

namespace PDF.Classes
{
    public class Utils
    {
        public static string path()
        {
            var p = System.Reflection.Assembly.GetExecutingAssembly().Location;
            FileInfo fi = new FileInfo(p);
            return fi.DirectoryName;
        }

        public static DateTime GetNistTime()
        {
            try
            {
                var client = new TcpClient("time.nist.gov", 13);
                DateTime localDateTime;
                using (var streamReader = new StreamReader(client.GetStream()))
                {
                    var response = streamReader.ReadToEnd();
                    var utcDateTimeString = response.Substring(7, 17);
                    localDateTime = DateTime.ParseExact(utcDateTimeString, "yy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal);
                }

                return localDateTime;
            }
            catch (Exception e)
            {
                return DateTime.Now;
            }
        }
    }
    [Serializable]
    public partial class clsFotos :IDisposable
    {
     
        public Image Logo { get; set; }
        public string LogoType { get; set; }
        public string Titulo { get; set; }
        public string Relatorio { get; set; }
        public List<clsFotosSeq> Dados { get; set; }

        public void Dispose()
        {
            Logo.Dispose();
            foreach (clsFotosSeq dado in Dados)
            {
                dado.Dispose();
            }
        }
    }

    [Serializable]
    public partial class clsFotosSeq : IDisposable
    {
        public bool Exportar { get; set; }
        public string Id { get; set; }
        //public Image Foto { get; set; }
        public string Descricao { get; set; }
        public DateTime Data { get; set; }
        public string Caminho { get; set; }
        public string C1 { get; set; }
        public string C2 { get; set; }
        public byte[] ImageB { get; set; }
        public int Rotacao { get; set; }
        public void Dispose()
        {
            //Foto.Dispose();
            ImageB = null;
        }
    }
}
