using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrisGame
{
    public class Grid
    {
        public static int[,] grid = new int[23, 10];
        
        //Griglia contenente tutti i tetramini caduti
        public static int[,] gridTetraminiCaduti = new int[23, 10];

        public Grid()
        {

        }
        public static void drawBorder()
        {
            for (int lengthCount = 0; lengthCount <= 22; ++lengthCount)
            {
                Console.SetCursorPosition(0, lengthCount);
                Console.Write("|");
                Console.SetCursorPosition(21, lengthCount);
                Console.Write("|");
            }
            Console.SetCursorPosition(0, 23);
            for (int widthCount = 0; widthCount <= 10; widthCount++)
            {
                Console.Write("==");
            }
        }
    }
}
