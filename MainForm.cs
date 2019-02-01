using System.Windows.Forms;

namespace minesweeper
{
    public class MainForm
    {
        public static void Main()
        {
            ChosingMenu chosingMenu = new ChosingMenu();
            Application.Run(chosingMenu);
        }
    }
}
