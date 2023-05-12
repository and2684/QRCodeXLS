using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;

namespace QRCodeXLS
{
    public partial class QRCodeXLSClass : Form
    {
        string XLSPath;

        public QRCodeXLSClass()
        {
            InitializeComponent();
        }

        private void btnGetXLS_Click(object sender, EventArgs e)
        {
            try
            {
                using (var dialog = new OpenFileDialog())
                {
                    dialog.InitialDirectory = "c:\\";
                    dialog.Filter = "Файлы Excel (*.xls)|*.xls";
                    dialog.FilterIndex = 1;
                    dialog.RestoreDirectory = true;

                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        XLSPath = dialog.FileName;
                        tbXLSPath.Text = XLSPath;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Не удалось выбрать файл.\nПричина:\n{0}",ex.Message),
                                "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            var QRList = new List<QRString>();
            try
            {
                using (var dialog = new FolderBrowserDialog())
                {
                    dialog.Description = "Выберите каталог для сохранения изображений:";
                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        Cursor.Current = Cursors.WaitCursor;
                        // Создаем каталог куда будем складывать картинки
                        var folderPath = Path.Combine(dialog.SelectedPath, "QR_" + DateTime.Now.ToString("yyyyMMdd_hhmmss"));
                        Directory.CreateDirectory(folderPath);

                        Helpers.ReadExcel(XLSPath, QRList); // Читаем эксель
                        Refresh();

                        if (QRList.Count > 0)
                        {
                            // Подготовим Прогрессбар
                            pBar.Maximum = QRList.Count;
                            pBar.Value = 1;

                            foreach (var QR in QRList) // Генерируем картинки
                            {
                                using (var img = Helpers.GenerateImage(QR.NameAgr, QR.SerialNumber, QR.URL))
                                    img.Save(Path.Combine(folderPath, string.Format("{0}.gif", img.Tag.ToString())), ImageFormat.Gif);
                                pBar.PerformStep();
                            }

                            MessageBox.Show(string.Format("В каталог {0} сохранено {1} файлов.", folderPath, Directory.GetFiles(folderPath).Length),
                                            Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            pBar.Value = 0;
                            Process.Start(new ProcessStartInfo("explorer.exe", folderPath)); // Открыть папку в эксплорере
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Ошибка при сохранении изображений этикеток!\nПричина:\n{2}", Environment.NewLine, Environment.NewLine, ex.Message),
                                "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }
    }
}
