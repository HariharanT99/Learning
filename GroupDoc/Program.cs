using GroupDocs.Redaction.Redactions;
using GroupDocs.Redaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using GroupDocs.Redaction.Options;
using SaveOptions = GroupDocs.Redaction.Options.SaveOptions;
using System.Drawing.Drawing2D;
using System.IO;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using UglyToad.PdfPig.Content;
using iTextSharp.text;
using System.IO.Packaging;
using DocumentFormat.OpenXml.Packaging;
using OpenXmlPowerTools;
using System.IO.Compression;
using System.Drawing;

namespace GroupDoc
{
    internal class Program
    {
        public static readonly string filePath = "D:\\Learning\\ModifyWordDoc\\TestDoc\\ActiveTemplate1.pdf";
        public static readonly string pathToSave = "D:\\Learning\\ModifyWordDoc\\TestDoc\\Modified\\Final\\TextStreamModifier";

        static void Main(string[] args)
        {
            //Compress();
            Decompress();

            //Decompress();
            //var filePath = "D:\\Learning\\ModifyWordDoc\\TestDoc\\ActiveTemplate1.pdf";

            //using (Redactor redactor = new Redactor(filePath))
            //{
            //    Redaction[] redactions = 
            //    {
            //        new ExactPhraseRedaction("<<First_name>>", false, new ReplacementOptions("Hariharan")),
            //        new ExactPhraseRedaction("<<Last_name>>", false, new ReplacementOptions("Tamilarasan test name"))
            //    };
            //    redactor.Apply(redactions);

                
            //    redactor.Save();
            //}

            //var filePath = "D:\\Learning\\ModifyWordDoc\\TestDoc\\ActivememberonlinejournalonlyCertificateTemplate.pdf";
            //var pathToSave = "D:\\Learning\\ModifyWordDoc\\TestDoc\\Modified\\Final\\test";

            //var source = Package.Open(filePath);
            //var document = WordprocessingDocument.Open(source);
            //HtmlConverterSettings settings = new HtmlConverterSettings();
            //XElement html = HtmlConverter.ConvertToHtml(document, settings);

            //Console.WriteLine(html.ToString());
            //var writer = File.CreateText(pathToSave);
            //writer.WriteLine(html.ToString());
            //writer.Dispose();
            Console.ReadLine();
        
        }

