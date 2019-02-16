using System.Windows.Forms;

namespace minesweeper
{
    public class MainForm
    {
        public static void Main()
        {
            ChoosingMenu choosingMenu = new ChoosingMenu();
            Application.Run(choosingMenu);
        }
    }
}
