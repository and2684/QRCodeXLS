using CsPotrace;
using ExcelLibrary.SpreadSheet;
using QRCoder;
using QRCodeXLS.Config;
using QRCodeXLS.Model;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading.Tasks;
using System.Windows.Navigation;
using Font = System.Drawing.Font;
using Image = System.Drawing.Image;

namespace QRCodeXLS.Helpers
{
    public static class Helpers
    {
        public static void ReadExcel(string path, List<QRString> list)
        {
            var book = Workbook.Load(path);
            var sheet = book.Worksheets[0];

            // Итерация по строкам с использованием индекса:
            for (int rowIndex = sheet.Cells.FirstRowIndex + 1; rowIndex <= sheet.Cells.LastRowIndex; rowIndex++)
            {
                var qrString = new QRString(sheet.Cells.GetRow(rowIndex));
                list.Add(qrString);
            }
        }

        public static async Task<Image> GenerateImageAsync(string nameAgr, string sn, string url)
        {
            var config = LoadConfigValues();

            var sizeMultiplyer = config.SizeMultiplier;
            var imageWidth = config.ImageWidth * sizeMultiplyer;
            var imageHeight = config.ImageHeight * sizeMultiplyer;
            var qrCodeSize = config.QRCodeSize;
            var qrCodeX = config.QRCodeX * sizeMultiplyer;
            var qrCodeY = config.QRCodeY * sizeMultiplyer;
            var qrCodeWidth = config.QRCodeWidth * sizeMultiplyer;
            var qrCodeHeight = config.QRCodeHeight * sizeMultiplyer;
            var rectangleAgrX = config.RectangleAgrX * sizeMultiplyer;
            var rectangleAgrY = config.RectangleAgrY * sizeMultiplyer;
            var rectangleAgrWidth = config.RectangleAgrWidth * sizeMultiplyer;
            var rectangleAgrHeight = config.RectangleAgrHeight * sizeMultiplyer;
            var rectangleSnx = config.RectangleSNX * sizeMultiplyer;
            var rectangleSny = config.RectangleSNY * sizeMultiplyer;
            var rectangleSnWidth = config.RectangleSNWidth * sizeMultiplyer;
            var rectangleSnHeight = config.RectangleSNHeight * sizeMultiplyer;

            var qrGenerator = new QRCodeGenerator();
            var qrCodeData = qrGenerator.CreateQrCode(url, QRCodeGenerator.ECCLevel.Q);
            var qrCode = new QRCode(qrCodeData);
            using (var qrImage = qrCode.GetGraphic(qrCodeSize))
            {
                Image imgFrame = new Bitmap(imageWidth, imageHeight, PixelFormat.Format24bppRgb);

                Graphics.FromImage(imgFrame).FillRectangle(Brushes.White, 0, 0, imgFrame.Width, imgFrame.Height);

                var drawFont = new Font("Arial", 20 * sizeMultiplyer);
                var drawFontBottom = new Font("Arial", 24 * sizeMultiplyer, FontStyle.Bold);

                StringFormat drawFormat = StringFormat.GenericTypographic;
                drawFormat.Alignment = StringAlignment.Center;
                drawFormat.LineAlignment = StringAlignment.Center;

                var reservedRightsFormat = new StringFormat();
                reservedRightsFormat.Alignment = StringAlignment.Far;
                reservedRightsFormat.LineAlignment = StringAlignment.Center;

                var rectangleAgr = new Rectangle(rectangleAgrX, rectangleAgrY, rectangleAgrWidth, rectangleAgrHeight);
                var rectangleSn = new Rectangle(rectangleSnx, rectangleSny, rectangleSnWidth, rectangleSnHeight);

                using (Bitmap bmImage = new Bitmap(qrImage))
                {
                    bmImage.SetResolution(imageWidth * 10 * sizeMultiplyer, imageHeight * 10 * sizeMultiplyer);

                    using (Graphics gFrame = Graphics.FromImage(imgFrame))
                    {
                        gFrame.DrawImage(bmImage, qrCodeX, qrCodeY, qrCodeWidth, qrCodeHeight);
                        gFrame.DrawString(nameAgr, drawFont, Brushes.Black, rectangleAgr, drawFormat);
                        gFrame.DrawString(sn, drawFontBottom, Brushes.Black, rectangleSn, drawFormat);
                        gFrame.DrawString("®", drawFontBottom, Brushes.Black, rectangleAgr, reservedRightsFormat);

                        DrawFrame(Brushes.Black, 1 * sizeMultiplyer, rectangleAgr, gFrame);
                        DrawFrame(Brushes.Black, 1 * sizeMultiplyer, rectangleSn, gFrame);
                    }
                }

                imgFrame.Tag = $"{nameAgr}_{sn}";
                return await Task.FromResult(imgFrame);
            }
        }

