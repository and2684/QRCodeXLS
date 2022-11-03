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
            var qrGenerator = new QRCodeGenerator();
            var qrCodeData = qrGenerator.CreateQrCode(textQR.Text, QRCodeGenerator.ECCLevel.Q);
            var qrCode = new QRCode(qrCodeData);
            var qrImage = qrCode.GetGraphic(20);

            //Bitmap b = new Bitmap(qrImage);
            //Graphics graphics = Graphics.FromImage(b);
            //graphics.DrawString("Hello", this.Font, Brushes.Black, 0, 0);

            Image imgFrame = Image.FromFile(@"C:\csharp\QRCodeDesktop\Frame.png");

            var drawFont = new Font("Arial", 50);
            var drawFormat = new StringFormat();
            drawFormat.Alignment = StringAlignment.Center;
            drawFormat.LineAlignment = StringAlignment.Center;
            var rectangleAgr = new Rectangle(0, -160, 700, 400);
            var rectangleSN = new Rectangle(0, 170, 700, 400);

            Bitmap bmImage = new Bitmap(qrImage);
            bmImage.SetResolution(700, 400);
            Graphics gFrame = Graphics.FromImage(imgFrame);
            gFrame.DrawImage(bmImage, 210, 50, 300, 300);
            gFrame.DrawString(textAgr.Text, drawFont, Brushes.Black, rectangleAgr, drawFormat);
            gFrame.DrawString(textSN.Text, drawFont, Brushes.Black, rectangleSN, drawFormat);

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

            var format = ImageFormat.Jpeg;

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                string ext = System.IO.Path.GetExtension(dialog.FileName);

                switch (ext)
                {
                    case ".png":
                        format = ImageFormat.Png;
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
