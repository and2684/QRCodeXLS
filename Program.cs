using QRCodeXLS;
using System;
using System.Windows.Forms;

namespace QRCodeDesktop
{
    internal static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new QRCodeXLSClass());
        }
    }
}
