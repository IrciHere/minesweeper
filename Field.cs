using System;
using System.Collections.Generic;
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
            if (isBomb)
                fieldButton.Text = "B";
            else
                fieldButton.Text = bombsNearby.ToString();
            form.Controls.Add(fieldButton);
        }
    }
}
