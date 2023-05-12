using ExcelLibrary.SpreadSheet;
using QRCoder;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;

namespace QRCodeXLS
{
    public static class Helpers
    {
        public static void ReadExcel(string path, List<QRString> list)
        {
            var book = Workbook.Load(path);
            var sheet = book.Worksheets[0];

            // Итерация по строкам с использованием индекса:
            for (int rowIndex = sheet.Cells.FirstRowIndex + 1;
                 rowIndex <= sheet.Cells.LastRowIndex;
                 rowIndex++)
            {
                var row = sheet.Cells.GetRow(rowIndex);
                var QRString = new QRString();
                if (row.GetCell(0).Value != null)
                    QRString.NameAgr = row.GetCell(0).Value.ToString();
                else
                    QRString.NameAgr = string.Empty;

                if (row.GetCell(1).Value != null)
                    QRString.SerialNumber = row.GetCell(1).Value.ToString();
                else
                    QRString.SerialNumber = string.Empty;

                if (row.GetCell(2).Value != null)
                    QRString.URL = row.GetCell(2).Value.ToString();
                else
                    QRString.URL = string.Empty;

                list.Add(QRString);
            }
        }

        public static System.Drawing.Image GenerateImage(string nameAgr, string sn, string url)
        {
            var SizeMultiplyer = 1; // По умолчанию зададим 1 (использовалось в версии без xls)
            var ImageWidth = 350 * SizeMultiplyer;
            var ImageHeight = 200 * SizeMultiplyer;

            var qrGenerator = new QRCodeGenerator();
            var qrCodeData = qrGenerator.CreateQrCode(url, QRCodeGenerator.ECCLevel.Q);
            var qrCode = new QRCode(qrCodeData);
            using (var qrImage = qrCode.GetGraphic(100))
            {
                System.Drawing.Image imgFrame = new Bitmap(ImageWidth, ImageHeight, PixelFormat.Format24bppRgb);

                Graphics.FromImage(imgFrame).FillRectangle(Brushes.White, 0, 0, imgFrame.Width, imgFrame.Height);

                var drawFont = new Font("Arial", 20 * SizeMultiplyer);
                var drawFontBottom = new Font("Arial", 24 * SizeMultiplyer, System.Drawing.FontStyle.Bold);

                StringFormat drawFormat = StringFormat.GenericTypographic;
                drawFormat.Alignment = StringAlignment.Center;
                drawFormat.LineAlignment = StringAlignment.Center;

                var reservedRightsFormat = new StringFormat();
                reservedRightsFormat.Alignment = StringAlignment.Far;
                reservedRightsFormat.LineAlignment = StringAlignment.Center;

                var rectangleAgr = new Rectangle(31 * SizeMultiplyer, 5 * SizeMultiplyer, 288 * SizeMultiplyer, 40 * SizeMultiplyer);
                var rectangleSN = new Rectangle(20 * SizeMultiplyer, 155 * SizeMultiplyer, 310 * SizeMultiplyer, 40 * SizeMultiplyer);

                using (Bitmap bmImage = new Bitmap(qrImage))
                {
                    bmImage.SetResolution(ImageWidth * 10, ImageHeight * 10);

                    Graphics gFrame = Graphics.FromImage(imgFrame);
                    gFrame.DrawImage(bmImage, 113 * SizeMultiplyer, 38 * SizeMultiplyer, 125 * SizeMultiplyer, 125 * SizeMultiplyer);
                    gFrame.DrawString(nameAgr, drawFont, Brushes.Black, rectangleAgr, drawFormat);
                    gFrame.DrawString(sn, drawFontBottom, Brushes.Black, rectangleSN, drawFormat);
                    gFrame.DrawString("®", drawFontBottom, Brushes.Black, rectangleAgr, reservedRightsFormat);

                    DrawFrame(Brushes.Black, 1 * SizeMultiplyer, rectangleAgr, gFrame);
                    DrawFrame(Brushes.Black, 1 * SizeMultiplyer, rectangleSN, gFrame);

                    gFrame.Dispose();
                }

                imgFrame.Tag = string.Format("{0}_{1}", nameAgr, sn);
                return imgFrame;
            }
        }

        public static void DrawFrame(Brush brush, int penWidth, Rectangle rect, Graphics gFrame, bool needDoubleFrame = true)
        {
            using (Pen pen = new Pen(Brushes.Black, penWidth))
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
