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
        Button startButton;

        public MainWindow()
        {
            //tworzenie okna
            this.Size = new System.Drawing.Size(350, 440);
            this.Text = "Minesweeper";

            //siatka pola
            positionX = 30; positionY = 100;
            width = 9; height = 9;
            howManyBombs = 10;

            //przycisk uruchamiający
            startButton = new Button();
            startButton.Size = new System.Drawing.Size(100, 50);
            startButton.Location = new System.Drawing.Point(115, 25);
            startButton.Text = "START";
            this.startButton.Click += new EventHandler(this.StartClicked);
            this.Controls.Add(startButton);

        }

        private void StartClicked(object sender, EventArgs e) //wykonuje się po naciśnięciu przycisku START
        {
            //wyłącza przycisk
            startButton.Enabled = false;

            //tworzy wzór pola minowego
            MineFieldGenerator generator = new MineFieldGenerator(width, height, howManyBombs);
            
            //rysuje pole na podstawie wygenerowanego wzoru
            Field[,] fieldArray = new Field[width + 2, height + 2];
            for (int i = 1; i < width + 1; i++)
            {
                for (int j=1; j< height+1; j++)
                {
                    fieldArray[i, j] = new Field(this, positionX, positionY, generator.allFieldBool[i,j], generator.allFieldInt[i,j]);
                    positionX += 30;
                }
                positionY += 30; positionX = 30;
            }
            positionX = 30; positionY = 30;
        }
    }
}
