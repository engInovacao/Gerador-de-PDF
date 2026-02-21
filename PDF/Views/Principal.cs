using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Management;
using System.Net;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using KeyData;
using MetadataExtractor;
using MetadataExtractor.Formats.Exif;
using PDF.Classes;
using PDF.ExifUtils.Exif;
using PDF.ExifUtils.Exif.IO;
using PDF.Properties;
using Renci.SshNet.Security;
using Directory = System.IO.Directory;

namespace PDF
{
    public partial class Principal : Form
    {
        public bool MostrarCoordenadas = false;
        public bool MostrarDataHora = false;
        public string AtualizarVersao = "";
        private int _row = -1;
        private int _col = -1;
        private Image _thumb;
        private Image _img;
        private string _filesExc = "";
        private Regex reg = new Regex(@"^((?<D>\d{1,2}(\.\d+)?)(?<W>[SN])|(?<D>\d{2})(?<M>\d{2}(\.\d+)?)(?<W>[SN])|(?<D>\d{2})(?<M>\d{2})(?<S>\d{2}(\.\d+)?)(?<W>[SN])|(?<D>\d{1,3}(\.\d+)?)(?<W>[WE])|(?<D>\d{3})(?<M>\d{2}(\.\d+)?)(?<W>[WE])|(?<D>\d{3})(?<M>\d{2})(?<S>\d{2}(\.\d+)?)(?<W>[WE]))$");
  

        public Principal()
        {
            InitializeComponent();

         
        }

