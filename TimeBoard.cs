using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace minesweeper
{
    public class TimeBoard
    {
        //
        //DECLARE VARIABLES NEEDED
        //
        public List<ScoreBoard> scoreBoard;
        private readonly Stopwatch _stopwatch = new Stopwatch();
        private TimeSpan _timeSpan;
        private string _json;

        //constructor only imports json from a file
        public TimeBoard()
        {
            ImportScoreBoard();
        }

        public void StartTimeCount()
        {
            _stopwatch.Reset();
            _stopwatch.Start();
        }

        public int StopTimeCount()
        {
            _stopwatch.Stop();
            _timeSpan = _stopwatch.Elapsed;
            return (int)_timeSpan.TotalSeconds;
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
                    scoreBoard[0].bestTimes.Add((int)_timeSpan.TotalSeconds);
                    scoreBoard[0].bestTimes.Sort();
                    break;
                case "medium":
                    scoreBoard[1].bestTimes.Add((int)_timeSpan.TotalSeconds);
                    scoreBoard[1].bestTimes.Sort();
                    break;
                case "hard":
                    scoreBoard[2].bestTimes.Add((int)_timeSpan.TotalSeconds);
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

