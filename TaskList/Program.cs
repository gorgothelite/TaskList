using System;
using System.Threading;
using System.Windows.Forms;

namespace Test
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var mutex = new Mutex(true, "TaskList_SingleInstance", out bool createdNew))
            {
                if (!createdNew)
                {
                    MessageBox.Show("TaskList is already running.", "Already Running",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new MainForm());
            }
        }
    }
}
