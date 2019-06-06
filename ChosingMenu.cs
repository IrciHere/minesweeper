﻿using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace minesweeper
{
    public class ChoosingMenu : Form
    {
        public List<ScoreBoard> scoreBoard;
        private string _json;
        public TextBox widthBox, heightBox, minesBox;
        public Label labelEasy, labelMedium, labelHard, widthLabel, heightLabel, minesLabel;
        public Button buttonEasy, buttonMedium, buttonHard, buttonCustom, buttonHighScores;
        public Label label;
        public ChoosingMenu()
        {
            Size = new Size(300, 400);
            Text = "Choose Difficulty Level";

            //
            //EASY DIFFICULTY
            //
            buttonEasy = new Button
            {
                Size = new Size(100, 50),
                Location = new Point(25, 25),
                Text = "EASY",
                Parent = this,
            };
            buttonEasy.MouseClick += ButtonEasy_MouseClick;

            labelEasy = new Label
            {
                Size = new Size(100, 50),
                Location = new Point(150, 35),
                Text = "9x9 field,\n10 bombs",
                Font = new Font("Arial", 10)
            };
            Controls.Add(labelEasy);

            //
            //MEDIUM DIFFICULTY
            //
            buttonMedium = new Button
            {
                Size = new Size(100, 50),
                Location = new Point(25, 100),
                Text = "MEDIUM",
                Parent = this
            };
            buttonMedium.MouseClick += ButtonMedium_MouseClick;

            labelMedium = new Label
            {
                Size = new Size(100, 50),
                Location = new Point(150, 110),
                Text = "16x16 field,\n40 bombs",
                Font = new Font("Arial", 10)
            };
            Controls.Add(labelMedium);

            //
            //HARD DIFFICULTY
            //
            buttonHard = new Button
            {
                Size = new Size(100, 50),
                Location = new Point(25, 175),
                Text = "HARD",
                Parent = this
            };
            buttonHard.MouseClick += ButtonHard_MouseClick;

            labelHard = new Label
            {
                Size = new Size(100, 50),
                Location = new Point(150, 185),
                Text = "30x16 field,\n99 bombs",
                Font = new Font("Arial", 10)
            };
            Controls.Add(labelHard);

            //
            //CUSTOM DIFFICULTY
            //
            buttonCustom = new Button
            {
                Size = new Size(100, 50),
                Location = new Point(25, 250),
                Text = "CUSTOM",
                Parent = this
            };
            buttonCustom.MouseClick += ButtonCustom_MouseClick;


                //set width
            widthBox = new TextBox                              
            {
                Size = new Size(30, 15),
                Location = new Point(175, 235),
                MaxLength = 3,
            };
            Controls.Add(widthBox);

            widthLabel = new Label
            {
                Location = new Point(135, 237),
                Text = "Width:"
            };
            Controls.Add(widthLabel);

                //set height
            heightBox = new TextBox
            {
                Size = new Size(30, 15),
                Location = new Point(175, 260),
                MaxLength = 3,
            };
            Controls.Add(heightBox);

            heightLabel = new Label
            {
                Location = new Point(135, 262),
                Text = "Height:"
            };
            Controls.Add(heightLabel);

                //set mines
            minesBox = new TextBox
            {
                Size = new Size(30, 15),
                Location = new Point(175, 285),
                MaxLength = 3,
            };
            Controls.Add(minesBox);

            minesLabel = new Label
            {
                Location = new Point(135, 287),
                Text = "Mines:"
            };
            Controls.Add(minesLabel);


            buttonHighScores = new Button
            {
                Size = new Size(100, 30),
                Location = new Point(25, 325),
                Text = "HIGH SCORES",
                Parent = this,
            };
            buttonHighScores.MouseClick += ButtonHighScores_MouseClick;
        }

        //
        //METHODS USED AFTER PRESSING BUTTONS
        //
        private void ButtonEasy_MouseClick(object sender, MouseEventArgs e)
        {
            MainWindow mainWindow = new MainWindow(this, 9, 9, 10, "easy");
            Visible = false;
            mainWindow.ShowDialog();
        }

        private void ButtonMedium_MouseClick(object sender, MouseEventArgs e)
        {
            MainWindow mainWindow = new MainWindow(this, 16, 16, 40, "medium");
            Visible = false;
            mainWindow.ShowDialog();
        }

        private void ButtonHard_MouseClick(object sender, MouseEventArgs e)
        {
            MainWindow mainWindow = new MainWindow(this, 30, 16, 99, "hard");
            Visible = false;
            mainWindow.ShowDialog();
        }

        //
        //SECTION FOR CUSTOM FIELD GENERATOR
        //
        private void ButtonCustom_MouseClick(object sender, MouseEventArgs e)
        {
            if (CheckConditions())
            {
                int customWidth = int.Parse(widthBox.Text);
                int customHeight = int.Parse(heightBox.Text);
                int customMines = int.Parse(minesBox.Text);

                MainWindow mainWindow = new MainWindow(this, customWidth, customHeight, customMines, "");
                Visible = false;
                mainWindow.ShowDialog();
            }
            else
            {
                MessageBox.Show("NIEPRAWIDŁOWE WARTOŚCI");
            }
        }

        private void ButtonHighScores_MouseClick(object sender, MouseEventArgs e)
        {
            ImportScoreBoard();
            MessageBox.Show(PrintHighScores());
        }

        private bool CheckConditions()
        {
            if (!int.TryParse(widthBox.Text, out int width) || !int.TryParse(heightBox.Text, out int height) || !int.TryParse(widthBox.Text, out int mines))
                return false;

            if (width > 100 || width <= 0 || height > 100 || height <= 0 || mines <= 0) return false;

            return mines < width * height;
        }

        //
        //CHECHING HIGH SCORES
        //

        public void ImportScoreBoard()
        {
            //try reading a scoreboard from json file to a string
            try
            {
                using (StreamReader r = new StreamReader("scoreBoard.json"))
                {
                    _json = r.ReadToEnd();
                }
            }

            //if it doesn't exist, string will contain a scoreboard without any scores
            catch
            {
                _json = @"[
                             {   
                                 'difficulty': 'Easy',
                                 'bestTimes': []
                             },
                             {   
                                 'difficulty': 'Medium',
                                 'bestTimes': []
                             },
                             {   
                                 'difficulty': 'Hard',
                                 'bestTimes': []
                             }
                         ]";
            }

            scoreBoard = JsonConvert.DeserializeObject<List<ScoreBoard>>(_json);
        }

        public string PrintHighScores()
        {
            string result = "";
            result += "EASY\n";
            for (int i = 0; i < 10; i++)
            {
                if (scoreBoard[0].bestTimes.Count < i)
                {
                    result += (i + 1) + ". \n";
                    continue;
                }
                result += (i + 1) + ". " + (scoreBoard[0].bestTimes[i]).ToString() + "\n";
            }

            result += "\nMEDIUM\n";
            for (int i = 0; i < 10; i++)
            {
                if (scoreBoard[1].bestTimes.Count < (i + 1))
                {
                    result += (i + 1) + ". \n";
                    continue;
                }
                result += (i + 1) + ". " + (scoreBoard[1].bestTimes[i]).ToString() + "\n";
            }

            result += "\nHARD\n";
            for (int i = 0; i < 10; i++)
            {
                if (scoreBoard[2].bestTimes.Count < (i + 1))
                {
                    result += (i + 1) + ". \n";
                    continue;
                }
                result += (i + 1) + ". " + (scoreBoard[2].bestTimes[i]).ToString() + "\n";
            }

            return result;
        }
    }
}
