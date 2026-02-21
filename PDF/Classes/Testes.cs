using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using iText.IO.Font;
using iText.IO.Image;
using iText.Kernel.Font;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Draw;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using HorizontalAlignment = System.Windows.Forms.HorizontalAlignment;

namespace PDF.Classes
{
    class Testes
    {
        //private void ManipulatePdf2(String dest, DataGridView dgv, string titulo, int qtd)
        //{
        //    FileInfo fi = new FileInfo(dest);
        //    PdfDocument pdfDoc = new PdfDocument(new PdfWriter(dest));
        //    Document doc = new Document(pdfDoc, PageSize.A4);
        //    PdfFont bold = PdfFontFactory.CreateFont(FontConstants.TIMES_BOLD);
        //    Text title;

        //    Cell cell;
        //    //Table table = new Table(UnitValue.CreatePercentArray(3)).UseAllAvailableWidth();
        //    float[] columnWidths = { 10, 1, 70 };
        //    Table table = new Table(UnitValue.CreatePercentArray(columnWidths));
        //    table.SetFixedLayout();



        //    var engImg = (System.Drawing.Image)Properties.Resources.Engeselt2;
        //    ImageData imageDataEng = ImageDataFactory.Create(ImageToByteArray(engImg));
        //    doc.Add(new iText.Layout.Element.Image(imageDataEng));


        //    title = new Text(titulo).SetFont(bold).SetFontSize(20);
        //    Paragraph p1 = new Paragraph();
        //    //PdfFont font = PdfFontFactory.CreateFont(FontConstants.TIMES_ROMAN);
        //    //Text author = title.SetFont(font);
        //    p1.Add(title);
        //    p1.SetTextAlignment(TextAlignment.CENTER);
        //    doc.Add(p1);

        //    doc.Add(new AreaBreak(AreaBreakType.NEXT_PAGE));

        //    for (int i = 0; i < dgv.Rows.Count; i++)
        //    {

        //        //Salvar Imagem 
        //        System.Drawing.Image drawImage = (System.Drawing.Image)dgv.Rows[i].Cells["IMAGEM"].Value;
        //        var arq = fi.Name.Replace(fi.Extension, "") + "_" + dgv.Rows[i].Cells["ID"].Value.ToString() + ".JPG";
        //        drawImage.Save(fi.DirectoryName + "\\" + arq, ImageFormat.Jpeg);

        //        title = new Text(dgv.Rows[i].Cells["ID"].Value.ToString()).SetFont(bold).SetFontSize(12);
        //        Paragraph p = new Paragraph();
        //        p.Add(title);
        //        p.SetTextAlignment(TextAlignment.CENTER);
        //        cell = new Cell(0, 0).Add(p).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetHorizontalAlignment(HorizontalAlignment.CENTER);
        //        table.AddCell(cell);

        //        var mi = (System.Drawing.Image)dgv.Rows[i].Cells["IMAGEM"].Value;
        //        ImageData imageData = ImageDataFactory.Create(ImageToByteArray(mi));
        //        //cell = new Cell(0, 1).Add(new Image(imageData).ScaleAbsolute(150,150)).SetWidth(100).SetHeight(150);
        //        float v = (150.0f * 4.0f);


        //        if (qtd > 5)
        //        {
        //            v = v / (qtd - 1);
        //        }
        //        else if (qtd == 5)
        //        {
        //            v = (700);
        //            v = v / qtd;
        //        }
        //        else
        //        {
        //            v = v / qtd;
        //        }

        //        if (qtd == 1)
        //        {
        //            v = 350;
        //        }
        //        cell = new Cell(0, 1).Add(new Image(imageData).ScaleAbsolute(v, v));//.SetWidth(200).SetHeight(200);
        //        table.AddCell(cell);

        //        title = new Text(dgv.Rows[i].Cells["DESCRICAO"].Value.ToString()).SetFont(bold).SetFontSize(12);
        //        p = new Paragraph();
        //        p.Add(title);
        //        p.SetTextAlignment(TextAlignment.JUSTIFIED).SetMarginLeft(10);
        //        cell = new Cell(0, 2).Add(p).SetVerticalAlignment(VerticalAlignment.MIDDLE);
        //        table.AddCell(cell);
        //        doc.Add(table);
        //        table = new Table(UnitValue.CreatePercentArray(columnWidths));
        //        table.SetFixedLayout();
        //        if (qtd == 1)
        //        {
        //            doc.Add(new AreaBreak(AreaBreakType.NEXT_PAGE));
        //        }

        //    }
        //    doc.Add(table);
        //    doc.Close();
        //    MessageBox.Show("Processo concluído!");
        //}

        //private void teste1(DataGridView dgv, string titulo, string arq)
        //{

        //    var mi = (System.Drawing.Image)dgv.Rows[0].Cells["IMAGEM"].Value;

        //    PdfDocument pdf = new PdfDocument(new PdfWriter(arq));
        //    Document document = new Document(pdf);
        //    PdfFont font = PdfFontFactory.CreateFont(FontConstants.TIMES_ROMAN);
        //    PdfFont bold = PdfFontFactory.CreateFont(FontConstants.TIMES_BOLD);
        //    Text title = new Text("The Strange Case of Dr. Jekyll and Mr. Hyde").SetFont(bold);
        //    Text author = new Text("Robert Louis Stevenson").SetFont(font);
        //    Paragraph p = new Paragraph();//.Add(title).Add(" by ").Add(author);
        //    p.Add(title).Add(" by ").Add(author);
        //    p.SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER);
        //    document.Add(p);


