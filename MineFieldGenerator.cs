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
        Random rng;
        int[] fieldX, fieldY;
        public int[,] allFieldInt; public bool[,] allFieldBool;
        int minesNearby;
        bool repeat;
        public MineFieldGenerator(int width, int height, int amount)
        {
            rng = new Random();
            allFieldInt = new int[width + 2, height + 2];
            allFieldBool = new bool[width+2, height+2];
            fieldX = new int[amount]; fieldY = new int[amount];
            generateMines(width, height, amount);
            generateField(width, height, amount);
        }

        private void generateMines(int width, int height, int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                repeat = false;
                fieldX[i] = rng.Next(1, width);
                fieldY[i] = rng.Next(1, height);
                {
                    if (fieldX[j] == fieldX[i] && fieldY[j] == fieldY[i])
                        repeat = true;
                }

                if (repeat)
                    i--;
            }
        }

        private void generateField(int width, int height, int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                allFieldBool[fieldX[i], fieldY[i]] = true;
            }
            for (int i=1; i<=height; i++)
            {
                for (int j=1; j<=width; j++)
                {
                    getNearestCells(j, i);
                    Debug.WriteLine(i + " " + j);
                }
            }
        }

        private void getNearestCells(int cellX, int cellY)
        {
            if (!allFieldBool[cellX, cellY])
            {
                Debug.WriteLine("Jestem tu");
                for (int k = (cellX - 1); k <= (cellX + 1); k++)
                {
                    for (int l = (cellY - 1); l <= (cellY + 1); l++)
                    {
                        Debug.WriteLine(k + " " + l);
                        if (allFieldBool[k, l])
                        {
                            minesNearby++;
                        }
                    }
                }
                allFieldInt[cellX, cellY] = minesNearby;
                minesNearby = 0;
            } 
        }
    }
}
