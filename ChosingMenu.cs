using System;
using System.Drawing;
using System.Windows.Forms;

namespace minesweeper
{
    public class ChosingMenu : Form
    {

        public Button buttonEasy, buttonMedium, buttonHard, buttonCustom;
        public Label label;
        public ChosingMenu()
        {
            Size = new Size(300, 365);
            Text = "Choose Difficulty Level";

            buttonEasy = new Button
            {
                Size = new Size(100, 50),
                Location = new Point(25, 25),
                Text = "EASY",
                Parent = this,
            };
            buttonEasy.MouseClick += buttonEasy_MouseClick;

            buttonMedium = new Button
            {
                Size = new Size(100, 50),
                Location = new Point(25, 100),
                Text = "MEDIUM",
                Parent = this
            };
            buttonMedium.MouseClick += buttonMedium_MouseClick;

            buttonHard = new Button
            {
                Size = new Size(100, 50),
                Location = new Point(25, 175),
                Text = "HARD",
                Parent = this
            };
            buttonHard.MouseClick += buttonHard_MouseClick;

            buttonCustom = new Button
            {
                Size = new Size(100, 50),
                Location = new Point(25, 250),
                Text = "CUSTOM",
                Parent = this,
                Enabled = false
            };
            buttonCustom.MouseClick += buttonCustom_MouseClick;

        }
        
        private void buttonEasy_MouseClick(object sender, MouseEventArgs e)
        {
            MainWindow mainWindow = new MainWindow(this, 9, 9, 10);
            Visible = false;
            mainWindow.ShowDialog();           
        }

        private void buttonMedium_MouseClick(object sender, MouseEventArgs e)
        {
            MainWindow mainWindow = new MainWindow(this, 16, 16, 40);
            Visible = false;
            mainWindow.ShowDialog();
        }

        private void buttonHard_MouseClick(object sender, MouseEventArgs e)
        {
            MainWindow mainWindow = new MainWindow(this, 30, 16, 99);
            Visible = false;
            mainWindow.ShowDialog();
        }

        private void buttonCustom_MouseClick(object sender, MouseEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
