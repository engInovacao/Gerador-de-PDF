using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;
using iText.IO.Font;
using iText.IO.Font.Constants;
using iText.IO.Image;
using iText.Kernel.Colors;
using iText.Kernel.Events;
using iText.Kernel.Font;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Draw;
using iText.Kernel.Pdf.Xobject;
using iText.Layout;
using iText.Layout.Borders;
using iText.Layout.Element;
using iText.Layout.Properties;
using iText.Layout.Renderer;
using Color = iText.Kernel.Colors.Color;
using HorizontalAlignment = iText.Layout.Properties.HorizontalAlignment;
using Image = iText.Layout.Element.Image;


namespace PDF.Classes
{
    public class Exportar
    {
        private static int paginas = 0;
        private static int qtd = 0;
        private static Principal pp;
        private static Document doc;
        public string fileName = "";
        private bool NovaPagina = false;
        public static int totPag = 0;
        private int totVezes = 1;


        public Exportar(Principal ppg, string filename, decimal qtds)
        {
            int erros = 0;
            try
            {
                fileName = filename;
                int totproc = 0;
               
                for (int i = 0; i < ppg.dgv.Rows.Count; i++)
                {
                    if (ppg.dgv.Rows[i].Cells["EXPORTAR"].Value == null)
                    {
                        continue;
                    }

                    if ((bool) ppg.dgv.Rows[i].Cells["EXPORTAR"].Value == false)
                    {
                        continue;
                    }

                    totproc++;
              
                }

                if (totproc == 0)
                {
                    MessageBox.Show("Nenum Item Selecuinado!");
                    return;
                }
                pp = ppg;
                qtd = int.Parse(qtds.ToString());
                paginas = 0;
                erros = 1;
                PdfDocument pdfDoc = new PdfDocument(new PdfWriter(filename));
                erros = 2;
                doc = new Document(pdfDoc, PageSize.A4);
                if (qtd > 1)
                {
                    Cab();
                }
                pdfDoc.AddEventHandler(PdfDocumentEvent.END_PAGE, new rodape());
                //pdfDoc.AddEventHandler(PdfDocumentEvent.START_PAGE, new cabechalho());


                int tot = 1;
         
                ppg.pgb.Value = 0;
                ppg.pgb.Maximum = pp.dgv.Rows.Count;
                ppg.lblMsg.Text = "Gerando arquivo"; ppg.lblMsg.Refresh();
                totPag = 0;
                NovaPagina = false;
                for (int i = 0; i < pp.dgv.Rows.Count; i++)
                {
             
                    if (pp.dgv.Rows[i].Cells["EXPORTAR"].Value == null)
                    {
                        ppg.pgb.Value++;
                        continue;
                    }

                    if ((bool) pp.dgv.Rows[i].Cells["EXPORTAR"].Value == false)
                    {
                        ppg.pgb.Value++;
                        continue;
                    }

                    if (qtd == 1)
                    {
                        Cab();
                    }

                    if (ppg.chkVertical.Checked)
                    {
                        CorpoVertical(i, pp);
                    }
                    else
                    {
                        CorpoHorizontal(i, pp);
                    }
                    //
                    ppg.pgb.Value++;
                }

                ppg.pgb.Value = 0;
                if (qtd == 1)
                {
                    try
                    {
                        pdfDoc.RemovePage(totPag+1);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                   
                    }
                }
                else
                {
                    if (NovaPagina)
                    {
                        try
                        {
                            pdfDoc.RemovePage(totPag+1);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e);

                        }
                    }
                }
                ppg.lblMsg.Text = "Gravando o arquivo"; ppg.lblMsg.Refresh();
                doc.Close();
                ppg.lblMsg.Text = ""; ppg.lblMsg.Refresh();
                MessageBox.Show("Processo concluído!");

            }
            catch (Exception e)
            {
                var arq = "";
                if (erros == 1)
                {
                    arq = filename;
                    MessageBox.Show(
                        "Não foi possível gerar o arquivo " + arq + "\n\n" +
                        "Verifique o caminho correto do arquivo e verifique se você tem permissão para gravar na pasta\n\n" +
                        e.ToString(), "ERRO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;

                }
                else
                {
                    throw new Exception("Erro criando PDF " + e.ToString());
                }
                
            }
        }

