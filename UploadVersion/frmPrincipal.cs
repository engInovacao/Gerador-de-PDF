using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Renci.SshNet;

namespace UploadVersion
{
    public partial class frmPrincipal : Form
    {
        private string PastaBase = "";
        private string versao = "";
        public const string User = "sar";
        public const string Password = "sar2019";
        public const string Server = "35.167.136.221";


        public const int Port = 22;

        public frmPrincipal()
        {
            InitializeComponent();
        }

        private void btnPasta_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fb = new FolderBrowserDialog();
            fb.Description = "Selecione a pasta Base";
            if (fb.ShowDialog() != DialogResult.OK)
            {
                Close();
            }

            PastaBase = fb.SelectedPath;
            lblPasta.Text = PastaBase;
            if (!File.Exists(PastaBase + "\\PDF_Eng.exe"))
            {
                MessageBox.Show("Arquivo " + PastaBase + "\\PDF_Eng.exe" + " Não existe");
                return;
            }
           
            FileInfo fi = new FileInfo(PastaBase + "\\PDF_Eng.exe");

            var versionInfo = FileVersionInfo.GetVersionInfo(fi.FullName);
            versao = versionInfo.FileVersion; // Wi
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            if (PastaBase.Length == 0)
            {
                MessageBox.Show("Informe a pasta base");
                return;
            }

            this.Cursor = Cursors.WaitCursor;
            lblmsg.Text = "Compactando...";
            lblmsg.Refresh();
            ZipMananger.Compactar(PastaBase, path() + "\\pdf.eng");
            lblmsg.Text = "Fazendo upload";
            lblmsg.Refresh();
            upload();
            lblmsg.Text = "";
            lblmsg.Refresh();
            MessageBox.Show("Processo concluído!");
            this.Cursor = Cursors.Default;
        }

        public void upload()
        {

            var fileInfo = new FileInfo(path() + "\\pdf.eng");
            if (fileInfo == null) return;


            using (var client = new SftpClient(Server, Port, User, Password))
            {
                client.Connect();
                if (client.IsConnected)
                {
                    Debug.WriteLine("I'm connected to the client");

                    using (var fileStream = new FileStream(fileInfo.FullName, FileMode.Open))
                    {

                        client.BufferSize = 4 * 1024; // bypass Payload error large files
                        client.ChangeDirectory("/var/www/html/sar/v2");
                        client.UploadFile(fileStream, fileInfo.Name, null);
                    }
                }
                else
                {
                    Debug.WriteLine("I couldn't connect");
                }
            }

            //Gera arquivo de versao
            var arqVer = path() + "\\versaopdf.txt";
            File.WriteAllText(arqVer,versao);
            using (var client = new SftpClient(Server, Port, User, Password))
            {
                client.Connect();
                if (client.IsConnected)
                {
                    Debug.WriteLine("I'm connected to the client");

                    using (var fileStream = new FileStream(arqVer, FileMode.Open))
                    {

                        client.BufferSize = 4 * 1024; // bypass Payload error large files
                        client.ChangeDirectory("/var/www/html/sar/v2");
                        client.UploadFile(fileStream, "versaopdf.txt", null);
                    }
                }
                else
                {
                    Debug.WriteLine("I couldn't connect");
                }
            }
            File.Delete(arqVer);
        }

        public static string path()
        {
            var p = System.Reflection.Assembly.GetExecutingAssembly().Location;
            FileInfo fi = new FileInfo(p);
            return fi.DirectoryName;
        }
    }
}
