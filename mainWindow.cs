using System;
using System.Drawing;
using System.Windows.Forms;

namespace minesweeper
{
    public class MainWindow : Form
    {
        //deklaracja potrzebnych zmiennych
        int positionX, positionY, width, height, howManyBombs;
        public Button startButton;
        public Field[,] fieldArray;
        public ChosingMenu form;

        public MainWindow(ChosingMenu form, int width, int height, int howManyBombs)
        {
            //tworzenie okna
            Size = new Size((width * 25) + 80, (height * 25) + 170);
            Text = "Minesweeper";

            //siatka pola
            positionX = 30; positionY = 100;
            this.width = width; this.height = height;
            this.howManyBombs = howManyBombs;

            //przycisk uruchamiający
            startButton = new Button
            {
                Size = new Size(100, 50),
                Location = new Point((((width * 25) + 80) / 2) - 60, 25),
                Text = "START"
            };
            startButton.MouseClick += StartButton_MouseClick;
            Controls.Add(startButton);

            //przy zamknięciu okna uruchomi spowrotem wybór poziomu trudności
            this.form = form;
            FormClosing += FormClosed;
        }

        //przy zamykaniu okna
        private new void FormClosed(object sender, EventArgs e)
        {
            form.Visible = true;
        }

        private void StartButton_MouseClick(object sender, MouseEventArgs e)
        {
            //wyłącza przycisk
            startButton.Enabled = false;

            //tworzy wzór pola minowego
            var generator = new MineFieldGenerator(width, height, howManyBombs);

            //rysuje pole na podstawie wygenerowanego wzoru
            fieldArray = new Field[width + 2, height + 2];
            for (int i = 1; i < height + 1; i++)
            {
                for (int j = 1; j < width + 1; j++)
                {
                    fieldArray[j, i] = new Field(this, positionX, positionY, generator.allFieldBool[j, i], generator.allFieldInt[j, i], j, i);
                    positionX += 25;
                }
                positionY += 25; positionX = 30;
            }
            positionX = 30; positionY = 30;
        }

        //metoda sprawdzająca wygraną
        public void CheckWin()
        {
            int fields = width * height;
            for (int i = 1; i < width + 1; i++)
            {
                for (int j = 1; j < height + 1; j++)
                {
                    if (fieldArray[i, j].isClicked == true)
                        fields--;
                }
            }
            if (fields == howManyBombs)
            {
                MessageBox.Show("Congratulations, you won!");
                Application.Exit();
            }
        }

        //metoda wywoływana gdy kliknięto puste pole - otwiera też pola dookoła
        public void CheckNearFields(int fieldX, int fieldY)
        {
            int startX = (fieldX - 1), startY = (fieldY - 1);

            for (int i = startX; i < startX + 3; i++)
            {
                for (int j = startY; j < startY + 3; j++)
                {
                    //wszystkie warunki kiedy nie wykonywać funkcji zawarłem w jednym boolu
                    bool v = (i == fieldX && j == fieldY) || i == 0 || j == 0 || i == (width + 1) || j == (height + 1);

                    if (!v) //wnętrze tego ifa jest skopiowane z klasy Field
                    {
                        if (!fieldArray[i, j].isClicked)
                        {
                            if (fieldArray[i, j].bombsNearby != 0)
                            {
                                fieldArray[i, j].fieldButton.Text = fieldArray[i, j].bombsNearby.ToString();
                            }

                            fieldArray[i, j].fieldButton.Enabled = false;
                            fieldArray[i, j].isClicked = true;
                            fieldArray[i, j].fieldButton.BackColor = Color.FromArgb(255, 204, 204);
                            CheckWin();

                            //jeżeli na którymś z okolicznych pól nie ma bomby, sprawdza również to pole
                            if (fieldArray[i, j].bombsNearby == 0)
                            {
                                CheckNearFields(i, j);
                            }
                        }

                    }
                }
            }
        }
    }
}
