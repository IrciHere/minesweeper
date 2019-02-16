using System;

namespace minesweeper
{
    internal class MineFieldGenerator
    {
        //utworzenie potrzebnych zmiennych
        private readonly Random _rng;
        private readonly int[] _fieldX;
        private readonly int[] _fieldY;
        public int[,] allFieldInt; public bool[,] allFieldBool;
        private int _minesNearby;
        private bool _repeat;

        //konstruktor "główny" - tworzy całe pole minowe
        public MineFieldGenerator(int width, int height, int amount)
        {
            _rng = new Random();
            allFieldInt = new int[width + 2, height + 2];
            allFieldBool = new bool[width + 2, height + 2];
            _fieldX = new int[amount]; _fieldY = new int[amount];
            GenerateMines(width, height, amount);
            GenerateField(width, height, amount);
        }

        //funkcja losująca pozycje min bez powtórzeń
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

        //tworzenie pola na podstawie danych
        private void GenerateField(int width, int height, int amount)
        {
            //pętla ustawiająca miny na odpowiednich miejscach
            for (int i = 0; i < amount; i++)
            {
                allFieldBool[_fieldX[i], _fieldY[i]] = true;
            }
            //sprawdzenie każdego pola po kolei na obecność min dookoła
            for (int i = 1; i <= height; i++)
            {
                for (int j = 1; j <= width; j++)
                {
                    GetNearestCells(j, i);
                }
            }
        }

        //funkcja sprawdzająca ilość min dookoła
        private void GetNearestCells(int cellX, int cellY)
        {
            if (!allFieldBool[cellX, cellY])
            {
                for (int k = (cellX - 1); k <= (cellX + 1); k++)
                {
                    for (int l = (cellY - 1); l <= (cellY + 1); l++)
                    {
                        if (allFieldBool[k, l])
                        {
                            _minesNearby++;
                        }
                    }
                }
                //tutaj zapisuje na polu ilość min
                allFieldInt[cellX, cellY] = _minesNearby;
                _minesNearby = 0;
            }
        }
    }
}