        public static void DrawFrame(Brush brush, int penWidth, Rectangle rect, Graphics gFrame, bool needDoubleFrame = true)
        {
            using (Pen pen = new Pen(brush, penWidth))
            {
                gFrame.DrawRectangle
                    (
                        pen,
                        rect.X, rect.Y,
                        rect.Width,
                        rect.Height
                    );

                if (needDoubleFrame)
                {
                    gFrame.DrawRectangle
                        (
                            pen,
                            rect.X + (penWidth * 5),
                            rect.Y + (penWidth * 5),
                            rect.Width - (penWidth * 10),
                            rect.Height - (penWidth * 10)
                        );
                }
            }
        }

        private static ImageConfig LoadConfigValues()
        {
            var config = new ImageConfig
            {
                SizeMultiplier = int.Parse(ConfigurationManager.AppSettings["SizeMultiplier"]),
                ImageWidth = int.Parse(ConfigurationManager.AppSettings["ImageWidth"]),
                ImageHeight = int.Parse(ConfigurationManager.AppSettings["ImageHeight"]),
                QRCodeSize = int.Parse(ConfigurationManager.AppSettings["QRCodeSize"]),
                QRCodeX = int.Parse(ConfigurationManager.AppSettings["QRCodeX"]),
                QRCodeY = int.Parse(ConfigurationManager.AppSettings["QRCodeY"]),
                QRCodeWidth = int.Parse(ConfigurationManager.AppSettings["QRCodeWidth"]),
                QRCodeHeight = int.Parse(ConfigurationManager.AppSettings["QRCodeHeight"]),
                RectangleAgrX = int.Parse(ConfigurationManager.AppSettings["RectangleAgrX"]),
                RectangleAgrY = int.Parse(ConfigurationManager.AppSettings["RectangleAgrY"]),
                RectangleAgrWidth = int.Parse(ConfigurationManager.AppSettings["RectangleAgrWidth"]),
                RectangleAgrHeight = int.Parse(ConfigurationManager.AppSettings["RectangleAgrHeight"]),
                RectangleSNX = int.Parse(ConfigurationManager.AppSettings["RectangleSNX"]),
                RectangleSNY = int.Parse(ConfigurationManager.AppSettings["RectangleSNY"]),
                RectangleSNWidth = int.Parse(ConfigurationManager.AppSettings["RectangleSNWidth"]),
                RectangleSNHeight = int.Parse(ConfigurationManager.AppSettings["RectangleSNHeight"])
            };

            return config;
        }


        public static void Vectorize(string inpath, string outpath)
        {
            var listOfPathes = new List<List<Curve>>();
            var bm = Bitmap.FromFile(inpath) as Bitmap;
            Potrace.Clear();
            listOfPathes.Clear();
            Potrace.Potrace_Trace(bm, listOfPathes);
            string svg = Potrace.getSVG();
            using (System.IO.StreamWriter W = new System.IO.StreamWriter(outpath))
            {
                W.WriteLine(svg);
            }
        }
    }
}