        private void ChecarValidade()
        {
            var txt = "";
            var r = "";
            try
            {
                string info1;
                Ch(out info1);
                string info2 =
                (
                    from nic in NetworkInterface.GetAllNetworkInterfaces()
                    where nic.OperationalStatus == OperationalStatus.Up
                    select nic.GetPhysicalAddress().ToString()
                ).FirstOrDefault();
                if (!File.Exists(Utils.path() + "\\PDF.BIN"))
                {
                    MessageBox.Show(
                        @"Arquivo " + Utils.path() +
                        "\\PDF.BIN, não encontrado\n\nEntre em contato com Engeselt Software informado esta chave de validação \n\n (" +
                        info1 + ")", @"Atenção",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    Clipboard.SetText(info1);
                    MessageBox.Show("A chave de validação foi copiada para sua área de transferência", "Atenção");
                    Close();
                    return;
                }

                var t = File.ReadAllText(Utils.path() + "\\PDF.BIN", Encoding.UTF8);
                r = KeyGen.Decrypt(t, "engeselt");
                if (r.Length < 20)
                {
                    MessageBox.Show("Arquivo BIN corrompido!");
                    Close();
                    return;
                }

                // cv.Read(Utils.path() + "\\PDF.BIN");
                var ano = r.Split(';')[0];
                var mes = r.Split(';')[1];
                var dia = r.Split(';')[2];
                txt = ano + " " + mes + " " + dia;
                var dt = new DateTime(int.Parse(ano), int.Parse(mes),
                    int.Parse(dia));
                var di = Utils.GetNistTime();
                var ch = r.Split(';')[3];
                if (ch != info1)
                {
                    if (ch != info2)
                    {
                        MessageBox.Show(
                            "Atenção! Chave inválida, entre en contato com Engeselt Software para obter um novo arquivo de validação\n\nSua chave é (" +
                            info2 + ")", @"ATENÇÃO",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        Clipboard.SetText(info2);
                        MessageBox.Show("A chave de validação foi copiada para sua área de transferência", "Atenção");
                        Close();
                        return;
                    }
                }

                if (di > dt)
                {
                    MessageBox.Show(@"Validade do programa vencida, entre em contato com Engeselt Software", @"ATENÇÃO",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    Close();
                    return;

                }

                int daysLeftInYear = (int) (dt - di).TotalDays;
                if (daysLeftInYear < 10)
                {
                    if (daysLeftInYear == 0)
                    {
                        MessageBox.Show(@"Sua licença vence hoje", @"ATENÇÃO",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        MessageBox.Show(@"Falta(m) " + daysLeftInYear + @" dia(s) para vencimento da licença ",
                            @"ATENÇÃO",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }

                Program.FTP_USER = r.Split(';')[4];
                Program.FTP_PASSWOR = r.Split(';')[5];
                Program.FTP_SERVER = r.Split(';')[6];
                Program.FTP_PORT = r.Split(';')[7];
                Program.FTP_PATH = r.Split(';')[8];

                DeleteFiles();
            }
            catch (Exception e)
            {
                throw new Exception(" R=" + r + "\n\n" + e.ToString());
            }
        }

        private void Principal_Load(object sender, EventArgs e)
        {
            pictureBox1.Image = Resources.Engeselt3;


           
            ChecarValidade();
            tmrVersao.Enabled = true;



            var assembly = Assembly.GetExecutingAssembly();
            FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
            string version = fvi.FileVersion;
            lblVersao.Text= @"  Versão:" + version;


        }

        private void ChecarVersao()
        {
            try
            {
                var rv =  VerificaVersao.DownLoadVerion();
                if (rv.Length > 10)
                {
                    return;
                }
                System.Reflection.Assembly executingAssembly = System.Reflection.Assembly.GetExecutingAssembly();
                var fieVersionInfo = FileVersionInfo.GetVersionInfo(executingAssembly.Location);
                var versaoExec = fieVersionInfo.FileVersion;
                if (rv != versaoExec)
                {
                    if (MessageBox.Show("Existe uma nova versão do programa, deseja atualiza-la agora?", "Nova versão",
                            MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    {
                        AtualizarVersao = "\"" + Utils.path() + "\\Atualizador.exe\" ";
                        Close();
                    
                        return;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private void rtfTitulo_Validating(object sender, CancelEventArgs e)
        {
            if (rtfTitulo.Text.Trim().Length == 0)
            {
                errp.SetError(rtfTitulo, "Campo obrigatório");
            }
            else
            {
                errp.SetError(rtfTitulo, "");
            }
        }

        private void btnAvancar_Click(object sender, EventArgs e)
        {
            //var rrr = Validation.Valid_StringOnly.IsMatch(rtfRelatorio.Text);
            
            if (dgv.Rows.Count == 0)
            {
                errp.SetError(btnAvancar, "Inclua pelo menos uma foto");
                return;
            }

            //if (rtfTitulo.Text.Trim().Length == 0)
            //{
            //    errp.SetError(rtfTitulo, "Campo obrigatório");
            //    return;
            //}


            //for (int i = 0; i < dgv.Rows.Count; i++)
            //{
            //    var value = dgv.Rows[i].Cells["DESCRICAO"].Value;
            //    if (value == null)
            //    {
            //        errp.SetError(dgv, "Foto sem descriçao no id (" + dgv.Rows[i].Cells["ID"].Value + ")");
            //        return;
            //    }

            //    if (value.ToString().Trim().Length == 0)
            //    {
            //        errp.SetError(dgv, "Foto sem descrição no id (" + dgv.Rows[i].Cells["ID"].Value + ")");
            //        return;
            //    }
            //}

            errp.SetError(rtfTitulo, "");
            errp.SetError(dgv, "");

            SaveFileDialog sf = new SaveFileDialog();
            sf.Filter = @"Arquivo PDF|*.PDF";
            if (sf.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            Cursor = Cursors.WaitCursor;
            new Exportar(this, sf.FileName, nudQtd.Value);
            Cursor = Cursors.Default;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var of = new OpenFileDialog();
            of.Multiselect = true;
            of.Filter = @"Imagens | *.jpg; *.jpeg; *.jpe; *.jfif; *.png; *.img";
            of.Title = @"Selecione as imagens";
            var res = of.ShowDialog();
            if (res != DialogResult.OK)
            {
                return;
            }








            bool reduzirImagem = MessageBox.Show(@"Deseja reduzir as imagens?", "Opção", MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question) == DialogResult.Yes;

            int NextID;

            for (int i = 0; i < of.FileNames.Length; i++)
            {
                if (!File.Exists(of.FileNames[i]))
                {
                    throw new Exception("Erro em btnAdd, arquivo não existe " + of.FileNames[i]);
                }

                FileInfo fi = new FileInfo(of.FileNames[i]);



                //var directories = ImageMetadataReader.ReadMetadata(fi.FullName);

                //foreach (var directory in directories)
                //{
                //    foreach (var tag in directory.Tags)
                //        Console.WriteLine($"{directory.Name} - {tag.Name} = {tag.Description}");
                //}

               
           

                if (reduzirImagem)
                {
                    lblMsg.Text = "Reduzindo imagem " + fi.Name; lblMsg.Refresh();
                    Image i1 = null;
                    try
                    {
                        i1 = new Bitmap(fi.FullName);
                    }
                    catch (Exception ee)
                    {
                        MessageBox.Show("Problema ao carregar a imagem\n\nErro abrindo imagem ("+fi.FullName+")\n\n\n"+ee.ToString(), "ERRO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.Close();
                        return;
                    }


                    var ex = EngExifTag.MSComments;
                    var coment = EngExifReader.GetExifData(i1, ex);
                    bool compactar = true;
                    if (coment.Count != 0)
                    {
                        if (coment[0].Value.ToString().ToUpper().Contains("REDUZIDA"))
                        {
                            i1.Dispose();
                            compactar = false;
                        }
                    }

                    if (compactar)
                    {
                        string newFile = fi.FullName.Replace(fi.Extension, "_R." + fi.Extension.Substring(1));
                        ReduzirImagem.reduzImagem(fi.FullName, newFile);


                        Image i2 = new Bitmap(newFile);

                        EngExifWriter.CloneExifData(i1, i2);


                        EngExifProperty copyright = new EngExifProperty();
                        copyright.Tag = EngExifTag.Copyright;
                        copyright.Value = String.Format("Copyright (c) {0} Engeselt Software.", DateTime.Now.Year);
                        EngExifWriter.AddExifData(i2, copyright);



                        EngExifProperty UserComment = new EngExifProperty(EngExifTag.MSComments, "Imagem Reduzida");
                        UserComment.Type = EngExifType.Ascii;
                        EngExifWriter.AddExifData(i2, UserComment);


                        i2.Save(newFile.Replace("_R.", "_R2."));
                        i1.Dispose();
                        i2.Dispose();
                        File.Copy(newFile.Replace("_R.", "_R2."), newFile, true);
                        File.Delete(newFile.Replace("_R.", "_R2."));
                        GC.Collect();
                        fi = new FileInfo(newFile);
                    }

                    lblMsg.Text = "";
                }


                NextID = NextId();
                dgv.Rows.Add();
                dgv.Rows[dgv.Rows.Count - 1].Cells["ID"].Value = NextID;
                dgv.Rows[dgv.Rows.Count - 1].Cells["CAMINHO"].Value = fi.FullName;

                try
                {
                    _img = Image.FromFile(fi.FullName);
                }
                catch (Exception ee)
                {
                    MessageBox.Show("Erro ao carregar a imagem ("+ fi.FullName + ")\n\n" + ee.ToString(),"ERRO",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    this.Close();
                    return;
                }

                //var v = Exportar.ImageToByteArray(img);
                _thumb = _img.GetThumbnailImage(100, 100, null, IntPtr.Zero);

                foreach (var prop in _img.PropertyItems)
                {
                    if (prop.Id == 0x112)
                    {
                        if (prop.Value[0] == 6)
                        {
                            //rotate = 90;
                            _thumb.RotateFlip(RotateFlipType.Rotate90FlipNone);
                            break;
                        }

                        if (prop.Value[0] == 8)
                        {
                            //rotate = -90;
                            _thumb.RotateFlip(RotateFlipType.Rotate270FlipNone);
                            break;
                        }

                        if (prop.Value[0] == 3)
                        {
                            //rotate = 180;
                            _thumb.RotateFlip(RotateFlipType.Rotate180FlipNone);
                            break;
                        }

                        //prop.Value[0] = 1;
                    }
                }





                dgv.Rows[dgv.Rows.Count - 1].Cells["FOTO"].Value = _thumb;

                DateTime dt = DataFotos.GetDateTakenFromImage(fi.FullName);
                if (dt.Year == 1900)
                {
                    dt = fi.LastWriteTime;
                }

                dgv.Rows[dgv.Rows.Count - 1].Cells["DATA"].Value = dt;
                _img.Dispose();
                Gps(of.FileNames[i], dgv.Rows.Count - 1);

                GC.Collect();
            }
            GC.Collect();
            Sort();
        }

        private void Gps(string fileName, int index)
        {

            try
            {
                var lat = "";
                var lng = "";
                var hora = "";
                var data = "";

                var directories = ImageMetadataReader.ReadMetadata(fileName);
                foreach (var d in directories)
                {
                    if (d.Name.Contains("GPS"))
                    {
                        foreach (var v in d.Tags)
                        {
                            if (v.ToString().Contains("GPS Latitude Ref"))
                            {
                            }

                            if (v.ToString().Contains("GPS Longitude Ref"))
                            {
                            }

                            if (v.ToString().ToUpper().Contains("LATITUDE"))
                            {
                                if (v.ToString().Contains("°"))
                                {
                                    //[GPS] GPS Latitude - -4° 9' 5,26"
                                    lat = v.ToString().Substring(v.ToString().IndexOf('-') + 1);
                                }
                            }

                            if (v.ToString().ToUpper().Contains("LONGI"))
                            {
                                if (v.ToString().Contains("°"))
                                {
                                    // -38° 6' 58,07"
                                    lng = v.ToString().Substring(v.ToString().IndexOf('-') + 1);
                                }
                            }

                            if (v.ToString().Contains("GPS Time-Stamp"))
                            {
                                hora = v.Description.Substring(0, v.Description.IndexOf(','));
                            }

                            if (v.ToString().Contains("GPS Date Stamp"))
                            {
                                data = v.Description;
                            }
                        }
                    }
                }

                if (lat != "")
                {
                    //Latitude "17.21.18S"
                    var grau = lat.Substring(0, lat.IndexOf('°')).Trim();
                    var minu = lat.Substring(lat.IndexOf('°') + 1);
                    minu = minu.Substring(0, minu.IndexOf('\'')).Trim();
                    var seco = lat.Substring(lat.IndexOf('\'') + 1);
                    seco = seco.Substring(0, seco.IndexOf('"'));
                    //var LATv = ConvertDegreeAngleToDouble(grau +"." +minu +"."+ seco + latr);

                    var laTv = Dms2Decimal(double.Parse(grau).ToString("00").Replace("-", "") +
                                           double.Parse(minu).ToString("00") + double.Parse(seco).ToString("00.000") +
                                           "S");
                    dgv.Rows[index].Cells["LAT"].Value = laTv.ToString();


                    //Longitude
                    grau = lng.Substring(0, lng.IndexOf('°')).Trim();
                    minu = lng.Substring(lng.IndexOf('°') + 1);
                    minu = minu.Substring(0, minu.IndexOf('\'')).Trim();
                    seco = lng.Substring(lng.IndexOf('\'') + 1);
                    seco = seco.Substring(0, seco.IndexOf('"'));
                    var lnGv = Dms2Decimal(double.Parse(grau).ToString("00").Replace("-", "") +
                                           double.Parse(minu).ToString("00") + double.Parse(seco).ToString("00.000") +
                                           "S");
                    dgv.Rows[index].Cells["LNG"].Value = lnGv.ToString();

                  
                    
                }

                if (data.Length != 0)
                {
                    var dateStr = data.Replace(":", "-") + " " + hora;

                    if (dateStr.Trim().Length != 0)
                    {
                        DateTime convertedDate = DateTime.SpecifyKind(
                            DateTime.Parse(dateStr),
                            DateTimeKind.Utc);

                        DateTime dt = convertedDate.ToLocalTime();

                        dgv.Rows[index].Cells["DATA"].Value = dt.ToString();
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception("Erro em classe GPS " + e);
            }
        }

       
        private double Dms2Decimal(string dms)
        {
            //DDMMSS.dddS
            double result = double.NaN;
            dms = dms.Replace(",", ".");
            var match = reg.Match(dms);
            string uiSep = CultureInfo.CurrentUICulture.NumberFormat.NumberDecimalSeparator;
            if (match.Success)
            {
                var degrees = double.Parse("0" + match.Groups["D"]);
                var minutes = double.Parse("0" + match.Groups["M"]);
                var seconds = double.Parse("0" + match.Groups["S"].ToString().Replace(".", uiSep));
                var direction = match.Groups["W"].ToString();
                var dec = (Math.Abs(degrees) + minutes / 60d + seconds / 3600d) *
                          (direction == "S" || direction == "W" ? -1 : 1);
                var absDec = Math.Abs(dec);

                if ((((direction == "W" || direction == "E") && degrees <= 180 & absDec <= 180) ||
                     (degrees <= 90 && absDec <= 90)) && minutes < 60 && seconds < 60)
                {
                    result = dec;
                }

            }

            return result;

        }

        private int NextId()
        {
            var nexId = 0;
            if (dgv.Rows.Count == 0)
            {
                return 1;
            }

            for (int i = 0; i < dgv.Rows.Count; i++)
            {
                nexId = i + 1;
                if (Convert.ToInt32(dgv.Rows[i].Cells["ID"].Value) != nexId)
                {
                    break;
                }
            }

            return nexId + 1;
        }

        private void dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            _row = e.RowIndex;
            _col = e.ColumnIndex;
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            if (dgv.Rows.Count == 0 || _row == -1)
            {
                MessageBox.Show(@"Escolha uma foto");
                return;
            }

            if (MessageBox.Show(@"Deseja mesmo remover o item ID(" + dgv.Rows[_row].Cells["ID"].Value + @")", @"Excluir",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) !=
                DialogResult.Yes)
            {
                return;
            }

            dgv.Rows.RemoveAt(_row);
            Reindex();
            _row = -1;
        }

        private void Sort()
        {
            dgv.Sort(dgv.Columns["ID"], ListSortDirection.Ascending);
        }

        private void Reindex()
        {
            for (int i = 0; i < dgv.Rows.Count; i++)
            {
                dgv.Rows[i].Cells["ID"].Value = (i + 1);
            }

            Sort();
        }

        private void salvarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgv.Rows.Count == 0)
                {
                    MessageBox.Show(@"Nenhum registro para salvar");
                    return;
                }

                var sf = new SaveFileDialog();
                sf.Title = @"Salvar em:";
                sf.Filter = @"Arquivo de fotos | *.EngeseltFotos";
                if (sf.ShowDialog() != DialogResult.OK)
                {
                    return;
                }

                lblMsg.Text = @"Aguarde gravando arquivo";
                lblMsg.Refresh();
                IFormatter formatter = new BinaryFormatter();
                var arq = sf.FileName;
                if (!arq.ToUpper().Contains("ENGESELTFOTOS"))
                {
                    arq = sf.FileName + ".EngeseltFotos";
                }

                if (File.Exists(arq))
                {
                    File.Delete(arq);
                }


                var cf = new clsFotos();

                cf.Dados = new List<clsFotosSeq>();
                for (int i = 0; i < dgv.Rows.Count; i++)
                {
                    clsFotosSeq d = new clsFotosSeq();
                    FileInfo fi = new FileInfo(dgv.Rows[i].Cells["CAMINHO"].Value.ToString());
                    d.Caminho = fi.Name;
                    fi = new FileInfo(arq);
                    var n = fi.Name.Replace(fi.Extension, "");
                    d.Caminho = d.Caminho.Replace(n + "_", "");
                    var dd = dgv.Rows[i].Cells["DATA"].Value.ToString();
                    d.Data = Convert.ToDateTime(dd);

                    if (dgv.Rows[i].Cells["DESCRICAO"].Value != null)
                    {
                        d.Descricao = dgv.Rows[i].Cells["DESCRICAO"].Value.ToString();
                    }

                    if (dgv.Rows[i].Cells["EXPORTAR"].Value != null)
                    {
                        d.Exportar = (bool) dgv.Rows[i].Cells["EXPORTAR"].Value;
                    }

                    //d.Foto =  Image.FromFile(dgv.Rows[i].Cells["CAMINHO"].Value.ToString());
                    d.ImageB = Exportar.ImageToByteArray(Image.FromFile(dgv.Rows[i].Cells["CAMINHO"].Value.ToString()));
                    d.Id = dgv.Rows[i].Cells["ID"].Value.ToString();
                    if (dgv.Rows[i].Cells["LAT"].Value != null)
                    {
                        d.C1 = dgv.Rows[i].Cells["LAT"].Value.ToString();
                    }

                    if (dgv.Rows[i].Cells["LNG"].Value != null)
                    {
                        d.C2 = dgv.Rows[i].Cells["LNG"].Value.ToString();
                    }

                    if (dgv.Rows[i].Cells["ROTACAO"].Value.ToString() == null ||
                        dgv.Rows[i].Cells["ROTACAO"].Value.ToString() == "")
                    {
                        dgv.Rows[i].Cells["ROTACAO"].Value = 0;
                    }
                    d.Rotacao = int.Parse(dgv.Rows[i].Cells["ROTACAO"].Value.ToString());
                    cf.Dados.Add(d);
                }

                cf.Logo = (Image) pictureBox1.Image.Clone();
                if (pictureBox1.Tag == null)
                {
                    pictureBox1.Tag = "BMP";
                }
                cf.LogoType = pictureBox1.Tag.ToString();

                cf.Titulo = rtfTitulo.Text;
                cf.Relatorio = rtfRelatorio.Text;
                Stream stream = new FileStream(arq, FileMode.Create, FileAccess.Write, FileShare.None);
                formatter.Serialize(stream, cf);
                stream.Close();
                stream.Dispose();
                cf.Dispose();
                GC.Collect();
                lblMsg.Text = "";
                lblMsg.Refresh();
                MessageBox.Show(@"Arquivo garavdo com sucesso!");
            }
            catch (Exception exception)
            {
                throw new Exception("Erro em salvarToolStripMenuItem " + exception);
            }
        }

        private void arquivoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                var of = new OpenFileDialog();
                of.Multiselect = true;
                of.Filter = @"Arquivo de fotos | *.EngeseltFotos";
                of.Title = @"Selecione o arquivo";
                var res = of.ShowDialog();
                if (res != DialogResult.OK)
                {
                    return;
                }

                lblMsg.Text = @"Aguarde recuperando arquivo";
                lblMsg.Refresh();

                //lista = new List<clsFotos>();
                IFormatter formatter = new BinaryFormatter();
                Stream stream = new FileStream(of.FileName, FileMode.Open, FileAccess.Read, FileShare.Read);
                clsFotos dados = (clsFotos) formatter.Deserialize(stream);
                stream.Close();
                stream.Dispose();
                rtfTitulo.Text = dados.Titulo;
                rtfRelatorio.Text = dados.Relatorio;
                pictureBox1.Image = new Bitmap(dados.Logo);
                pictureBox1.Refresh();
                pictureBox1.Tag = dados.LogoType;
                dgv.Rows.Clear();
                _filesExc = new FileInfo(of.FileName).Name.Replace(new FileInfo(of.FileName).Extension, "");
                foreach (clsFotosSeq c in dados.Dados)
                {
                    dgv.Rows.Add();
                    var i = dgv.Rows.Count - 1;
                    dgv.Rows[i].Cells["DESCRICAO"].Value = c.Descricao;
                    dgv.Rows[i].Cells["ID"].Value = c.Id;
                    dgv.Rows[i].Cells["LAT"].Value = c.C1;
                    dgv.Rows[i].Cells["LNG"].Value = c.C2;
                    dgv.Rows[i].Cells["DATA"].Value = c.Data;
                    try
                    {
                        dgv.Rows[i].Cells["ROTACAO"].Value = c.Rotacao;
                    }
                    catch (Exception exception)
                    {

                    }
                    _img = Exportar.byteArrayToImage(c.ImageB);

                    //img = new Bitmap(c.Foto);
                    FileInfo fi = new FileInfo(of.FileName);
                    var arq = Utils.path() + "\\" + fi.Name.Replace(fi.Extension, "");
                    fi = new FileInfo(c.Caminho);
                    arq = arq + "_" + fi.Name.Replace(fi.Extension, ".BTS");
                    if (File.Exists(arq))
                    {
                        File.Delete(arq);
                    }

                    Exportar.ByteArrayToFile(arq, c.ImageB);
                    //img.Save(arq);
                    dgv.Rows[i].Cells["CAMINHO"].Value = arq;


                    _thumb = _img.GetThumbnailImage(100, 100, null, IntPtr.Zero);

                    foreach (var prop in _img.PropertyItems)
                    {
                        if (prop.Id == 0x112)
                        {
                            if (prop.Value[0] == 6)
                            {
                                //rotate = 90;
                                _thumb.RotateFlip(RotateFlipType.Rotate90FlipNone);
                                break;
                            }

                            if (prop.Value[0] == 8)
                            {
                                //rotate = -90;
                                _thumb.RotateFlip(RotateFlipType.Rotate270FlipNone);
                                break;
                            }

                            if (prop.Value[0] == 3)
                            {
                                //rotate = 180;
                                _thumb.RotateFlip(RotateFlipType.Rotate180FlipNone);
                                break;
                            }

                            //prop.Value[0] = 1;
                        }
                    }


                  
                    dgv.Rows[i].Cells["FOTO"].Value = _thumb;


                    _img.Dispose();
                }

                dados.Dispose();
                GC.Collect();
                lblMsg.Text = "";
                lblMsg.Refresh();
                Refresh();
            }
            catch (Exception exception)
            {

                throw new Exception("Erro em arquivoToolStripMenuItem_Click " + exception);
            }
        }

        private void dgv_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            var r = e.RowIndex;
            var c = e.ColumnIndex;
            if (r == -1)
            {
                return;
            }

            if (dgv.Columns[c].Name == "FOTO")
            {
                try
                {
                    var path = Assembly.GetExecutingAssembly().Location;
                    FileInfo fi = new FileInfo(path);
                    File.Delete(fi.DirectoryName + "\\FT_" + dgv.Rows[r].Cells["ID"].Value + ".jpg");

                    //((Image) dgv.Rows[r].Cells["IMAGEM"].Value).Save(
                    //    fi.DirectoryName + "\\FT_" + dgv.Rows[r].Cells["ID"].Value + ".jpg");

                    GC.Collect();

                    Process externalProcess = new Process();
                    externalProcess.StartInfo.FileName =
                        fi.DirectoryName + "\\FT_" + dgv.Rows[r].Cells["ID"].Value + ".jpg";
                    externalProcess.StartInfo.WindowStyle = ProcessWindowStyle.Maximized;
                    externalProcess.Start();
                }
                catch (Exception exception)
                {
                    if (exception.HResult == -2147024809)
                    {
                        MessageBox.Show(@"O arquivo já está aberto");
                        return;
                    }

                    MessageBox.Show(@"Erro em dgv_CellDoubleClick " + e);
                }
            }
        }

        private void Principal_FormClosed(object sender, FormClosedEventArgs e)
        {
            DeleteFiles();
            if (AtualizarVersao.Length != 0)
            {
                try
                {
                    Process.Start(Utils.path()+"\\Atualizador.exe");
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception);
                   
                }
            }
        }
        
        private void chkMarcar_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < dgv.Rows.Count; i++)
            {
                if (chkMarcar.Checked)
                {
                    dgv.Rows[i].Cells["EXPORTAR"].Value = true;
                }
                else
                {
                    dgv.Rows[i].Cells["EXPORTAR"].Value = null;
                }
            }
        }

        private void dgv_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Tab && dgv.CurrentCell.ColumnIndex == 1)
            {
                e.Handled = true;
                DataGridViewCell cell = dgv.Rows[_row].Cells[_col];
                dgv.CurrentCell = cell;
                dgv.BeginEdit(true);
            }
        }

        private void dgv_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            //dgv.EndEdit();
        }

        private void novoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DeleteFiles();
            dgv.Rows.Clear();
            dgv.Refresh();
            rtfTitulo.Text = @"Engeselt Engenharia e Serviços Elétricos LTDA";
            rtfTitulo.Refresh();
            rtfRelatorio.Text = "";
            rtfRelatorio.Refresh();
            pictureBox1.Image = Resources.Engeselt3;
            pictureBox1.Refresh();
            _filesExc = "";
        }

        private void btnOrganizar_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dgv.Rows.Count; i++)
            {
                dgv.Rows[i].Cells["DATA"].Value = dgv.Rows[i].Cells["DATA"].Value.ToString();
            }

            dgv.Sort(dgv.Columns["DATA"], ListSortDirection.Descending);
            int c = 0;
            for (int i = dgv.Rows.Count - 1; i >= 0; i--)
            {
                c++;
                dgv.Rows[i].Cells["ID"].Value = c;
            }

            dgv.Sort(dgv.Columns["DATA"], ListSortDirection.Ascending);
        }

        private void testeToolStripMenuItem_Click(object sender, EventArgs e)
        {

           

            var macAddr =
            (
                from nic in NetworkInterface.GetAllNetworkInterfaces()
                where nic.OperationalStatus == OperationalStatus.Up
                select nic.GetPhysicalAddress().ToString()
            ).FirstOrDefault();

           
        }
      
        private void dgv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            dgv.EndEdit();
        }