        private class cabechalho : IEventHandler
        {
            public void HandleEvent(Event currentEvent)
            {
                PdfDocumentEvent docEvent = (PdfDocumentEvent) currentEvent;
                if (currentEvent.GetEventType() == "StartPdfPage")
                {
                    Cab();
                }
            }
        }

        private class rodape : IEventHandler
        {
            public void HandleEvent(Event currentEvent)
            {
                PdfDocumentEvent docEvent = (PdfDocumentEvent)currentEvent;
                iText.Kernel.Geom.Rectangle pageSize = docEvent.GetPage().GetPageSize();
                PdfFont font = null;
                try
                {
                    font = PdfFontFactory.CreateFont(StandardFonts.HELVETICA_OBLIQUE);

                }
                catch (IOException e)
                {

                    // Such an exception isn't expected to occur,
                    // because helvetica is one of standard fonts
                    Console.Error.WriteLine(e.Message);
                }

                float coordX = ((pageSize.GetLeft() + doc.GetLeftMargin())
                                + (pageSize.GetRight() - doc.GetRightMargin())) / 2;
                float headerY = pageSize.GetTop() - doc.GetTopMargin() + 10;
                float footerY = doc.GetBottomMargin()-35;
                paginas++;

                Canvas canvas = new Canvas(docEvent.GetPage(), pageSize);
                canvas

                    // If the exception has been thrown, the font variable is not initialized.
                    // Therefore null will be set and iText will use the default font - Helvetica
                    .SetFont(font)
                    .SetFontSize(15)
                    //.ShowTextAligned("this is a header", coordX, headerY, TextAlignment.CENTER)

                    .ShowTextAligned(paginas.ToString(), coordX, footerY, TextAlignment.CENTER)

                    .Close();
            }
        }
        
        private static void Cab()
        {
            LineSeparator ls = new LineSeparator(new SolidLine());


            float[] columnWidths = { 40, 160 };
            //Paragraph header = new Paragraph("HEADER")
            //    .SetTextAlignment(TextAlignment.CENTER)
            //    .SetFontSize(20);
            //document.Add(header);



            Table table = new Table(UnitValue.CreatePercentArray(columnWidths));

            //Cell c1 = new Cell(0, 0).Add(new Paragraph("State"));

            if (pp.pictureBox1.Image == null)
            {
                //pp.pictureBox1.Image=new Bitmap(500,500);
               



                pp.pictureBox1.Image = new Bitmap(500, 500);
                using (Graphics gfx = Graphics.FromImage(pp.pictureBox1.Image))
                using (SolidBrush brush = new SolidBrush(System.Drawing.Color.White))
                {
                    gfx.FillRectangle(brush, 0, 0, 500, 500);
                }


            }
            System.Drawing.Image image = new Bitmap(pp.pictureBox1.Image);


            byte[] b;
            using (MemoryStream ms = new MemoryStream())
            {
                if (pp.pictureBox1.Tag == null || pp.pictureBox1.Tag.ToString().Length == 0)
                {
                    pp.pictureBox1.Tag = "BMP";
                }
                ImageFormat imf = null; //= System.Drawing.Imaging.ImageFormat.Png;
                var imgtype = pp.pictureBox1.Tag.ToString();
                if (imgtype=="PNG")
                {
                    imf = System.Drawing.Imaging.ImageFormat.Png;
                }
                else if (imgtype == "BMP")
                {
                    imf = System.Drawing.Imaging.ImageFormat.Bmp;
                }
                else if (imgtype == "EMF")
                {
                    imf = System.Drawing.Imaging.ImageFormat.Emf;
                }
                else if (imgtype == "EXIF")
                {
                    imf = System.Drawing.Imaging.ImageFormat.Exif;
                }
                else if (imgtype == "GIF")
                {
                    imf = System.Drawing.Imaging.ImageFormat.Gif;
                }
                else if (imgtype == "ICON")
                {
                    imf = System.Drawing.Imaging.ImageFormat.Icon;
                }
                else if (imgtype == "JPG")
                {
                    imf = System.Drawing.Imaging.ImageFormat.Jpeg;
                }
                else if (imgtype == "JPEG")
                {
                    imf = System.Drawing.Imaging.ImageFormat.Jpeg;
                }
                else if (imgtype == "TIFF")
                {
                    imf = System.Drawing.Imaging.ImageFormat.Tiff;
                }
                else if (imgtype == "TIF")
                {
                    imf = System.Drawing.Imaging.ImageFormat.Tiff;
                }
                else if (imgtype == "WMF")
                {
                    imf = System.Drawing.Imaging.ImageFormat.Wmf;
                }
                else
                {
                    throw  new Exception("Tipo de imagem do logo não suportada");
                }

                image.Save(ms, imf);
                b = ms.ToArray();
            }


            ImageData imageDataEng = ImageDataFactory.Create(b);
            //Cell c1 = new Cell().Add(new Image(imageDataEng).ScaleAbsolute(100, 75));
            Cell c1 = new Cell().Add(new Image(imageDataEng).SetAutoScale(true));
            table.AddCell(c1);


            var title = new Text(Environment.NewLine + pp.rtfTitulo.Text + Environment.NewLine + " ").SetFontSize(20);
            Paragraph p1 = new Paragraph();
            p1.Add(title);
            p1.SetTextAlignment(TextAlignment.CENTER);

            Cell c2 = new Cell().Add(p1);

            c2.Add(ls);


            title = new Text(pp.rtfRelatorio.Text).SetFontSize(12);
            Paragraph p2 = new Paragraph();
            p2.Add(title);
            p2.SetTextAlignment(TextAlignment.CENTER);
            c2.Add(p2);
            table.AddCell(c2);

            doc.Add(table);
            totPag++;
        }
        
