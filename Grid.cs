using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrisGame
{
    class Grid
    {
        public static int[,] grid = new int[20, 10];
        public Grid()
        {

        }
        public static void drawBorder()
        {
            for (int lengthCount = 0; lengthCount <= 20; ++lengthCount)
            {
                Console.SetCursorPosition(0, lengthCount);
                Console.Write("|" + lengthCount);
                Console.SetCursorPosition(21, lengthCount);
                Console.Write("|");
            }
            Console.SetCursorPosition(0, 20);
            for (int widthCount = 0; widthCount <= 10; widthCount++)
            {
                Console.Write("--");
            }
        }
    }
}
