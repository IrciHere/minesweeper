using System.Drawing;
using System.Windows.Forms;

namespace minesweeper
{
    public class Field
    {
        //
        //DECLARE VARIABLES NEEDED
        //
        private readonly MainWindow _form;
        public Button fieldButton;
        public bool isClicked;
        public readonly bool isBomb;
        public bool isFlagSet;
        public int bombsNearby, fieldX, fieldY;

        //
        //SINGLE FIELD CONSTRUCTOR
        //
        public Field(MainWindow form, int positionX, int positionY, bool bomb, int bombs, int fieldX, int fieldY)
        {
            _form = form;
            this.fieldX = fieldX;
            this.fieldY = fieldY;
            isClicked = false;
            bombsNearby = bombs;
            isBomb = bomb;
            isFlagSet = false;

            //drawing field (as a button)
            fieldButton = new Button
            {
                Size = new System.Drawing.Size(25, 25),
                Location = new System.Drawing.Point(positionX, positionY),
                BackColor = Color.FromArgb(204, 235, 255),
                FlatStyle = FlatStyle.Flat
            };
            fieldButton.MouseDown += FieldClicked;

            form.Controls.Add(fieldButton);
        }

        //check which mouse button was clicked
        private void FieldClicked(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Left:
                    CheckField();
                    break;
                case MouseButtons.Right:
                    PutFlag();
                    break;
            }
        }

        //
        //AFTER PRESSING LEFT MOUSE BUTTON, DISABLE FIELD AND SHOW ITS CONTENT
        //
        public void CheckField()
        {
            if (isClicked || isFlagSet)
            {
                return;
            }

            fieldButton.Enabled = false;

            //if there is bomb on a field, player loses
            if (isBomb)
            {
                fieldButton.Text = "X";
                fieldButton.ForeColor = Color.Red;
                fieldButton.Font = new Font(fieldButton.Font.FontFamily, fieldButton.Font.Size, FontStyle.Bold);
                fieldButton.FlatAppearance.BorderColor = Color.FromArgb(0, 0, 0);
                MessageBox.Show("Bomb! You died...");
                _form.Close();
            }

            //entering 'else' if there is no bomb on a field
            else
            {
                if (isClicked || isFlagSet)
                {
                    return;
                }
                if (bombsNearby != 0)
                {
                    fieldButton.Text = bombsNearby.ToString();
                }
                else
                {
                    _form.CheckNearFields(fieldX, fieldY);
                }

                isClicked = true;
                fieldButton.BackColor = Color.FromArgb(255, 204, 204);
                _form.CheckWin();
            }
        }

        //
        //AFTER PRESSING FIELD WITH RIGHT MOUSE BUTTON
        //
        private void PutFlag()
        {
            if (isClicked)
            {
                return;
            }
            if (!isFlagSet)
            {
                isFlagSet = true;
                fieldButton.Text = "O";
                fieldButton.ForeColor = Color.Red;
                fieldButton.Font = new Font(fieldButton.Font.FontFamily, fieldButton.Font.Size, FontStyle.Bold);
            }
            else
            {
                isFlagSet = false;
                fieldButton.Text = "";
                fieldButton.ForeColor = Color.Black;
                fieldButton.Font = new Font(fieldButton.Font.FontFamily, fieldButton.Font.Size, FontStyle.Regular);
            }
        }
    }
}