        //    ImageData imageData = ImageDataFactory.Create(ImageToByteArray(mi));
        //    document.Add(new iText.Layout.Element.Image(imageData));

        //    Paragraph paragraph = new Paragraph();
        //    Image pdfImg = new Image(imageData);
        //    paragraph.Add(pdfImg);
        //    document.Add(paragraph);

        //    Table table = new Table(UnitValue.CreatePercentArray(5)).UseAllAvailableWidth();

        //    for (int r = 'A'; r <= 'Z'; r++)
        //    {
        //        for (int c = 1; c <= 5; c++)
        //        {
        //            Cell cell = new Cell();
        //            cell.Add(new Paragraph(((char)r) + c.ToString()));
        //            table.AddCell(cell);
        //        }
        //    }

        //    table.SetNextRenderer(new OverlappingImageTableRenderer(table, imageData));
        //    document.Add(table);


        //    document.Close();
        //}

        //private void ManipulatePdf(String dest, DataGridView dgv, string titulo, string relatorio, int qtd)
        //{
        //    //FileInfo fi = new FileInfo(dest);
        //    //PdfDocument pdfDoc = new PdfDocument(new PdfWriter(dest));
        //    //Document doc = new Document(pdfDoc, PageSize.A4);

        //    //float[] columnWidths = { 1, 5, 5 };
        //    //Table table = new Table(UnitValue.CreatePercentArray(columnWidths));

        //    //PdfFont f = PdfFontFactory.CreateFont(StandardFonts.HELVETICA);
        //    //Cell cell = new Cell(1, 3)
        //    //    .Add(new Paragraph("Este é o cabeçalho"))
        //    //    .SetFont(f)
        //    //    .SetFontSize(13)
        //    //    .SetFontColor(DeviceGray.WHITE)
        //    //    .SetBackgroundColor(DeviceGray.BLACK)
        //    //    .SetTextAlignment(TextAlignment.CENTER);

        //    //table.AddHeaderCell(cell);


        //    //doc.Add(table);

        //    //doc.Close();


        //    PdfDocument pdfDoc = new PdfDocument(new PdfWriter(dest));
        //    Document document = new Document(pdfDoc, PageSize.A4);


        //    //Paragraph header = new Paragraph("HEADER")
        //    //    .SetTextAlignment(TextAlignment.CENTER)
        //    //    .SetFontSize(20);


        //    //document.Add(header);

        //    LineSeparator ls = new LineSeparator(new SolidLine());
        //    //document.Add(ls);

        //    float[] columnWidths = { 10, 80 };
        //    Table table = new Table(UnitValue.CreatePercentArray(columnWidths));

        //    Cell c1 = new Cell(0, 0).Add(new Paragraph("State"));
        //    table.AddCell(c1);

        //    Cell c2 = new Cell(0, 0).Add(new Paragraph("C2"));
        //    c2.Add(ls);
        //    c2.Add(new Paragraph("C3"));
        //    table.AddCell(c2);



        //    //Cell cell11 = new Cell(1, 1)
        //    //    .SetBackgroundColor(ColorConstants.GRAY)
        //    //    .SetTextAlignment(TextAlignment.CENTER)
        //    //    .Add(new Paragraph("State"));
        //    //Cell cell12 = new Cell(1, 1)
        //    //    .SetBackgroundColor(ColorConstants.GRAY)
        //    //    .SetTextAlignment(TextAlignment.CENTER)
        //    //    .Add(new Paragraph("Capital"));

        //    //Cell cell21 = new Cell(1, 1)
        //    //    .SetTextAlignment(TextAlignment.CENTER)
        //    //    .Add(new Paragraph("New York"));
        //    //Cell cell22 = new Cell(1, 1)
        //    //    .SetTextAlignment(TextAlignment.CENTER)
        //    //    .Add(new Paragraph("Albany"));

        //    //Cell cell31 = new Cell(1, 1)
        //    //    .SetTextAlignment(TextAlignment.CENTER)
        //    //    .Add(new Paragraph("New Jersey"));
        //    //Cell cell32 = new Cell(1, 1)
        //    //    .SetTextAlignment(TextAlignment.CENTER)
        //    //    .Add(new Paragraph("Trenton"));

        //    //Cell cell41 = new Cell(1, 1)
        //    //    .SetTextAlignment(TextAlignment.CENTER)
        //    //    .Add(new Paragraph("California"));
        //    //Cell cell42 = new Cell(1, 1)
        //    //    .SetTextAlignment(TextAlignment.CENTER)
        //    //    .Add(new Paragraph("Sacramento"));

        //    //table.AddCell(cell11);
        //    //table.AddCell(cell12);
        //    //table.AddCell(cell21);
        //    //table.AddCell(cell22);
        //    //table.AddCell(cell31);
        //    //table.AddCell(cell32);
        //    //table.AddCell(cell41);
        //    //table.AddCell(cell42);
        //    document.Add(table);


        //    document.Close();

        //}




    }
}
