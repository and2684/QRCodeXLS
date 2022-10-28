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
            pbQR.Image = qrCode.GetGraphic(20);
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
