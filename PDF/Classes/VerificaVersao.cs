using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Cache;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Renci.SshNet;

namespace PDF.Classes
{
    class VerificaVersao
    {
        [DllImport("WinInet.dll", PreserveSig = true, SetLastError = true)]
        public static extern void DeleteUrlCacheEntry(string url);
        public static WebResponse GetResponseNoCache(Uri uri)
        {
            // Set a default policy level for the "http:" and "https" schemes.
            HttpRequestCachePolicy policy = new HttpRequestCachePolicy(HttpRequestCacheLevel.Default);
            HttpWebRequest.DefaultCachePolicy = policy;
            // Create the request.
            WebRequest request = WebRequest.Create(uri);
            // Define a cache policy for this request only. 
            HttpRequestCachePolicy noCachePolicy = new HttpRequestCachePolicy(HttpRequestCacheLevel.NoCacheNoStore);
            request.CachePolicy = noCachePolicy;
            request.Timeout = 10 * 1000;
            WebResponse response = request.GetResponse();
            Console.WriteLine("IsFromCache? {0}", response.IsFromCache);
            return response;
        }

   
        public static string DownLoadVerion()
        {


            try
            {
                //

                var url = "";
                url = "http://" + Program.FTP_SERVER + "/sar/v2/versaopdf.txt";
                //if (!IsUrlAlive(url, 15))
                //{
                //    return 0;
                // }


                HttpWebRequest myWebRequest = (HttpWebRequest)WebRequest.Create(url);

                try
                {
                    DeleteUrlCacheEntry(url);
                    GetResponseNoCache(myWebRequest.RequestUri);
                }
                catch (Exception e)
                {
                    //MessageBox.Show("Talvez não seja possível atualizar a versão, não consegui limpar o cache, mas vamos tentar assim mesmo ok?");
                }


                myWebRequest.Timeout = 10 * 1000;

                // Obtain the 'Proxy' of the  Default browser.  
                IWebProxy proxy = myWebRequest.Proxy;
                
                WebProxy webProx = new WebProxy(proxy.GetProxy(myWebRequest.RequestUri));
                webProx.BypassProxyOnLocal = false;
                myWebRequest.Proxy = webProx;
                ICredentials credentials = new NetworkCredential("sar.sistema", "sar.sistema#2019");
                myWebRequest.Proxy.Credentials = credentials;
                myWebRequest.Method = WebRequestMethods.File.DownloadFile;
                var res = (HttpWebResponse)myWebRequest.GetResponse();
             
                if (res == null)
                {
                    throw new Exception("Falha no Dowload");
                }

                if (res.StatusCode != HttpStatusCode.OK)
                {
                    throw new Exception("Falha no Download\n\n" + res.StatusDescription);
                }

                var streamReader = new StreamReader(res.GetResponseStream());
                return streamReader.ReadToEnd();
            }
            catch (Exception e)
            {
               
                return e.ToString();
            }
        }
    }
}
