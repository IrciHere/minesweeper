using System;
using System.Drawing;
using System.Windows.Forms;

namespace minesweeper
{
    public class MainWindow : Form
    {
        //
        //DECLARE VARIABLES NEEDED
        //
        private int _positionX, _positionY, _bombsLeft;
        private readonly int _width, _height, _howManyBombs;
        private readonly string _difficulty;
        public Button startButton;
        public Field[,] fieldArray;
        public ChoosingMenu form;
        public TimeBoard timeBoard;
        public Label bombsLabel;

        public MainWindow(ChoosingMenu form, int width, int height, int howManyBombs, string difficulty)
        {
            //creating window
            Size = new Size((width * 25) + 80, (height * 25) + 170);
            Text = "Minesweeper";

            //minefield template
            _positionX = 30; _positionY = 100;
            _width = width; _height = height;
            _howManyBombs = howManyBombs;
            _bombsLeft = howManyBombs;

            //start button
            startButton = new Button
            {
                Size = new Size(100, 50),
                Location = new Point((((width * 25) + 80) / 2) - 60, 25),
                Text = "START"
            };
            startButton.MouseClick += StartButton_MouseClick;
            Controls.Add(startButton);

            //after closing window you can choose difficulty again
            this.form = form;
            FormClosing += FormClosed;

            //scoreboard and stopwatch
            _difficulty = difficulty;
            timeBoard = new TimeBoard(this);

            bombsLabel = new Label
            {
                AutoSize = true,
                Location = new Point(5, 5),
                Text = "BOMBS LEFT: " + _bombsLeft.ToString(),
                Visible = true,
                Font = new Font("Arial", 10)
            };
            Controls.Add(bombsLabel);
        }

        public void ChangeBombs(int bombsChange)
        {
            _bombsLeft += bombsChange;
            bombsLabel.Text = "BOMBS LEFT: " + _bombsLeft.ToString();
        }

        //when closing window
        private new void FormClosed(object sender, EventArgs e)
        {
            form.Visible = true;
        }

        private void StartButton_MouseClick(object sender, MouseEventArgs e)
        {
            //disables start button
            startButton.Enabled = false;

            //creates minefield template (object)
            MineFieldGenerator generator = new MineFieldGenerator(_width, _height, _howManyBombs);

            //draws field based on created template
            fieldArray = new Field[_width + 2, _height + 2];
            for (int i = 1; i < _height + 1; i++)
            {
                for (int j = 1; j < _width + 1; j++)
                {
                    fieldArray[j, i] = new Field(this, _positionX, _positionY, generator.allFieldIsMine[j, i], generator.allFieldBombsNearby[j, i], j, i);
                    _positionX += 25;
                }
                _positionY += 25; _positionX = 30;
            }
            _positionX = 30; _positionY = 30;

            //turns the stopwatch on after creating board
            timeBoard.StartTimeCount();
        }
        
        //
        //WIN CHECKING METHOD
        //
        public void CheckWin()
        {
            int fields = _width * _height;
            for (int i = 1; i < _width + 1; i++)
            {
                for (int j = 1; j < _height + 1; j++)
                {
                    if (fieldArray[i, j].isClicked)
                        fields--;
                }
            }

            if (fields != _howManyBombs) return;

            MessageBox.Show("Congratulations, you won in "+ timeBoard.StopTimeCount() +" seconds!");
            timeBoard.SaveBestResults(_difficulty);
            Close();
        }

        //
        //WHEN EMPTY FIELD CLICKED - ALSO SHOWS FIELDS AROUND IT
        //
        public void CheckNearFields(int fieldX, int fieldY)
        {
            int startX = (fieldX - 1), startY = (fieldY - 1);

            for (int i = startX; i < startX + 3; i++)
            {
                for (int j = startY; j < startY + 3; j++)
                {
                    //bool containing all conditions when function shouldn't be called
                    bool v = (i == fieldX && j == fieldY) || i == 0 || j == 0 || i == (_width + 1) || j == (_height + 1);

                    if (v) continue;

                    if (fieldArray[i, j].isClicked) continue;

                    if (fieldArray[i, j].bombsNearby != 0)
                    {
                        fieldArray[i, j].fieldButton.Text = fieldArray[i, j].bombsNearby.ToString();
                    }

                    fieldArray[i, j].fieldButton.Enabled = false;
                    fieldArray[i, j].isClicked = true;
                    fieldArray[i, j].fieldButton.BackColor = Color.FromArgb(255, 204, 204);
                    CheckWin();

                    //checks one of the fields around if is also empty
                    if (fieldArray[i, j].bombsNearby == 0)
                    {
                        CheckNearFields(i, j);
                    }
                }
            }
        }
    }
}
