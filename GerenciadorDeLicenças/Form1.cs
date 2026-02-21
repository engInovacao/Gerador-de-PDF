
using System;
using System.IO;
using System.Text;
using System.Windows.Forms;
using KeyData;

namespace GerenciadorDeLicenças
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Text = "Gerenciador de Licenças 1.1";
        }

        private int r = -1;
        private int c = -1;
        private void btnUpLoad_Click(object sender, System.EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            this.Enabled = false;
            WebMapipulation web = new WebMapipulation();
            web.upload(dgv, lblMsg);
            this.Cursor = Cursors.Default;
            this.Enabled = true;
        }

        private void btnDownload_Click(object sender, System.EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            this.Enabled = false;
            WebMapipulation web = new WebMapipulation();
            web.Download(dgv,lblMsg);
            this.Cursor = Cursors.Default;
            this.Enabled = true;
        }

        private void btnGerarBin_Click(object sender, System.EventArgs e)
        {
            if (r == -1)
            {
                MessageBox.Show("Escolha um item");
                return;
            }

            var res = dgv.Rows[r].Cells["ANO"].Value + ";"
                                                     + dgv.Rows[r].Cells["MES"].Value + ";"
                                                     + dgv.Rows[r].Cells["DIA"].Value + ";"
                                                     + dgv.Rows[r].Cells["CHAVE"].Value + ";"
                                                     + dgv.Rows[r].Cells["FTP_USER"].Value + ";"
                                                     + dgv.Rows[r].Cells["FTP_PASSWORD"].Value + ";"
                                                     + dgv.Rows[r].Cells["FTP_SERVER"].Value + ";"
                                                     + dgv.Rows[r].Cells["FTP_PORT"].Value + ";"
                                                     + dgv.Rows[r].Cells["FTP_PATH"].Value + ";";
            if (res.Length < 20)
            {
                MessageBox.Show("Erro ao criar o arquivo BIN, verifique se seleionou o item correto e tente novamente");
                return;
            }
            var enc = KeyGen.Encrypt(res,"engeselt");
            
            File.WriteAllText(path()+"\\PDF.BIN",enc,Encoding.UTF8);
            MessageBox.Show("Arquivo gerado em:\n\n" + path() + "\\PDF.BIN");

        }

        private void dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            r = e.RowIndex;
            c = e.ColumnIndex;
        }

        public static string path()
        {
            var p = System.Reflection.Assembly.GetExecutingAssembly().Location;
            FileInfo fi = new FileInfo(p);
            return fi.DirectoryName;
        }

        private void Form1_Load(object sender, System.EventArgs e)
        {
            btnDownload_Click(new object(), new EventArgs());
        }

        private void dgv_CellValueNeeded(object sender, DataGridViewCellValueEventArgs e)
        {

        }

        private void dgv_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            if (dgv.Rows.Count == 1)
            {
                return;
            }
            var i = dgv.Rows.Count - 2;
            if (dgv.Rows[i].Cells["FTP_USER"].Value == null || dgv.Rows[i].Cells["FTP_USER"].Value.ToString()=="")
            {
                dgv.Rows[i].Cells["FTP_USER"].Value = "sar";
            }
            if (dgv.Rows[i].Cells["FTP_PASSWORD"].Value == null || dgv.Rows[i].Cells["FTP_PASSWORD"].Value.ToString() == "")
            {
                dgv.Rows[i].Cells["FTP_PASSWORD"].Value = "sar2019";
            }
            if (dgv.Rows[i].Cells["FTP_SERVER"].Value == null || dgv.Rows[i].Cells["FTP_SERVER"].Value.ToString() == "")
            {
                dgv.Rows[i].Cells["FTP_SERVER"].Value = "35.167.136.221";
            }
            if (dgv.Rows[i].Cells["FTP_PORT"].Value == null || dgv.Rows[i].Cells["FTP_PORT"].Value.ToString() == "")
            {
                dgv.Rows[i].Cells["FTP_PORT"].Value = "22";
            }
            if (dgv.Rows[i].Cells["FTP_PATH"].Value == null || dgv.Rows[i].Cells["FTP_PATH"].Value.ToString() == "")
            {
                dgv.Rows[i].Cells["FTP_PATH"].Value = "/var/www/html/sar/v2";
            }
        }
    }
}
