using System;
using System.Windows.Forms;

namespace FormatToHex
{
    static class Program
    {
        /// <summary>
        /// Reformat a text file from plain hex to usable hex.
        /// </summary>

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
