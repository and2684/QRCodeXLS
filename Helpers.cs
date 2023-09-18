using ExcelLibrary.SpreadSheet;
using QRCoder;
using System.Collections.Generic;
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
            var ImageWidth = 350;
            var ImageHeight = 200;

            var qrGenerator = new QRCodeGenerator();
            var qrCodeData = qrGenerator.CreateQrCode(url, QRCodeGenerator.ECCLevel.Q);
            var qrCode = new QRCode(qrCodeData);
            using (var qrImage = qrCode.GetGraphic(100))
            {
                System.Drawing.Image imgFrame = new Bitmap(ImageWidth, ImageHeight, PixelFormat.Format24bppRgb);

                Graphics.FromImage(imgFrame).FillRectangle(Brushes.White, 0, 0, imgFrame.Width, imgFrame.Height);

                var drawFont = new Font("Arial", 20);
                var drawFontBottom = new Font("Arial", 24, FontStyle.Bold);

                StringFormat drawFormat = StringFormat.GenericTypographic;
                drawFormat.Alignment = StringAlignment.Center;
                drawFormat.LineAlignment = StringAlignment.Center;

                var reservedRightsFormat = new StringFormat();
                reservedRightsFormat.Alignment = StringAlignment.Far;
                reservedRightsFormat.LineAlignment = StringAlignment.Center;

                var rectangleAgr = new Rectangle(31, 5, 288, 40);
                var rectangleSN = new Rectangle(20, 155, 310, 40);

                using (Bitmap bmImage = new Bitmap(qrImage))
                {
                    bmImage.SetResolution(ImageWidth * 10, ImageHeight * 10);

                    using (Graphics gFrame = Graphics.FromImage(imgFrame))
                    {
                        gFrame.DrawImage(bmImage, 113, 38, 125, 125);
                        gFrame.DrawString(nameAgr, drawFont, Brushes.Black, rectangleAgr, drawFormat);
                        gFrame.DrawString(sn, drawFontBottom, Brushes.Black, rectangleSN, drawFormat);
                        gFrame.DrawString("®", drawFontBottom, Brushes.Black, rectangleAgr, reservedRightsFormat);

                        DrawFrame(Brushes.Black, 1, rectangleAgr, gFrame);
                        DrawFrame(Brushes.Black, 1, rectangleSN, gFrame);
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

    }
}