        private void dgv_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgv_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            var r = e.RowIndex;
            var c = e.ColumnIndex;
            if (dgv.Columns[c].Name == "ID")
            {
                try
                {
                    int res = Convert.ToInt32(dgv.Rows[r].Cells[c].Value);
                }
                catch (Exception exception)
                {
                    MessageBox.Show(@"Apenas números", @"Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    dgv.CancelEdit();
                    dgv.Rows[r].Cells[c].Value = 0;
                    return;
                }
            }

            if (dgv.Columns[c].Name == "LAT" || dgv.Columns[c].Name == "LNG")
            {
                if (dgv.Rows[r].Cells[c].Value == null || (string) dgv.Rows[r].Cells[c].Value == "")
                {
                    return;
                }

                try
                {
                    double res = Convert.ToDouble(dgv.Rows[r].Cells[c].Value);
                }
                catch (Exception)
                {
                    MessageBox.Show(@"Apenas números", @"Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    dgv.CancelEdit();
                    dgv.Rows[r].Cells[c].Value = null;
                    return;
                }
            }

            dgv.EndEdit();
        }

        private void adicionarLogoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    var of = new OpenFileDialog();
            //    of.Multiselect = true;
            //    of.Filter = @"Imagens | *.jpg; *.jpeg; *.jpe; *.jfif; *.png; *.img";
            //    of.Title = @"Selecione as imagens";
            //    var res = of.ShowDialog();
            //    if (res != DialogResult.OK)
            //    {
            //        return;
            //    }



            //    int NextID;

            //    for (int i = 0; i < of.FileNames.Length; i++)
            //    {
            //        FileInfo fi = new FileInfo(of.FileNames[i]);
            //        NextID = NextId();
            //        dgv.Rows.Add();
            //        dgv.Rows[dgv.Rows.Count - 1].Cells["ID"].Value = NextID;
            //        dgv.Rows[dgv.Rows.Count - 1].Cells["CAMINHO"].Value = fi.FullName;
            //        _img = Image.FromFile(fi.FullName);
            //        _thumb = _img.GetThumbnailImage(100, 100, null, IntPtr.Zero);
            //        dgv.Rows[dgv.Rows.Count - 1].Cells["FOTO"].Value = _thumb;

            //        //dgv.Rows[dgv.Rows.Count - 1].Cells["IMAGEM"].Value = Image.FromFile(dgv.Rows[dgv.Rows.Count - 1].Cells["CAMINHO"].Value.ToString());

            //        DateTime dt = DataFotos.GetDateTakenFromImage(fi.FullName);
            //        if (dt.Year == 1900)
            //        {
            //            dt = fi.LastWriteTime;
            //        }

            //        dgv.Rows[dgv.Rows.Count - 1].Cells["DATA"].Value = dt;
            //        _img.Dispose();
            //        GC.Collect();
            //    }

            //    Sort();
            //}
            //catch (Exception exception)
            //{

            //    throw new Exception("Erro em adicionarLogoToolStripMenuItem_Click " + exception);
            //}
        }

