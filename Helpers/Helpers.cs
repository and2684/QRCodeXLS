using ExcelLibrary.SpreadSheet;
using QRCoder;
using QRCodeXLS.Config;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading.Tasks;

namespace QRCodeXLS
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

        public static async Task<System.Drawing.Image> GenerateImageAsync(string nameAgr, string sn, string url)
        {
            var config = LoadConfigValues();

            var SizeMultiplyer = config.SizeMultiplyer;
            var ImageWidth = config.ImageWidth * SizeMultiplyer;
            var ImageHeight = config.ImageHeight * SizeMultiplyer;
            var QRCodeSize = config.QRCodeSize;
            var QRCodeX = config.QRCodeX * SizeMultiplyer;
            var QRCodeY = config.QRCodeY * SizeMultiplyer;
            var QRCodeWidth = config.QRCodeWidth * SizeMultiplyer;
            var QRCodeHeight = config.QRCodeHeight * SizeMultiplyer;
            var RectangleAgrX = config.RectangleAgrX * SizeMultiplyer;
            var RectangleAgrY = config.RectangleAgrY * SizeMultiplyer;
            var RectangleAgrWidth = config.RectangleAgrWidth * SizeMultiplyer;
            var RectangleAgrHeight = config.RectangleAgrHeight * SizeMultiplyer;
            var RectangleSNX = config.RectangleSNX * SizeMultiplyer;
            var RectangleSNY = config.RectangleSNY * SizeMultiplyer;
            var RectangleSNWidth = config.RectangleSNWidth * SizeMultiplyer;
            var RectangleSNHeight = config.RectangleSNHeight * SizeMultiplyer;

            var qrGenerator = new QRCodeGenerator();
            var qrCodeData = qrGenerator.CreateQrCode(url, QRCodeGenerator.ECCLevel.Q);
            var qrCode = new QRCode(qrCodeData);
            using (var qrImage = qrCode.GetGraphic(QRCodeSize))
            {
                System.Drawing.Image imgFrame = new Bitmap(ImageWidth, ImageHeight, PixelFormat.Format24bppRgb);

                Graphics.FromImage(imgFrame).FillRectangle(Brushes.White, 0, 0, imgFrame.Width, imgFrame.Height);

                var drawFont = new Font("Arial", 20 * SizeMultiplyer);
                var drawFontBottom = new Font("Arial", 24 * SizeMultiplyer, FontStyle.Bold);

                StringFormat drawFormat = StringFormat.GenericTypographic;
                drawFormat.Alignment = StringAlignment.Center;
                drawFormat.LineAlignment = StringAlignment.Center;

                var reservedRightsFormat = new StringFormat();
                reservedRightsFormat.Alignment = StringAlignment.Far;
                reservedRightsFormat.LineAlignment = StringAlignment.Center;

                var rectangleAgr = new Rectangle(RectangleAgrX, RectangleAgrY, RectangleAgrWidth, RectangleAgrHeight);
                var rectangleSN = new Rectangle(RectangleSNX, RectangleSNY, RectangleSNWidth, RectangleSNHeight);

                using (Bitmap bmImage = new Bitmap(qrImage))
                {
                    bmImage.SetResolution(ImageWidth * 10 * SizeMultiplyer, ImageHeight * 10 * SizeMultiplyer);

                    using (Graphics gFrame = Graphics.FromImage(imgFrame))
                    {
                        gFrame.DrawImage(bmImage, QRCodeX, QRCodeY, QRCodeWidth, QRCodeHeight);
                        gFrame.DrawString(nameAgr, drawFont, Brushes.Black, rectangleAgr, drawFormat);
                        gFrame.DrawString(sn, drawFontBottom, Brushes.Black, rectangleSN, drawFormat);
                        gFrame.DrawString("®", drawFontBottom, Brushes.Black, rectangleAgr, reservedRightsFormat);

                        DrawFrame(Brushes.Black, 1 * SizeMultiplyer, rectangleAgr, gFrame);
                        DrawFrame(Brushes.Black, 1 * SizeMultiplyer, rectangleSN, gFrame);
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
            ImageConfig config = new ImageConfig();

            config.SizeMultiplyer = int.Parse(ConfigurationManager.AppSettings["SizeMultiplyer"]);
            config.ImageWidth = int.Parse(ConfigurationManager.AppSettings["ImageWidth"]);
            config.ImageHeight = int.Parse(ConfigurationManager.AppSettings["ImageHeight"]);
            config.QRCodeSize = int.Parse(ConfigurationManager.AppSettings["QRCodeSize"]);
            config.QRCodeX = int.Parse(ConfigurationManager.AppSettings["QRCodeX"]);
            config.QRCodeY = int.Parse(ConfigurationManager.AppSettings["QRCodeY"]);
            config.QRCodeWidth = int.Parse(ConfigurationManager.AppSettings["QRCodeWidth"]);
            config.QRCodeHeight = int.Parse(ConfigurationManager.AppSettings["QRCodeHeight"]);
            config.RectangleAgrX = int.Parse(ConfigurationManager.AppSettings["RectangleAgrX"]);
            config.RectangleAgrY = int.Parse(ConfigurationManager.AppSettings["RectangleAgrY"]);
            config.RectangleAgrWidth = int.Parse(ConfigurationManager.AppSettings["RectangleAgrWidth"]);
            config.RectangleAgrHeight = int.Parse(ConfigurationManager.AppSettings["RectangleAgrHeight"]);
            config.RectangleSNX = int.Parse(ConfigurationManager.AppSettings["RectangleSNX"]);
            config.RectangleSNY = int.Parse(ConfigurationManager.AppSettings["RectangleSNY"]);
            config.RectangleSNWidth = int.Parse(ConfigurationManager.AppSettings["RectangleSNWidth"]);
            config.RectangleSNHeight = int.Parse(ConfigurationManager.AppSettings["RectangleSNHeight"]);

            return config;
        }
    }
}
