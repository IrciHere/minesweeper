using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace minesweeper
{
    class Field: Form
    {
        //deklaracja potrzebnych zmiennych
        public Button fieldButton;
        bool isBomb, isClicked;
        int bombsNearby;

        //konstruktor pojedynczego pola
        public Field(Form form, int positionX, int positionY, bool bomb, int bombs)
        {
            bombsNearby = bombs;
            isBomb = bomb;
            isClicked = false;
            fieldButton = new Button();

            //rysowanie pola (jako przycisku)
            fieldButton.Size = new System.Drawing.Size(25, 25);
            fieldButton.Location = new System.Drawing.Point(positionX, positionY);
            fieldButton.BackColor = Color.FromArgb(204, 235, 255);
            fieldButton.FlatStyle = FlatStyle.Flat;

            if (isBomb)
            {
                fieldButton.Text = "X";
                fieldButton.ForeColor = Color.Red;
                fieldButton.Font = new Font(fieldButton.Font.FontFamily, fieldButton.Font.Size, FontStyle.Bold);
                fieldButton.FlatAppearance.BorderColor = Color.FromArgb(0, 0, 0);
            }
                
            else
            {
                if(bombsNearby != 0)
                    fieldButton.Text = bombsNearby.ToString();
            }
                
            form.Controls.Add(fieldButton);
        }
    }
}
