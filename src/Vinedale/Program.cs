using System;
using System.Windows.Forms;

namespace Vinedale
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            using MainForm mf = new MainForm();
            Application.Run(mf);
        }
    }
}
