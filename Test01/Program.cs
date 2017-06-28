using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Test01
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Form2 settingsForm;

            do {
                settingsForm = new Form2();

                Application.Run(settingsForm);

                if (settingsForm.formClosed)
                {
                    Form1 mainForm = new Form1(settingsForm.boardSizeX, settingsForm.boardSizeY, settingsForm.numToWin);
                    Application.Run(mainForm);
                }
            } while (settingsForm.formClosed);
        }
    }
}