        public static void DecompressStreamData(byte[] data)
        {

            int start = 0;
            //while ((data[start] == 0x0a) | (data[start] == 0x0d)) start++; // skip trailling cr, lf

            byte[] tempdata = new byte[data.Length - start];
            Array.Copy(data, start, tempdata, 0, data.Length - start);

            MemoryStream msInput = new MemoryStream(tempdata);
            MemoryStream msOutput = new MemoryStream();
            try
            {
                GZipStream decomp = new GZipStream(msInput, CompressionMode.Decompress);
                decomp.CopyTo(msOutput);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }

        private static string decompress(byte[] input)
        {
            byte[] cutinput = new byte[input.Length - 2];
            Array.Copy(input, 2, cutinput, 0, cutinput.Length);

            var stream = new MemoryStream();

            using (var compressStream = new MemoryStream(cutinput))
            using (var decompressor = new DeflateStream(compressStream, CompressionMode.Decompress))
                decompressor.CopyTo(stream);

            return Encoding.Default.GetString(stream.ToArray());
        }

        private static void Compress()
        {
            FileStream fs = new FileStream(filePath, FileMode.Create);

            DeflateStream d_Stream = new DeflateStream(fs, CompressionMode.Compress);
            for (byte n = 0; n < 255; n++)
                d_Stream.WriteByte(n);
            d_Stream.Close();
            fs.Close();
        }

        private static void Decompress()
        {
            //FileStream fs = new FileStream(filePath, FileMode.Open);

            ////First two bytes are irrelevant
            //      fs.ReadByte();
            //fs.ReadByte();
            //byte[] bytes = new byte[fs.Length];
            //for (byte n = 0; n < 255; n++)
            //    bytes[n] = (byte)fs.ReadByte();



            decompress(File.ReadAllBytes(filePath));
            //DecompressStreamData(File.ReadAllBytes(filePath));

            //DeflateStream d_Stream = new DeflateStream(fs, CompressionMode.Decompress);

            //StreamToFile(d_Stream, pathToSave + DateTime.UtcNow.Ticks + ".txt", FileMode.OpenOrCreate);

            //d_Stream.Close();
            //fs.Close();
        }

        //private static void Decompress()
        //{
        //    FileStream fs = new FileStream(filePath, FileMode.Open);

        //    //First two bytes are irrelevant
        //    fs.ReadByte();
        //    fs.ReadByte();

        //    DeflateStream d_Stream = new DeflateStream(fs, CompressionMode.Decompress);

        //    StreamToFile(d_Stream, pathToSave + ".txt", FileMode.OpenOrCreate);

        //    d_Stream.Close();
        //    fs.Close();
        //}

        private static void StreamToFile(Stream inputStream, string outputFile, FileMode fileMode)
        {
            if (inputStream == null)
                throw new ArgumentNullException("inputStream");

            if (String.IsNullOrEmpty(outputFile))
                throw new ArgumentException("Argument null or empty.", "outputFile");

            //byte[] buffer = new byte[LEN];

            //decompress(inputStream.Read(buffer, 0, LEN))

            using (FileStream outputStream = new FileStream(outputFile, fileMode, FileAccess.Write))
            {
                int cnt = 0;
                const int LEN = 4096;
                byte[] buffer = new byte[LEN];

                //decompress(inputStream.Read(buffer, 0, (int)inputStream.Length));
                while ((cnt = inputStream.Read(buffer, 0, LEN)) != 0)
                    outputStream.Write(buffer, 0, cnt);
            }
        }

    }



    //internal class Program
    //{
    //    static void Main(string[] args)
    //    {
    //        var filePath = "D:\\Learning\\ModifyWordDoc\\TestDoc\\ActivememberonlinejournalonlyCertificateTemplate.pdf";
    //        var pathToSave = "D:\\Learning\\ModifyWordDoc\\TestDoc\\Modified\\Final\\";
    //        var pathToSaveTempImg = "D:\\Learning\\ModifyWordDoc\\TestDoc\\Modified\\Final\\Img";

    //        //Create File Save Path 
    //        if (!Directory.Exists(pathToSave))
    //            Directory.CreateDirectory(pathToSave);

    //        //Create File Save Path 
    //        if (!Directory.Exists(pathToSaveTempImg))
    //            Directory.CreateDirectory(pathToSaveTempImg);
    //        using (FileStream newFileStream = new FileStream(pathToSave + "test" + DateTime.UtcNow.Ticks, FileMode.Create))
    //        {
    //            PdfReader pdfReader = new PdfReader(filePath);
    //            PdfStamper pdfstamp = new PdfStamper(pdfReader, newFileStream);
    //            iTextSharp.text.pdf.PdfStream pdfStream = null;
    //            List<System.Drawing.Image> ImgList = new List<System.Drawing.Image>();


    //            PdfContentByte contentByte = null;
    //            PdfContentByte contentByteTemp = null;
    //            PdfContentByte contentByteTemp1 = null;
    //            BaseFont baseFont = null;

    //            TextWithFontExtractionStategy strategy = new TextWithFontExtractionStategy();

    //            string currentText = PdfTextExtractor.GetTextFromPage(pdfReader, 1, strategy);


    //            var list = strategy.GetResultantTextInfo();
    //            var userId = "«User_id» || Calibri ";
    //            var test = userId.ToLower().Contains("<<user_id>>");
    //            var textInfoList = list.Where(w => !string.IsNullOrWhiteSpace(w.Text) && (w.Text.ToLower().Contains(MembershipCertificatePlaceholder.UserId) || w.Text.ToLower().Contains(MembershipCertificatePlaceholder.FirstName) || w.Text.ToLower().Contains(MembershipCertificatePlaceholder.LastName) || w.Text.ToLower().Contains(MembershipCertificatePlaceholder.ApprovedDate) || w.Text.ToLower().Contains(MembershipCertificatePlaceholder.Title)));

