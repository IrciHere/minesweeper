using System.Drawing;
using System.Windows.Forms;

namespace minesweeper
{
    public class Field
    {
        //deklaracja potrzebnych zmiennych
        private readonly MainWindow _form;
        public Button fieldButton;
        public bool isClicked;
        public readonly bool isBomb;
        public bool isFlagSet;
        public int bombsNearby, fieldX, fieldY;

        //konstruktor pojedynczego pola
        public Field(MainWindow form, int positionX, int positionY, bool bomb, int bombs, int fieldX, int fieldY)
        {
            _form = form;
            this.fieldX = fieldX;
            this.fieldY = fieldY;
            isClicked = false;
            bombsNearby = bombs;
            isBomb = bomb;
            isFlagSet = false;

            //rysowanie pola (jako przycisku)
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

        //po kliknięciu przycisku sprawdź przycisk myszy
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

        //po kliknięciu lewym wyłącz przycisk i pokaż co na nim jest
        public void CheckField()
        {
            if (isClicked || isFlagSet)
            {
                return;
            }

            fieldButton.Enabled = false;

            //jeżeli na polu jest bomba, gracz przegrywa
            if (isBomb)
            {
                fieldButton.Text = "X";
                fieldButton.ForeColor = Color.Red;
                fieldButton.Font = new Font(fieldButton.Font.FontFamily, fieldButton.Font.Size, FontStyle.Bold);
                fieldButton.FlatAppearance.BorderColor = Color.FromArgb(0, 0, 0);
                MessageBox.Show("Bomb! You died...");
                Application.Exit();
            }

            //wykona się jeżeli na klikniętym polu nie ma bomby
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

        //po kliknięciu prawym sprawdź oflagowanie
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
            }
        }
    }
}