        private void CorpoHorizontal(int posicao, Principal pp)
        {
            PdfFont bold = PdfFontFactory.CreateFont(FontConstants.TIMES_BOLD);
            Text title;
            Cell cell;
            float[] columnWidths = { 10, 1, 70 };
            Table table = new Table(UnitValue.CreatePercentArray(columnWidths));
            table.SetFixedLayout();

            if (clsUtils.éNuloOuVazio(pp.dgv.Rows[posicao].Cells["CAMINHO"].Value))
            {
                // throw new Exception("Erro em CorpoHorizontal, arquivo não existe ");
                MessageBox.Show(
                    "CH - O programa perdeu o caminho da imgem ID ("+ pp.dgv.Rows[posicao].Cells["ID"].Value + "), por favor remova e inclua a imagem novamente");
                return;
                
            }
            System.Drawing.Image drawImage = new Bitmap(pp.dgv.Rows[posicao].Cells["CAMINHO"].Value.ToString());
            if (!File.Exists(pp.dgv.Rows[posicao].Cells["CAMINHO"].Value.ToString()))
            {
                throw new Exception("Erro em CorpoHorizontal, arquivo não existe "+ pp.dgv.Rows[posicao].Cells["CAMINHO"].Value.ToString());
            }
            FileInfo fi = new FileInfo(pp.dgv.Rows[posicao].Cells["CAMINHO"].Value.ToString());
            var arq = "";
            fi = new FileInfo(fileName);
            arq = fi.Name.Replace(fi.Extension, "") + "_" + pp.dgv.Rows[posicao].Cells["ID"].Value.ToString() + ".JPG";
            try
            {
                drawImage.Save(fi.DirectoryName + "\\" + arq, ImageFormat.Jpeg);
            }
            catch (Exception e)
            {
                throw new Exception("Erro em CorpoHorizontal, tentando salvar arquivo " + fi.DirectoryName + "\\" +
                                    arq);
            }

            title = new Text(pp.dgv.Rows[posicao].Cells["ID"].Value.ToString()).SetFont(bold).SetFontSize(12);
            Paragraph p = new Paragraph();
            p.Add(title);
            p.SetTextAlignment(TextAlignment.CENTER);
            cell = new Cell(0, 0).Add(p).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetHorizontalAlignment(HorizontalAlignment.CENTER);
            table.AddCell(cell);

            float rotate = 0f;
            float rotateRad = 0f;
            foreach (var prop in drawImage.PropertyItems)
            {
                if (prop.Id == 0x112)
                {
                    if (prop.Value[0] == 6)
                    {
                        rotate = 90;
                        rotateRad = -1.5708f;
                    }

                    if (prop.Value[0] == 8)
                    {
                        rotate = -90;
                        rotateRad= 1.5708f;
                    }

                    if (prop.Value[0] == 3)
                    {
                        rotate = 180;
                        rotateRad = 3.14159f;
                    }
                    //prop.Value[0] = 1;
                }
            }

            ImageData imageData = ImageDataFactory.Create(ImageToByteArray(drawImage));

            //imageData.SetRotation(rotate);
            //cell = new Cell(0, 1).Add(new Image(imageData).ScaleAbsolute(150,150)).SetWidth(100).SetHeight(150);
            float v = (150.0f * 4.0f);


            if (qtd > 5)
            {
                v = v / (qtd - 1);
            }
            else if (qtd == 5)
            {
                v = (700);
                v = v / qtd;
            }
            else
            {
                v = v / qtd;
            }

            if (qtd == 1)
            {
                v = 350;
            }
            //cell = new Cell(0, 1).Add(new Image(imageData).ScaleToFit(v, v).SetRotationAngle(rotateRad));
            var nimg = new Image(imageData).ScaleAbsolute(v, v).SetRotationAngle(rotateRad);
            if (!clsUtils.éNuloOuVazio(pp.dgv.Rows[posicao].Cells["ROTACAO"].Value))
            {
                if (pp.dgv.Rows[posicao].Cells["ROTACAO"].Value.ToString() == "90")
                {
                    nimg = new Image(imageData).ScaleAbsolute(v, v).SetRotationAngle(rotateRad+ (-1.5708f));
                }
            }
            if (!clsUtils.éNuloOuVazio(pp.dgv.Rows[posicao].Cells["ROTACAO"].Value))
            {
                if (pp.dgv.Rows[posicao].Cells["ROTACAO"].Value.ToString() == "180")
                {
                    nimg = new Image(imageData).ScaleAbsolute(v, v).SetRotationAngle(rotateRad + 3.14159);
                }
            }
            if (!clsUtils.éNuloOuVazio(pp.dgv.Rows[posicao].Cells["ROTACAO"].Value))
            {
                if (pp.dgv.Rows[posicao].Cells["ROTACAO"].Value.ToString() == "270")
                {
                    nimg = new Image(imageData).ScaleAbsolute(v, v).SetRotationAngle(rotateRad+ 1.5708f);
                }
            }
            cell = new Cell(0, 1).Add(nimg);
            table.AddCell(cell);

            var texto = "";
            if (clsUtils.éNuloOuVazio(pp.dgv.Rows[posicao].Cells["DESCRICAO"].Value))
            {
                pp.dgv.Rows[posicao].Cells["DESCRICAO"].Value = "";
            }
            if (clsUtils.éNuloOuVazio(pp.dgv.Rows[posicao].Cells["LAT"].Value))
            {
                pp.dgv.Rows[posicao].Cells["LAT"].Value = "";
            }
            if (clsUtils.éNuloOuVazio(pp.dgv.Rows[posicao].Cells["LNG"].Value))
            {
                pp.dgv.Rows[posicao].Cells["LNG"].Value = "";
            }

            if (!clsUtils.éNuloOuVazio(pp.dgv.Rows[posicao].Cells["LAT"].Value) )
            {
                if (pp.MostrarCoordenadas)
                {
                    texto += "X=" + pp.dgv.Rows[posicao].Cells["LAT"].Value + " " + Environment.NewLine;
                }
            }

            if (!clsUtils.éNuloOuVazio(pp.dgv.Rows[posicao].Cells["LNG"].Value))
            {
                if (pp.MostrarCoordenadas)
                {
                    texto += "Y=" + pp.dgv.Rows[posicao].Cells["LNG"].Value + " " + Environment.NewLine;
                }
            }

            if (pp.MostrarDataHora)
            {
                if (!clsUtils.éNuloOuVazio(pp.dgv.Rows[posicao].Cells["DATA"].Value))
                {
                    texto += pp.dgv.Rows[posicao].Cells["DATA"].Value.ToString() + Environment.NewLine;
                }
            }

            if (!clsUtils.éNuloOuVazio(pp.dgv.Rows[posicao].Cells["DESCRICAO"].Value))
            {
                texto += pp.dgv.Rows[posicao].Cells["DESCRICAO"].Value.ToString();
            }

            title = new Text(texto).SetFont(bold).SetFontSize(12);
            p = new Paragraph();
            p.Add(title);
            p.SetTextAlignment(TextAlignment.JUSTIFIED).SetMarginLeft(10);
            cell = new Cell(0, 2).Add(p).SetVerticalAlignment(VerticalAlignment.MIDDLE);
            table.AddCell(cell);
            doc.Add(table);
            table = new Table(UnitValue.CreatePercentArray(columnWidths));
            table.SetFixedLayout();


            drawImage.Dispose();
          
            GC.Collect();


            doc.Add(table);
            NovaPagina = false;

            if (qtd == 1)
            {
                doc.Add(new AreaBreak(AreaBreakType.NEXT_PAGE));
                NovaPagina = true;
                return;
            }

          
            if (totVezes == qtd)
            {
                doc.Add(new AreaBreak(AreaBreakType.NEXT_PAGE));
                NovaPagina = true;
                Cab();
                totVezes = 1;
                return;
            }

            totVezes++;
        }

