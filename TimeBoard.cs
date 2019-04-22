using System;
using System.Collections.Generic;
using System.IO;
using System.Timers;
using System.Drawing;
using Newtonsoft.Json;

namespace minesweeper
{
    public class TimeBoard
    {
        //
        //DECLARE VARIABLES NEEDED
        //
        public delegate void NextPrimeDelegate();
        public List<ScoreBoard> scoreBoard;
        private readonly Timer _timer;
        private int _timerTime;
        private string _json;
        private MainWindow _mainWindow;

        //constructor only imports json from a file
        public TimeBoard(MainWindow mainWindow)
        {
            ImportScoreBoard();
            _mainWindow = mainWindow;

            _timer = new Timer(1000);
            _timer.Elapsed += TimerOnElapsed;
            _timer.AutoReset = true;

        }

        private void TimerOnElapsed(object sender, ElapsedEventArgs e)
        {
            _timerTime += 1;
        }

        public void StartTimeCount()
        {
            _timerTime = 0;
            _timer.Close();
            _timer.Start();
        }

        public int StopTimeCount()
        {
            _timer.Stop();
            return _timerTime;
        }

        //
        //IMPORTS SCORE BOARD FROM A JSON FILE (IF IT EXISTS)
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

        //
        //SAVES BEST RESULTS IN JSON FILE
        //
        public void SaveBestResults(string difficulty)
        {
            //saves score to correct diffitulty scoreboard
            switch (difficulty)
            {
                case "easy":
                    scoreBoard[0].bestTimes.Add(_timerTime);
                    scoreBoard[0].bestTimes.Sort();
                    break;
                case "medium":
                    scoreBoard[1].bestTimes.Add(_timerTime);
                    scoreBoard[1].bestTimes.Sort();
                    break;
                case "hard":
                    scoreBoard[2].bestTimes.Add(_timerTime);
                    scoreBoard[2].bestTimes.Sort();
                    break;
                default:
                    return;
            }

            //writes scoreboard object to a file
            using (StreamWriter file = File.CreateText("scoreBoard.json"))
            {
                _json = JsonConvert.SerializeObject(scoreBoard, Formatting.Indented);
                file.Write(_json);
            }
        }
    }
}