        private void Principal_FormClosing(object sender, FormClosingEventArgs e)
        {
            lblMsg.Text = @"Aguarde Finalizando";
            lblMsg.Refresh();
        }

        private void DeleteFiles()
        {
            try
            {
                var path = Utils.path();
                var files = Directory.GetFiles(path, "FT_*.JPG");
                foreach (string s in files)
                {
                    File.Delete(s);
                }

                if (_filesExc.Length != 0)
                {
                    files = Directory.GetFiles(path, _filesExc + "_*.BTS");
                    foreach (string s in files)
                    {
                        try
                        {
                            File.Delete(s);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e);

                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception("Erro em deleteFiles " + e);
            }

        }



        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            try
            {
                var of = new OpenFileDialog();
                of.Multiselect = false;
                of.Filter = @"Imagens | *.jpg; *.jpeg; *.jpe; *.jfif; *.png; *.img";
                of.Title = @"Selecione a imagen";
                var res = of.ShowDialog();
                if (res != DialogResult.OK)
                {
                    return;
                }

                pictureBox1.Image = new Bitmap(Image.FromFile(of.FileName));
                pictureBox1.Tag = new FileInfo(of.FileName).Extension.ToUpper().Replace(".", "");
                pictureBox1.Refresh();
            }
            catch (Exception exception)
            {

                throw new Exception("Erro em toolStripButton1_Click " + exception);
            }
        }

        private void dgv_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void chkVertical_CheckedChanged(object sender, EventArgs e)
        {
            if (chkVertical.Checked)
            {
                nudQtd.Value = 2;
                nudQtd.Enabled = false;
            }
            else
            {
                nudQtd.Enabled = true;
            }
        }

        private void chkMostrarCoordenadas_Click(object sender, EventArgs e)
        {
            chkMostrarCoordenadas.Checked = !chkMostrarCoordenadas.Checked;
            MostrarCoordenadas = chkMostrarCoordenadas.Checked;
        }

        private void chkMostrarDataHora_Click(object sender, EventArgs e)
        {
            chkMostrarDataHora.Checked = !chkMostrarDataHora.Checked;
            MostrarDataHora = chkMostrarDataHora.Checked;
        }

        private string Ch(out string info)
        {
            string cpuInfo = string.Empty;
           
            ManagementClass mc = new ManagementClass("win32_processor");
            ManagementObjectCollection moc = mc.GetInstances();

            foreach (var o in moc)
            {
                var mo = (ManagementObject) o;
                if (cpuInfo == "")
                {
                    cpuInfo = mo.Properties["processorID"].Value + "-" +
                              mo.Properties["SystemName"].Value;
                    break;
                }
            }

            info = cpuInfo;
            return KeyGen.Encrypt(cpuInfo,"engeselt");
        }
        private void Vid()
        {


            string txt;
            //msg.Text = "Conectando ao FTP";
            //msg.Refresh();
            var url = "http://"+Program.FTP_PATH+"/pdflista.csv";
            HttpWebRequest myWebRequest = (HttpWebRequest) WebRequest.Create(url);
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
                streamReader.Dispose();
            }
            catch (Exception)
            {
                lblMsg.Text = ""; lblMsg.Refresh();
                return;
            }
           
            txt = txt.Replace(Environment.NewLine, "¨");

            var head = new List<string>();


            //msg.Text = "Carregando dados";
            //msg.Refresh();
            string ch;
            var ano = "";
            var mes = "";
            var dia = "";

            string ch1;
            Ch(out ch1);
            bool ok = false;
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
                    for (int j = 0; j < head.Count; j++)
                    {
                        if (head[j] == "CHAVE")
                        {
                            ch= txt.Split('¨')[i].Split(';')[j];
                            ch = KeyGen.Decrypt(ch, "engeselt");
                            if (ch == ch1)
                            {
                                ok = true;
                            }
                        }

                        if (head[j] == "ANO")
                        {
                            ano = txt.Split('¨')[i].Split(';')[j];
                        }

                        if (head[j] == "MES")
                        {
                            mes = txt.Split('¨')[i].Split(';')[j];
                        }
                        if (head[j] == "DIA")
                        {
                            dia = txt.Split('¨')[i].Split(';')[j];
                        }
                    }

                    if (ok)
                    {
                        break;
                    }
                }
            }

