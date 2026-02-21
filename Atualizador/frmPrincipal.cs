using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.Cache;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using KeyData;

namespace Atualizador
{
    public partial class frmPrincipal : Form
    {
        public frmPrincipal()
        {
            InitializeComponent();
        }

        private bool iniciado = false;
        private bool TudoOK = false;
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

        private void frmPrincipal_Load(object sender, EventArgs e)
        {
            
        }

        private void frmPrincipal_Activated(object sender, EventArgs e)
        {
            if (iniciado) { return; }

            iniciado = true;
            Thread.Sleep(5000);

            ChecarValidade();
            if (!File.Exists(path() + "\\PDF_ENG.EXE"))
            {
                MessageBox.Show("Não foi possivel localizar o arquivo " + path() + "\\PDF_ENG.EXE", "ERRO");
                Close();
                return;
            }

            var versionInfo = FileVersionInfo.GetVersionInfo(path() + "\\PDF_ENG.EXE");
            var versaoEXEC = versionInfo.FileVersion; 
            lblVersaoAtual.Text = "Versão atual: " + versaoEXEC;
            this.Refresh();
            NovaAtual();
            this.Refresh();
            lblMsg.Text = ""; lblMsg.Refresh();
            if (versaoEXEC == Program.vNova)
            {
                rtf.Clear();
                rtf.Text = "Sua versão já está atualizada";
                return;
            }
            lblMsg.Text = "Aguarde baixando versão " + Program.vNova; lblMsg.Refresh();
            AtualizaVersao();
            lblMsg.Text = ""; lblMsg.Refresh();
            this.Refresh();
        }

