using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrisGame
{
    class Program
    {
        static Tetramino nextForma;
        static Tetramino forma;

        static void Main(string[] args)
        {
            nextForma = new Tetramino();
            forma = nextForma;
            forma.Spawn();
            nextForma = new Tetramino();
            while (true)
            {
                Grid gameGrid = new Grid();
                Grid.drawBorder();
                
            }
            
        }

        public static void Disegna()
        {
            for (int i = 0; i < 19; ++i)
            {
                for (int j = 0; j < 10; j++)
                {
                    Console.SetCursorPosition(1 + 2 * j, i);
                    if (Grid.grid[i, j] == 1 /*| droppedtetrominoeLocationGrid[i, j] == 1*/)
                    {
                        Console.SetCursorPosition(1 + 2 * j, i);
                        Console.Write("■");
                    }
                    else
                    {
                        Console.Write("  ");
                    }
                }

            }
        }
    }
}
