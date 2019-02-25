using System.Collections.Generic;

namespace minesweeper
{
    //
    //SIMPLE CLASS ONLY HOLDING A SCOREBOARD
    //
    public class ScoreBoard
    {
        public string difficulty;
        public List<int> bestTimes;

        public ScoreBoard(string difficulty, List<int> bestTimes)
        {
            this.difficulty = difficulty;
            this.bestTimes = bestTimes;
        }
    }
}