        private void AtualizaVersao()
        {
            var pastaBase = path() + "\\TempPDF";
            try
            {
                if (Directory.Exists(pastaBase))
                {
                    Directory.Delete(pastaBase, true);
                }

                Directory.CreateDirectory(pastaBase);



                //Verifica se o programa está rodando
                Process[] processes = Process.GetProcesses();
                foreach (Process p in processes)
                {
                    if (!String.IsNullOrEmpty(p.MainWindowTitle))
                    {
                        if (p.ToString().Contains("PDF_Eng"))
                        {
                            if (!p.MainWindowTitle.Contains("Visual"))
                            {
                                MessageBox.Show(
                                    "Não é possível atualizar o PDF_Eng, ele está em execução no momento!\n\nFeche a aplicação SAR e tente novamente!",
                                    "Atualização", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                        }
                    }
                }

                //Baixa o arquivo
              
                try
                {
                    var url = "http://"+Program.FTP_SERVER+"/sar/v2/pdf.eng";
                    DeleteUrlCacheEntry(url);
                    HttpWebRequest myWebRequest = (HttpWebRequest) WebRequest.Create(url);

                    try
                    {
                        GetResponseNoCache(myWebRequest.RequestUri);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);

                    }

                    // Obtain the 'Proxy' of the  Default browser.  

                    MSG("Conectando ao servidor...");
                     IWebProxy proxy = myWebRequest.Proxy;

                    WebProxy webProx = new WebProxy(proxy.GetProxy(myWebRequest.RequestUri));
                    webProx.BypassProxyOnLocal = false;
                    myWebRequest.Proxy = webProx;
                    ICredentials credentials = new NetworkCredential("sar.sistema", "sar.sistema#2019");
                    myWebRequest.Proxy.Credentials = credentials;
                    myWebRequest.Method = WebRequestMethods.File.DownloadFile;
                    var res = (HttpWebResponse) myWebRequest.GetResponse();
                    if (res.StatusCode != HttpStatusCode.OK)
                    {
                        throw new Exception("Falha no Download\n\n" + res.StatusDescription);
                    }

                    MSG("Recebendo atualização");
                    using (Stream ftpStream = res.GetResponseStream())
                    using (Stream fileStream =
                        File.Create(pastaBase + "\\pdf.eng"))
                    {
                        ftpStream.CopyTo(fileStream);
                    }

                    MSG("Atualização recebida, aplicando");


                    ZipFile.ExtractToDirectory(pastaBase + "\\pdf.eng", pastaBase);

                    File.Delete(pastaBase + "\\pdf.eng");

                    var listFiles = Directory.GetFiles(pastaBase);
                    progressBar1.Value = 0;
                    progressBar1.Maximum = listFiles.Length;
                    foreach (string f in listFiles)
                    {
                        progressBar1.Value++;
                        FileInfo fi = new FileInfo(f);
                        var Origem = fi.FullName;
                        var Destino = path()+ "\\" + fi.Name;
                        MSG("Aplicando arquivo "+ fi.Name);
                        File.Copy(Origem, Destino, true);
                    }
                    Directory.Delete(pastaBase,true);
                    TudoOK = true;
                    MSG("Processo concluído");
                    progressBar1.Value = 0;
                    System.Diagnostics.Process.Start(path() + "\\pdf_eng.exe");
                }
                catch (Exception e)
                {
                    MSG("\n\n\n" + e.ToString());
                }
            }
            catch (Exception e)
            {
                MSG("\n\n\n" + e.ToString());
            }
        }

        private void MSG(string m)
        {
            rtf.Text += m + Environment.NewLine;
            rtf.SelectionStart = rtf.Text.LastIndexOfAny(Environment.NewLine.ToCharArray()) + 1;
            rtf.ScrollToCaret();
            rtf.Refresh();
        }

        private void NovaAtual()
        {
            string txt;
            //msg.Text = "Conectando ao FTP";
            //msg.Refresh();
            var url = "http://"+Program.FTP_SERVER+"/sar/v2/versaopdf.txt";
            HttpWebRequest myWebRequest = (HttpWebRequest)WebRequest.Create(url);
            myWebRequest.Timeout = 10 * 1000;

            // Obtain the 'Proxy' of the  Default browser.  
            IWebProxy proxy = myWebRequest.Proxy;

            WebProxy webProx = new WebProxy(proxy.GetProxy(myWebRequest.RequestUri));
            webProx.BypassProxyOnLocal = false;
            myWebRequest.Proxy = webProx;
            ICredentials credentials = new NetworkCredential("sar.sistema", "sar.sistema#2019");
            myWebRequest.Proxy.Credentials = credentials;
            myWebRequest.Method = WebRequestMethods.File.DownloadFile;
            try
            {
                var res = (HttpWebResponse)myWebRequest.GetResponse();

                if (res.StatusCode != HttpStatusCode.OK)
                {
                    MessageBox.Show(@"Falha no Download\n\n" + res.StatusDescription, @"ERRO", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    //msg.Text = "Falha no Download";
                    //msg.Refresh();
                    return;
                }

                var streamReader = new StreamReader(res.GetResponseStream());
                txt = streamReader.ReadToEnd();
                Program.vNova = txt;
                lblNovaVersao.Text = "Nova versão: " + txt;lblNovaVersao.Refresh();
                streamReader.Dispose();
            }
            catch (Exception ex)
            {
                lblMsg.Text = "Problema na conexão"; lblMsg.Refresh();
                return;
            }

        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            Close();
        }

        public static string path()
        {
            var p = System.Reflection.Assembly.GetExecutingAssembly().Location;
            FileInfo fi = new FileInfo(p);
            return fi.DirectoryName;
        }

        private void ChecarValidade()
        {
            if (!File.Exists(path() + "\\PDF.BIN"))
            {
                MessageBox.Show(@"Arquivo " + path() + @"\PDF.BIN, não encontrado", @"Atenção",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                Close();
                return;
            }

            var t = File.ReadAllText(path() + "\\PDF.BIN", Encoding.UTF8);
            var r = KeyGen.Decrypt(t, "engeselt");
         
            Program.FTP_USER = r.Split(';')[4];
            Program.FTP_PASSWOR = r.Split(';')[5];
            Program.FTP_SERVER = r.Split(';')[6];
            Program.FTP_PORT = r.Split(';')[7];
            Program.FTP_PATH = r.Split(';')[8];

        }
    }
}