    //            foreach (var textInfo in textInfoList)
    //            {
    //                String[] sperator = { "||" };

    //                var textList = textInfo.Text.Split(sperator, StringSplitOptions.RemoveEmptyEntries);

    //                var sourceText = textList[0];
    //                var fontName = textList.Length > 1 ? textList[1] : string.Empty;

    //                string fontPath = string.Empty;
    //                if (!string.IsNullOrWhiteSpace(fontName))
    //                    fontPath = "D:\\Learning\\ConsoleApp1\\Font\\" + fontName.Trim().ToLower() + ".TTF";

    //                var DefaultFont = "D:\\Learning\\ConsoleApp1\\Font\\" + "lucida calligraphy.TTF";

    //                if (File.Exists(fontPath))
    //                {
    //                    baseFont = BaseFont.CreateFont(fontPath, "", false);
    //                }
    //                else
    //                {
    //                    baseFont = BaseFont.CreateFont(DefaultFont, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
    //                }

    //                contentByteTemp = pdfstamp.GetOverContent(1);
    //                contentByteTemp.Rectangle(textInfo.TextStart, textInfo.TextEnd - 10, textInfo.Width, textInfo.Height + 13);
    //                contentByteTemp.SetColorFill(BaseColor.WHITE);
    //                contentByteTemp.SetFontAndSize(baseFont, textInfo.FontHeight);
    //                contentByteTemp.Fill();
    //                //contentByteTemp1 = pdfstamp.GetOverContent(1).ShowText("TEsdasdfsadsgfad");
    //                //contentByteTemp1.Fill();

    //                using (UglyToad.PdfPig.PdfDocument document = UglyToad.PdfPig.PdfDocument.Open(filePath))
    //                {
    //                    for (var i = 0; i < document.NumberOfPages; i++)
    //                    {
    //                        // This starts at 1 rather than 0.
    //                        var page = document.GetPage(i + 1);

    //                        //foreach (var word in page.GetWords())
    //                        //{
    //                        //    Console.WriteLine(word.Text);
    //                        //}
    //                        IEnumerable<IPdfImage> images = page.GetImages();

    //                        foreach (var image in images)
    //                        {
    //                            PdfContentByte contentTemp = null;

    //                            contentByteTemp = pdfstamp.GetOverContent(1);

    //                            File.WriteAllBytes(pathToSaveTempImg + "1.png", image.RawBytes.ToArray());
    //                            Image img = Image.GetInstance(pathToSaveTempImg + "1.png");
    //                            iTextSharp.text.pdf.parser.Matrix matrix = new iTextSharp.text.pdf.parser.Matrix((float)image.Bounds.Left, (float)image.Bounds.Right);
    //                            contentByte.AddImage(img);

    //                        }

    //                    }

    //                }

    //                contentByte = pdfstamp.GetOverContent(1);
    //                contentByte.SetColorFill(BaseColor.BLACK);
    //                contentByte.SetFontAndSize(baseFont, textInfo.FontHeight);
    //                contentByte.BeginText();
    //                contentByte.ShowTextAligned(0, "Afil Jamali  tradfdasdffds", textInfo.TextStart, textInfo.TextEnd, 0);
    //                contentByte.EndText();
    //                contentByte.Fill();
    //            }

    //            pdfstamp.Close();
    //            pdfReader.Close();
    //        }

    //        Console.WriteLine("Found table in the document");

    //        Console.ReadLine();

    //    }

