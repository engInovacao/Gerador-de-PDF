
using Renci.SshNet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Windows.Forms;

namespace GerenciadorDeLicenças
{
    public class WebMapipulation
    {
        public const string Server = "ftp.engeselt.com.br";//"35.167.136.221";
        public const int Port = 22;
        public const string Password = "sar2019";
        public const string User = "sar";
        public const string mURL = "/home/sar/sar/v2";//"/var/www/html/sar/v2";

        public void upload(DataGridView dgv,Label msg)
        {
             var txt = "";
            var dados = "";
            foreach (var column in dgv.Columns)
            {
                txt += ";" + ((System.Windows.Forms.DataGridViewColumn) column).Name;
            }

            txt = txt.Substring(1);

            for (int i = 0; i < dgv.Rows.Count - 1; i++)
            {
                for (int j = 0; j < txt.Split(';').Length; j++)
                {
                    var campo = txt.Split(';')[j];
                    dados += dgv.Rows[i].Cells[campo].Value.ToString()+";";
                }

                dados = dados.Substring(0, dados.Length - 1);
                dados += Environment.NewLine;
            }


            msg.Text = "Gerando arquivo...";msg.Refresh();

            var filePath = Utils.path() + "\\pdflista.csv";

            File.WriteAllText(filePath, txt + Environment.NewLine + dados);

            //using (var client = new WebClient())
            //{
            //    ICredentials credentials = new NetworkCredential(User, Password);
            //    client.Credentials = credentials;
            //    //client.Credentials = new NetworkCredential(User, Password);
            //    client.UploadFile("ftp://35.167.136.221/home/sar/sar/v2/pdflista.csv", WebRequestMethods.Ftp.UploadFile, filePath);
            //}

            PasswordConnectionInfo connectionInfo = new PasswordConnectionInfo("35.167.136.221", 22, "ftpengeselt", "ftpengeselt");

            using (var client = new SftpClient(Server, Port, User, Password)) //new SftpClient(connectionInfo)) //
            {
                msg.Text = "Conectando ao FTP"; msg.Refresh();
                
                client.Connect();
                if (client.IsConnected)
                {
                    msg.Text = "Conectado, enviando..."; msg.Refresh();

                    using (var fileStream = new FileStream(filePath, FileMode.Open))
                    {

                        client.BufferSize = 4 * 1024; // bypass Payload error large files
                        client.ChangeDirectory(mURL);
                        client.UploadFile(fileStream, "pdflista.csv", null);
                    }
                    msg.Text = "Upload concluído com sucesso!"; msg.Refresh();
                }
                else
                {
                    msg.Text = "NÃO CONECTADO!"; msg.Refresh();
                }
            }

            File.Delete(filePath);
        }

        public void Download(DataGridView dgv, Label msg)
        {

            msg.Text = "Conectando ao FTP"; msg.Refresh();
            var url = "http://35.167.136.221/sar/v2/pdflista.csv";
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
            var txt = "";
            try
            {
                var res = (HttpWebResponse)myWebRequest.GetResponse();


                if (res == null)
                {
                    MessageBox.Show("Falha no Download\n\n", "ERRO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    msg.Text = "Falha no Download";
                    msg.Refresh();
                    return;
                }

                if (res.StatusCode != HttpStatusCode.OK)
                {
                    MessageBox.Show("Falha no Download\n\n" + res.StatusDescription, "ERRO", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    msg.Text = "Falha no Download";
                    msg.Refresh();
                    return;
                }

                var streamReader = new StreamReader(res.GetResponseStream());

                txt = streamReader.ReadToEnd();

                streamReader.Dispose();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
                return;
            }

            txt = txt.Replace(Environment.NewLine, "¨");

            var head = new List<string>();

            dgv.Rows.Clear();

            msg.Text = "Carregando dados"; msg.Refresh();
            for (int i = 0; i < txt.Split('¨').Length - 1; i++)
            {
                if (head.Count == 0)
                {

                    for (int j = 0; j < txt.Split('¨')[i].Split(';').Length; j++)
                    {
                        head.Add(txt.Split('¨')[i].Split(';')[j]);
                    }
                }
                else
                {
                    dgv.Rows.Add();
                    int k = dgv.Rows.Count - 2;
                    for (int j = 0; j < head.Count; j++)
                    {
                       
                        var d = txt.Split('¨')[i].Split(';')[j];
                        dgv.Rows[k].Cells[head[j]].Value = d;
                    }
                }
            }

            bool apagar = true;
            while (apagar)
            {
                apagar = false;
                for (int i = 0; i < dgv.Rows.Count; i++)
                {
                    if (dgv.Rows[i].Cells[1].Value != null)
                    {
                        if (Convert.ToInt32(dgv.Rows[i].Cells[1].Value.ToString()) < DateTime.Now.Year)
                        {
                            dgv.Rows.RemoveAt(i);
                            apagar = true;
                            break;
                        }

                    }
                }
            }


            msg.Text = "Download Concluído com sucesso!"; msg.Refresh();


        }
    }

}
