using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using KeyData;

namespace KeyGenerate
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var cv = new KeyGen();
           
            var p = System.Reflection.Assembly.GetExecutingAssembly().Location;
            FileInfo fi = new FileInfo(p);
            cv.Write(fi.DirectoryName + "\\PDF.BIN", dtp.Value.Year.ToString("0000")+ ","+dtp.Value.Month.ToString("00")+","+ dtp.Value.Day.ToString("00")+","+txtChave.Text);
            MessageBox.Show("Aqruivo gerado em " + fi.DirectoryName + "\\PDF.BIN");
            this.Close();
        }
    }
}
