using QRCoder;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace QRCodeDesktop
{
    public partial class QRCodeDesktop : Form
    {
        public QRCodeDesktop()
        {
            InitializeComponent();
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            var SizeMultiplyer = tbImageSize.Value;
            var ImageWidth = 700 * SizeMultiplyer;
            var ImageHeight = 400 * SizeMultiplyer;

            var qrGenerator = new QRCodeGenerator();
            var qrCodeData = qrGenerator.CreateQrCode(textQR.Text, QRCodeGenerator.ECCLevel.Q);
            var qrCode = new QRCode(qrCodeData);
            var qrImage = qrCode.GetGraphic(200);

            //Bitmap b = new Bitmap(qrImage);
            //Graphics graphics = Graphics.FromImage(b);
            //graphics.DrawString("Hello", this.Font, Brushes.Black, 0, 0);

            //Image imgFrame = Image.FromFile(@"C:\csharp\QRCodeDesktop\Frame.png");
            Image imgFrame = new Bitmap(ImageWidth, ImageHeight, PixelFormat.Format24bppRgb);
            Graphics.FromImage(imgFrame).FillRectangle(Brushes.White, 0, 0, imgFrame.Width, imgFrame.Height);

            var drawFont = new Font("Arial", 50 * SizeMultiplyer);
            var drawFormat = new StringFormat();
            drawFormat.Alignment = StringAlignment.Center;
            drawFormat.LineAlignment = StringAlignment.Center;

            var rectangleAgr = new Rectangle(80 * SizeMultiplyer, 10 * SizeMultiplyer, 540 * SizeMultiplyer, 90 * SizeMultiplyer);
            var rectangleSN = new Rectangle(80 * SizeMultiplyer, 300 * SizeMultiplyer, 540 * SizeMultiplyer, 90 * SizeMultiplyer);

            Bitmap bmImage = new Bitmap(qrImage);
            bmImage.SetResolution(ImageWidth, ImageHeight);
            Graphics gFrame = Graphics.FromImage(imgFrame);
            gFrame.DrawImage(bmImage, 250 * SizeMultiplyer, 100 * SizeMultiplyer, 200 * SizeMultiplyer, 200 * SizeMultiplyer);
            gFrame.DrawString(textAgr.Text, drawFont, Brushes.Black, rectangleAgr, drawFormat);
            gFrame.DrawString(textSN.Text, drawFont, Brushes.Black, rectangleSN, drawFormat);

            Helpers.DrawFrame(Brushes.Black, 1 * SizeMultiplyer, rectangleAgr, gFrame);
            Helpers.DrawFrame(Brushes.Black, 1 * SizeMultiplyer, rectangleSN, gFrame);

            gFrame.Dispose();

            pbQR.Image = imgFrame;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveImage(pbQR.Image);
        }

        private void SaveImage(Image image)
        {
            var dialog = new SaveFileDialog();
            dialog.Filter = "Images|*.jpg;*.png;*.bmp";

            var format = ImageFormat.Png;

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                string ext = System.IO.Path.GetExtension(dialog.FileName);

                switch (ext)
                {
                    case ".jpg":
                        format = ImageFormat.Jpeg;
                        break;
                    case ".jpeg":
                        format = ImageFormat.Jpeg;
                        break;
                    case ".bmp":
                        format = ImageFormat.Bmp;
                        break;
                }
                image.Save(dialog.FileName, format);
            }
        }
    }
}
