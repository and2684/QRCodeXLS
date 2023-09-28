using QRCodeXLS.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.IO;
using System.Threading.Tasks;
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
                    dialog.InitialDirectory = "C:\\";
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
                MessageBox.Show("Не удалось выбрать файл.\n" +
                                $"Причина:{ex.Message}",
                                "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnGenerate_Click(object sender, EventArgs e)
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

                        Helpers.Helpers.ReadExcel(XLSPath, QRList); // Читаем эксель

                        if (QRList.Count > 0)
                        {
                            // Подготовим Прогрессбар
                            
                            pBar.Maximum = QRList.Count;
                            if (int.Parse(ConfigurationManager.AppSettings["Makesvg"]) == 1)
                                pBar.Maximum *= 2;
                            pBar.Value = 0;

                            var tasks = new List<Task>();
                            foreach (var QR in QRList) // Генерируем картинки
                            {
                                var task = Task.Run(async () =>
                                {
                                    using (var img = await Helpers.Helpers.GenerateImageAsync(QR.NameAgr,
                                               QR.SerialNumber, QR.URL))
                                    {
                                        var pathJpg = Path.Combine(folderPath, $"{img.Tag}.jpeg");
                                        img.Save(pathJpg, ImageFormat.Jpeg);
                                    }

                                    Invoke((MethodInvoker)delegate { pBar.PerformStep(); }); // Выполняет движение по прогрессбару в UI-потоке
                                });
                                tasks.Add(task);
                            }

                            await Task.WhenAll(tasks);

                            // Vectorize использует static-поля класса Potrace, нельзя им пользоваться сразу в нескольких потоках
                            if (int.Parse(ConfigurationManager.AppSettings["Makesvg"]) == 1)
                            {
                                Directory.CreateDirectory(Path.Combine(folderPath, "svg"));
                                foreach (var QR in QRList)
                                {
                                    var inpath = Path.Combine(folderPath, $"{QR.NameAgr}_{QR.SerialNumber}.jpeg");
                                    var outpath = Path.Combine(folderPath, "svg", $"{QR.NameAgr}_{QR.SerialNumber}.svg");
                                    Helpers.Helpers.Vectorize(inpath, outpath);
                                    pBar.PerformStep();
                                }
                            }

                            MessageBox.Show($"В каталог {folderPath} сохранено {pBar.Value} файлов.",
                                            Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            pBar.Value = 0;
                            Process.Start(new ProcessStartInfo("explorer.exe", folderPath)); // Открыть папку в эксплорере
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при сохранении изображений этикеток!\n" +
                                $"Причина: {ex.Message}",
                                "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }
    }
}
