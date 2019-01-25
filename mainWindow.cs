using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace minesweeper
{
    public class MainWindow: Form
    {
        //deklaracja potrzebnych zmiennych
        int positionX, positionY, width, height, howManyBombs;
        public Button startButton;
        public Field[,] fieldArray;

        public MainWindow()
        {
            //tworzenie okna
            this.Size = new System.Drawing.Size(305, 395);
            this.Text = "Minesweeper";

            //siatka pola
            positionX = 30; positionY = 100;
            width = 9; height = 9;
            howManyBombs = 10;

            //przycisk uruchamiający
            startButton = new Button
            {
                Size = new System.Drawing.Size(100, 50),
                Location = new System.Drawing.Point(95, 25),
                Text = "START"
            };
            startButton.MouseClick += StartButton_MouseClick;
            this.Controls.Add(startButton);

        }

        private void StartButton_MouseClick(object sender, MouseEventArgs e)
        {
            //wyłącza przycisk
            startButton.Enabled = false;

            //tworzy wzór pola minowego
            MineFieldGenerator generator = new MineFieldGenerator(width, height, howManyBombs);

            //rysuje pole na podstawie wygenerowanego wzoru
            fieldArray = new Field[width + 2, height + 2];
            for (int i = 1; i < width + 1; i++)
            {
                for (int j = 1; j < height + 1; j++)
                {
                    fieldArray[i, j] = new Field(this, positionX, positionY, generator.allFieldBool[i, j], generator.allFieldInt[i, j]);
                    positionX += 25;
                }
                positionY += 25; positionX = 30;
            }
            positionX = 30; positionY = 30;
        }

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
    }
}
