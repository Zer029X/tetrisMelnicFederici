using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrisGame
{
    class Grid
    {
        private static int[,] grid = new int[20, 10];
        /*public static void fillGrid()
        {
            for (int i = 0; i < 23; i++)
            {
                Console.SetCursorPosition(1, i);
                for(int j = 0; j < 10; j++)
                {
                    Console.Write(sqr);
                }
                Console.WriteLine();
            }
        }*/
        public void drawBorder()
        {
            for (int lengthCount = 0; lengthCount <= 19; ++lengthCount)
            {
                Console.SetCursorPosition(0, lengthCount);
                Console.Write("*");
                Console.SetCursorPosition(18, lengthCount);
                Console.Write("*");
            }
            Console.SetCursorPosition(0, 20);
            for (int widthCount = 0; widthCount <= 10; widthCount++)
            {
                Console.Write("*-");
            }
        }
    }
}