            //msg.Text = "Download Concluído com sucesso!";
            //msg.Refresh();
           
            //if (!ok)
            //{
            //    if (MessageBox.Show(@"Produto não registrado, deseja registrar agora ?", @"Registro",
            //            MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            //    {
            //        Close();
            //        return;
            //    }
            //    frmRegitro frm = new frmRegitro();
            //    frm.ShowDialog(this);
            //    Close();
            //}


            var dtval = new DateTime(int.Parse(ano), int.Parse(mes), int.Parse(dia));
            var dtatu = Utils.GetNistTime();

          

            if (dtatu > dtval)
            {
                MessageBox.Show(@"Validade do programa vencida, entre em contato com Engeselt Software fornecendo a chave do produto", @"ATENÇÃO",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Close();

            }

          

            int daysLeftInYear = (int)(dtval - dtatu).TotalDays;
            lblMsg.Text = ""; lblMsg.Refresh();
            if (daysLeftInYear < 10)
            {
                MessageBox.Show(@"Falta(m) " + daysLeftInYear + @" dia(s) para vencimento do programa\nEntre em contato com Engeselt Software fornecendo a chave do produto para nova validação ", @"ATENÇÃO",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

          
        }

        private void chaveDoProdutoToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void copiarParaAÁreaDeTransferênciaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string cc;
            var ch = Ch(out cc);
            Clipboard.SetText(ch);
            MessageBox.Show(ch + @"Copiado para área de transferência", @"Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void tmrVersao_Tick(object sender, EventArgs e)
        {
            tmrVersao.Enabled = false;
            ChecarVersao();
        }
       
        #region rotacionar
        private void defaultToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Rotacionar_thumb(0);
        }

        private void btnR90_Click(object sender, EventArgs e)
        {
            Rotacionar_thumb(90);
        }

        private void btnR180_Click(object sender, EventArgs e)
        {
            Rotacionar_thumb(180);
        }

        private void btnR_90_Click(object sender, EventArgs e)
        {
            Rotacionar_thumb(270);
        }


        private void Rotacionar_thumb(int angulo)
        {
            if (_row==-1)
            {
                MessageBox.Show("Nenhum item selecionado!");
                return;
            }
            var arq = dgv.Rows[_row].Cells["CAMINHO"].Value.ToString();
            _img = Image.FromFile(arq);
            _thumb = _img.GetThumbnailImage(100, 100, null, IntPtr.Zero);

            foreach (var prop in _img.PropertyItems)
            {
                if (prop.Id == 0x112)
                {
                    if (prop.Value[0] == 6)
                    {
                        //rotate = 90;
                        _thumb.RotateFlip(RotateFlipType.Rotate90FlipNone);
                        break;
                    }

                    if (prop.Value[0] == 8)
                    {
                        //rotate = -90;
                        _thumb.RotateFlip(RotateFlipType.Rotate270FlipNone);
                        break;
                    }

                    if (prop.Value[0] == 3)
                    {
                        //rotate = 180;
                        _thumb.RotateFlip(RotateFlipType.Rotate180FlipNone);
                        break;
                    }

                    //prop.Value[0] = 1;
                }
            }

            if (angulo == 90)
            {
                if (dgv.Rows[_row].Cells["ROTACAO"].Value == null)
                {
                    dgv.Rows[_row].Cells["ROTACAO"].Value = 0;
                }
                if(dgv.Rows[_row].Cells["ROTACAO"].Value.ToString() == "90")
                {
                    return;
                }
                dgv.Rows[_row].Cells["ROTACAO"].Value = "90";
                _thumb.RotateFlip(RotateFlipType.Rotate90FlipNone);
            }
            else if (angulo == 180)
            {
                if (dgv.Rows[_row].Cells["ROTACAO"].Value == null)
                {
                    dgv.Rows[_row].Cells["ROTACAO"].Value = 0;
                }
                if (dgv.Rows[_row].Cells["ROTACAO"].Value.ToString() == "180")
                {
                    return;
                }
                dgv.Rows[_row].Cells["ROTACAO"].Value = "180";
                _thumb.RotateFlip(RotateFlipType.Rotate180FlipNone);
            }
            else if (angulo == 270)
            {
                if (dgv.Rows[_row].Cells["ROTACAO"].Value == null)
                {
                    dgv.Rows[_row].Cells["ROTACAO"].Value = 0;
                }
                if (dgv.Rows[_row].Cells["ROTACAO"].Value.ToString() == "270")
                {
                    return;
                }
                dgv.Rows[_row].Cells["ROTACAO"].Value = "270";
                _thumb.RotateFlip(RotateFlipType.Rotate270FlipNone);
            }
            else
            {
                dgv.Rows[_row].Cells["ROTACAO"].Value = "0";
            }
            dgv.Rows[_row].Cells["FOTO"].Value = _thumb;
            _img.Dispose();
            GC.Collect();
        }
        #endregion

        private void toolStripDropDownButton1_Click(object sender, EventArgs e)
        {

        }
    }
}