    //    public class TextWithFontExtractionStategy : ITextExtractionStrategy
    //    {
    //        private StringBuilder result = new StringBuilder();
    //        private float llx;
    //        private float lly;
    //        private float urx;
    //        private float ury;
    //        private StringBuilder text = new StringBuilder();
    //        private List<TextInformation> textInformationList = new List<TextInformation>();
    //        TextInformation textInformation = null;

    //        private Vector lastBaseLine;

    //        public void RenderText(iTextSharp.text.pdf.parser.TextRenderInfo renderInfo)
    //        {
    //            string curFont = renderInfo.GetFont().PostscriptFontName;
    //            this.text.Append(renderInfo.GetText());

    //            //This code assumes that if the baseline changes then we're on a newline
    //            Vector curBaseline = renderInfo.GetBaseline().GetStartPoint();
    //            Vector topRight = renderInfo.GetAscentLine().GetEndPoint();
    //            iTextSharp.text.Rectangle rect = new iTextSharp.text.Rectangle(curBaseline[Vector.I1], curBaseline[Vector.I2], topRight[Vector.I1], topRight[Vector.I2]);
    //            Single curFontSize = rect.Height + 3;

    //            if (this.lastBaseLine == null || (curBaseline[Vector.I2] != lastBaseLine[Vector.I2]))
    //            {
    //                textInformation = new TextInformation();
    //                llx = curBaseline[Vector.I1];
    //                lly = curBaseline[Vector.I2];
    //                urx = topRight[Vector.I1];
    //                textInformation.TextStart = llx;
    //                textInformation.TextEnd = lly;
    //                textInformation.Height = topRight[Vector.I2] - curBaseline[Vector.I2];
    //                textInformation.FontHeight = curFontSize;
    //                textInformation.FontFamily = curFont;
    //                textInformation.Text = this.text.ToString();
    //                textInformation.Width = urx - textInformation.TextStart;
    //                this.text = new StringBuilder();
    //                this.textInformationList.Add(textInformation);
    //            }
    //            else if ((curBaseline[Vector.I2] == lastBaseLine[Vector.I2]))
    //            {

    //                //ury = curBaseline[Vector.I2];
    //                urx = topRight[Vector.I1];
    //                this.textInformationList.Last().Text = this.textInformationList.Last().Text + this.text.ToString();
    //                this.textInformationList.Last().Width = urx - this.textInformationList.Last().TextStart;
    //                this.text = new StringBuilder();
    //            }


    //            //Set currently used properties
    //            this.lastBaseLine = curBaseline;
    //        }

    //        public List<TextInformation> GetResultantTextInfo()
    //        {
    //            return textInformationList;
    //        }

    //        //public Font GetFont(string fontName, string filename)
    //        //{
    //        //    if (!FontFactory.IsRegistered(fontName))
    //        //    {
    //        //        var fontPath = Environment.GetEnvironmentVariable("SystemRoot") + "\\fonts\\" + filename;
    //        //        FontFactory.Register(fontPath);
    //        //    }
    //        //    return FontFactory.GetFont(fontName, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
    //        //}
    //        public void BeginTextBlock()
    //        {
    //        }

    //        public void EndTextBlock()
    //        {
    //        }

    //        public string GetResultantText()
    //        {
    //            return result.ToString();
    //        }

    //        public void RenderImage(ImageRenderInfo renderInfo)
    //        {
    //        }
    //    }

    //    public static class MembershipCertificatePlaceholder
    //    {
    //        public const string FirstName = "<<first_name>>";
    //        public const string LastName = "<<last_name>>";
    //        public const string UserId = "<<user_id>>";
    //        public const string ApprovedDate = "<<approved_date>>";
    //        public const string Title = "<<title>>";
    //    }
    //    public class TextInformation
    //    {
    //        public string Text { get; set; }
    //        public float FontHeight { get; set; }
    //        public float TextStart { get; set; }
    //        public float TextEnd { get; set; }
    //        public float Width { get; set; }
    //        public float Height { get; set; }
    //        public string FontFamily { get; set; }
    //    }
    //}

}
