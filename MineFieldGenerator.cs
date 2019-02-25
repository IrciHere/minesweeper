using System;

namespace minesweeper
{
    internal class MineFieldGenerator
    {
        //
        //DECLARE VARIABLES NEEDED
        //
        private readonly Random _rng;
        private readonly int[] _fieldX;
        private readonly int[] _fieldY;
        public int[,] allFieldBombsNearby; public bool[,] allFieldIsMine;
        private int _minesNearby;
        private bool _repeat;

        //
        //'MAIN' CONSTRUCTOR - CREATES THE MINEFIELD
        //
        public MineFieldGenerator(int width, int height, int amount)
        {
            _rng = new Random();
            allFieldBombsNearby = new int[width + 2, height + 2];
            allFieldIsMine = new bool[width + 2, height + 2];
            _fieldX = new int[amount]; _fieldY = new int[amount];
            GenerateMines(width, height, amount);
            GenerateField(width, height, amount);
        }

        //
        //PICKS RANDOM FIELDS WITHOUT REPEATING
        //
        private void GenerateMines(int width, int height, int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                _repeat = false;
                _fieldX[i] = _rng.Next(1, width);
                _fieldY[i] = _rng.Next(1, height);
                for (int j = 0; j < i; j++)
                {
                    if (_fieldX[j] == _fieldX[i] && _fieldY[j] == _fieldY[i])
                        _repeat = true;
                }

                if (_repeat)
                    i--;
            }
        }

        //
        //CREATES FIELD BASED ON GIVEN DATA
        //
        private void GenerateField(int width, int height, int amount)
        {
            //puts mines on correct places
            for (int i = 0; i < amount; i++)
            {
                allFieldIsMine[_fieldX[i], _fieldY[i]] = true;
            }

            //counts mines around every single field
            for (int i = 1; i <= height; i++)
            {
                for (int j = 1; j <= width; j++)
                {
                    GetNearestCells(j, i);
                }
            }
        }

        //
        //COUNT MINES AROUND
        //
        private void GetNearestCells(int cellX, int cellY)
        {
            if (allFieldIsMine[cellX, cellY]) return;

            for (int k = (cellX - 1); k <= (cellX + 1); k++)
            {
                for (int l = (cellY - 1); l <= (cellY + 1); l++)
                {
                    if (allFieldIsMine[k, l])
                    {
                        _minesNearby++;
                    }
                }
            }

            //saves number of bonbs around on a field
            allFieldBombsNearby[cellX, cellY] = _minesNearby;
            _minesNearby = 0;
        }
    }
}
