using EFLibrary.Forms;
using System;
using System.Windows.Forms;

namespace EFLibrary
{
    internal class Program
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
                Application.Run(new MainScreen());
            }      
    }
}
