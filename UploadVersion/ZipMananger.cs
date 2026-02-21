using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UploadVersion
{
    public static class ZipMananger
    {

        public static void Extrair(string zipPath)
        {
            ZipFile.ExtractToDirectory(zipPath, Path.GetDirectoryName(zipPath) + "\\bin_TEMP");
        }

        public static void Compactar(string origem, string destino)
        {
            if (File.Exists(destino))
            {
                File.Delete(destino);
            }
            ZipFile.CreateFromDirectory(origem, destino);
        }
    }
}
