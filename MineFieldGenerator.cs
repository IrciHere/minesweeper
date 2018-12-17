using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace minesweeper
{
    class MineFieldGenerator
    {
        //utworzenie potrzebnych zmiennych
        Random rng;
        int[] fieldX, fieldY;
        public int[,] allFieldInt; public bool[,] allFieldBool;
        int minesNearby;
        bool repeat;

        //konstruktor "główny" - tworzy całe pole minowe
        public MineFieldGenerator(int width, int height, int amount)
        {
            rng = new Random();
            allFieldInt = new int[width + 2, height + 2];
            allFieldBool = new bool[width+2, height+2];
            fieldX = new int[amount]; fieldY = new int[amount];
            GenerateMines(width, height, amount);
            GenerateField(width, height, amount);
        }

        //funkcja losująca pozycje min bez powtórzeń
        private void GenerateMines(int width, int height, int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                repeat = false;
                fieldX[i] = rng.Next(1, width);
                fieldY[i] = rng.Next(1, height);
                for (int j = 0; j < i; j++)
                {
                    if (fieldX[j] == fieldX[i] && fieldY[j] == fieldY[i])
                        repeat = true;
                }

                if (repeat)
                    i--;
            }
        }

        //tworzenie pola na podstawie danych
        private void GenerateField(int width, int height, int amount)
        {
            //pętla ustawiająca miny na odpowiednich miejscach
            for (int i = 0; i < amount; i++)
            {
                allFieldBool[fieldX[i], fieldY[i]] = true;
            }
            //sprawdzenie każdego pola po kolei na obecność min dookoła
            for (int i=1; i<=height; i++)
            {
                for (int j=1; j<=width; j++)
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
                            minesNearby++;
                        }
                    }
                }
                //tutaj zapisuje na polu ilość min
                allFieldInt[cellX, cellY] = minesNearby;
                minesNearby = 0;
            } 
        }
    }
}
