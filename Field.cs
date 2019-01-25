using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace minesweeper
{
    public class Field
    {
        //deklaracja potrzebnych zmiennych
        private MainWindow form;
        public Button fieldButton;
        public bool isClicked;
        readonly bool isBomb;
        bool flag;
        int bombsNearby;

        //konstruktor pojedynczego pola
        public Field(MainWindow form, int positionX, int positionY, bool bomb, int bombs)
        {
            this.form = form;
            isClicked = false;
            bombsNearby = bombs;
            isBomb = bomb;
            flag = false;
            //rysowanie pola (jako przycisku)
            fieldButton = new Button 
            { 
                Size = new System.Drawing.Size(25, 25),
                Location = new System.Drawing.Point(positionX, positionY),
                BackColor = Color.FromArgb(204, 235, 255),
                FlatStyle = FlatStyle.Flat
            };
            fieldButton.MouseDown += new MouseEventHandler(FieldClicked);

            form.Controls.Add(fieldButton);
        }

        //po kliknięciu przycisku sprawdź przycisk myszy
        private void FieldClicked(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Left:
                    ClickedLeft();
                    break;
                case MouseButtons.Right:
                    ClickedRight();
                    break;
            }
            
        }

        //po kliknięciu lewym wyłącz przycisk i pokaż co na nim jest
        private void ClickedLeft()
        {
            fieldButton.Enabled = false;
            if (isBomb)
            {
                fieldButton.Text = "X";
                fieldButton.ForeColor = Color.Red;
                fieldButton.Font = new Font(fieldButton.Font.FontFamily, fieldButton.Font.Size, FontStyle.Bold);
                fieldButton.FlatAppearance.BorderColor = Color.FromArgb(0, 0, 0);
                MessageBox.Show("Bomb! You died...");
                Application.Exit();
            }
            else
            {
                if (bombsNearby != 0)
                    fieldButton.Text = bombsNearby.ToString();
            }
            isClicked = true;
            fieldButton.BackColor = Color.FromArgb(255, 204, 204);
            form.CheckWin();
        }

        //po kliknięciu prawym sprawdź oflagowanie
        private void ClickedRight()
        {
            if (!flag)
            {
                flag = true;
                fieldButton.Text = "O";
                fieldButton.ForeColor = Color.Red;
                fieldButton.Font = new Font(fieldButton.Font.FontFamily, fieldButton.Font.Size, FontStyle.Bold);
            }
            else
            {
                flag = false;
                fieldButton.Text = "";
            }
        }
    }
}