        private void CorpoVertical(int posicao, Principal pp)
        {
            //Table table = new Table(UnitValue.CreatePercentArray(2)).UseAllAvailableWidth();
            Table table1 = new Table(UnitValue.CreatePercentArray(1)).UseAllAvailableWidth();
            Table table2 = new Table(UnitValue.CreatePercentArray(1)).UseAllAvailableWidth();
            //Text author = new Text("Robert Louis Stevenson");
            //Paragraph p = new Paragraph();
            //p.Add(author);
            //p.SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER);
            //doc.Add(p);
            Cell cell = new Cell();

            //cell.Add(new Paragraph("TESTE"));
            LineSeparator ls = new LineSeparator(new SolidLine());
            //cell.Add(ls);
            //cell.Add(new Paragraph("TESTE2"));
            //table.AddCell(cell);
            //doc.Add(table);


            if (clsUtils.éNuloOuVazio(pp.dgv.Rows[posicao].Cells["CAMINHO"].Value))
            {
                MessageBox.Show(
                    "CV - O programa perdeu o caminho da imgem ID (" + pp.dgv.Rows[posicao].Cells["ID"].Value + "), por favor remova e inclua a imagem novamente");
                return;

            }

            System.Drawing.Image drawImage = new Bitmap(pp.dgv.Rows[posicao].Cells["CAMINHO"].Value.ToString());
            if (!File.Exists(pp.dgv.Rows[posicao].Cells["CAMINHO"].Value.ToString()))
            {
                throw new Exception("Erro em CorpoVertical, arquivo não existe "+ pp.dgv.Rows[posicao].Cells["CAMINHO"].Value.ToString());
            }
            FileInfo fi = new FileInfo(pp.dgv.Rows[posicao].Cells["CAMINHO"].Value.ToString());
            var arq = "";
            fi = new FileInfo(fileName);
            arq = fi.Name.Replace(fi.Extension, "") + "_" + pp.dgv.Rows[posicao].Cells["ID"].Value.ToString() + ".JPG";
            try
            {
                drawImage.Save(fi.DirectoryName + "\\" + arq, ImageFormat.Jpeg);
            }
            catch (Exception e)
            {
                throw new Exception("Erro em CorpoVertical, salvando arquivo " + fi.DirectoryName + "\\" + arq);
            }
            ImageData imageData = ImageDataFactory.Create(ImageToByteArray(drawImage));

            float rotate = 0f;
            float rotateRad = 0f;
            foreach (var prop in drawImage.PropertyItems)
            {
                if (prop.Id == 0x112)
                {
                    if (prop.Value[0] == 6)
                    {
                        rotate = 90;
                        rotateRad = -1.5708f;
                    }

                    if (prop.Value[0] == 8)
                    {
                        rotate = -90;
                        rotateRad = 1.5708f;
                    }

                    if (prop.Value[0] == 3)
                    {
                        rotate = 180;
                        rotateRad = 3.14159f;
                    }
                    //prop.Value[0] = 1;
                }
            }



            //cell.Add(new Image(imageData).SetAutoScale(true).SetRotationAngle(rotateRad).SetHorizontalAlignment(HorizontalAlignment.CENTER)).SetWidth(200).SetHeight(400);

            var nimg = new Image(imageData).ScaleAbsolute(200, 400).SetRotationAngle(rotateRad);
            if (pp.dgv.Rows[posicao].Cells["ROTACAO"].Value != null)
            {
                if (pp.dgv.Rows[posicao].Cells["ROTACAO"].Value.ToString() == "90")
                {
                    //cell.Add(new Image(imageData).SetAutoScale(true).SetRotationAngle(rotateRad + (-1.5708f)).SetHorizontalAlignment(HorizontalAlignment.CENTER)).SetWidth(200).SetHeight(400);
                    nimg = new Image(imageData).SetAutoScale(true).SetRotationAngle(rotateRad + (-1.5708f))
                        .SetHorizontalAlignment(HorizontalAlignment.CENTER);
                }
            }
            else if (pp.dgv.Rows[posicao].Cells["ROTACAO"].Value != null)
            {
                if (pp.dgv.Rows[posicao].Cells["ROTACAO"].Value.ToString() == "180")
                {
                    //cell.Add(new Image(imageData).SetAutoScale(true).SetRotationAngle(rotateRad + 3.14159).SetHorizontalAlignment(HorizontalAlignment.CENTER)).SetWidth(200).SetHeight(400);
                    nimg = new Image(imageData).SetAutoScale(true).SetRotationAngle(rotateRad + 3.14159)
                        .SetHorizontalAlignment(HorizontalAlignment.CENTER);
                }
            }
            else if (pp.dgv.Rows[posicao].Cells["ROTACAO"].Value != null)
            {
                if (pp.dgv.Rows[posicao].Cells["ROTACAO"].Value.ToString() == "270")
                {
                    //cell.Add(new Image(imageData).SetAutoScale(true).SetRotationAngle(rotateRad + 1.5708f).SetHorizontalAlignment(HorizontalAlignment.CENTER)).SetWidth(200).SetHeight(400);
                    nimg = new Image(imageData).SetAutoScale(true).SetRotationAngle(rotateRad + 1.5708f)
                        .SetHorizontalAlignment(HorizontalAlignment.CENTER);
                }
            }
            else
            {
                cell.Add(nimg).SetWidth(200).SetHeight(400);
            }

            //cell.Add(ls);
            //cell.Add(new Paragraph("TESTE2"));
            table1.AddCell(cell);
            cell=new Cell().SetTextAlignment(TextAlignment.CENTER);
            var text0 = pp.dgv.Rows[posicao].Cells["ID"].Value.ToString() + " - ";
            if (!clsUtils.éNuloOuVazio(pp.dgv.Rows[posicao].Cells["DESCRICAO"].Value))
            {
                text0 += pp.dgv.Rows[posicao].Cells["DESCRICAO"].Value.ToString();
            }

            cell.Add(new Paragraph(text0));

            if (pp.MostrarCoordenadas)
            {
                if (!clsUtils.éNuloOuVazio(pp.dgv.Rows[posicao].Cells["LAT"].Value) ||
                    !clsUtils.éNuloOuVazio(pp.dgv.Rows[posicao].Cells["LNG"].Value))
                {
                    var text1 = pp.dgv.Rows[posicao].Cells["LAT"].Value.ToString() + "  " +
                                pp.dgv.Rows[posicao].Cells["LNG"].Value.ToString();
                    cell.Add(new Paragraph(text1));
                }
            }

            if (pp.MostrarDataHora)
            {
                if (!clsUtils.éNuloOuVazio(pp.dgv.Rows[posicao].Cells["DATA"].Value))
                {
                    var text2 = pp.dgv.Rows[posicao].Cells["DATA"].Value.ToString();
                    cell.Add(new Paragraph(text2));
                }

            }
            table2.AddCell(cell);
            doc.Add(table1);
            doc.Add(table2);

            if (qtd == 1)
            {
                doc.Add(new AreaBreak(AreaBreakType.NEXT_PAGE));
                NovaPagina = true;
                return;
            }
        }
        
        private class OverlappingImageTableRenderer : TableRenderer
        {
            private ImageData image;

            public OverlappingImageTableRenderer(Table modelElement, ImageData img)
                : base(modelElement)
            {
                image = img;
            }
        }

        public static byte[] ImageToByteArray(System.Drawing.Image imageIn)
        {
            using (var ms = new MemoryStream())
            {
                imageIn.Save(ms, imageIn.RawFormat);
                return ms.ToArray();
            }
        }

        public static System.Drawing.Image byteArrayToImage(byte[] byteArrayIn)
        {
            using (MemoryStream ms = new MemoryStream(byteArrayIn, 0, byteArrayIn.Length))
            {
                ms.Write(byteArrayIn, 0, byteArrayIn.Length);
                return System.Drawing.Image.FromStream(ms, true); 
            }

        }

        public static bool ByteArrayToFile(string fileName, byte[] byteArray)
        {
            try
            {
                using (var fs = new FileStream(fileName, FileMode.Create, FileAccess.Write))
                {
                    fs.Write(byteArray, 0, byteArray.Length);
                    return true;
                }
            }
            catch (Exception ex)
            {
               throw new Exception("Erro em ByteArrayToFile "+ex.ToString());
            }
        }

    }

   
}
