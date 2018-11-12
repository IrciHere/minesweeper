using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace minesweeper
{
    public class MainForm
    {
        public static void Main()
        {
            var mainWindow = new MainWindow();
            Application.Run(mainWindow);
        }
    }
}
