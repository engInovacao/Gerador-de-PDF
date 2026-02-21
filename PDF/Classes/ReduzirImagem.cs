using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDF.Classes
{
    public  class ReduzirImagem
    {
        public static void reduzImagem(string origem, string destino,long percentualReducao=30)
        {
            FileInfo fi = new FileInfo(origem);
            using (Bitmap bitmap = new Bitmap(Image.FromFile(fi.FullName)))
            {
                var myEncoder = System.Drawing.Imaging.Encoder.Quality;
                var myEncoderParameters = new EncoderParameters(1);
                var myEncoderParameter = new EncoderParameter(myEncoder, percentualReducao);
                myEncoderParameters.Param[0] = myEncoderParameter;
                bitmap.Save(destino,
                    ImageCodecInfo.GetImageDecoders()
                        .FirstOrDefault(codec => codec.FormatID == ImageFormat.Jpeg.Guid), myEncoderParameters);
                bitmap.Dispose();
                GC.SuppressFinalize(bitmap);
                GC.SuppressFinalize(myEncoder);
                GC.SuppressFinalize(myEncoderParameters);
                GC.Collect();
            }
        }
    }
}
