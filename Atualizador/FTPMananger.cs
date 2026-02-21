using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Renci.SshNet;

namespace Atualizador
{
    public static class FTPMananger
    {
      
        private static WebResponse FTPRequest(string url)
        {
            HttpWebRequest myWebRequest = (HttpWebRequest)WebRequest.Create(url);

            // Obtain the 'Proxy' of the  Default browser.  
            IWebProxy proxy = myWebRequest.Proxy;

            WebProxy webProx = new WebProxy(proxy.GetProxy(myWebRequest.RequestUri));
            webProx.BypassProxyOnLocal = false;
            myWebRequest.Proxy = webProx;
            ICredentials credentials = new NetworkCredential("sar.sistema", "sar.sistema#2019");
            myWebRequest.Proxy.Credentials = credentials;
            myWebRequest.Method = WebRequestMethods.File.DownloadFile;
            return (HttpWebResponse)myWebRequest.GetResponse();
        }

        public static void BaixarVersao(string urlBase, string tempPath)
        {
            var response = FTPRequest(Path.Combine(urlBase, "pdf.eng"));

            using (Stream ftpStream = response.GetResponseStream())
            using (Stream fileStream = File.Create(tempPath))
            {
                ftpStream.CopyTo(fileStream);
            }
        }
    }
}
