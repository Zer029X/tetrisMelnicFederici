using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrisGame
{
    public class Tetramino

    {
        //  Crea tutti i tipi di tetramino
        private static int[,] I = new int[1, 4] { { 1, 1, 1, 1 } };
        private static int[,] O = new int[2, 2] { { 1, 1 }, { 1, 1 } };
        private static int[,] T = new int[2, 3] { { 0, 1, 0 }, { 1, 1, 1 } };
        private static int[,] S = new int[2, 3] { { 0, 1, 1 }, { 1, 1, 0 } };
        private static int[,] Z = new int[2, 3] { { 1, 1, 0 }, { 0, 1, 1 } };
        private static int[,] J = new int[2, 3] { { 1, 0, 0 }, { 1, 1, 1 } };
        private static int[,] L = new int[2, 3] { { 0, 0, 1 }, { 1, 1, 1 } };
        // Gli aggiunge in una lista
        private List<int[,]> tetramini= new List<int[,]>() { I, O, T, S, Z, J, L };
        private List<int[]> location = new List<int[]>();
        private int[,] tetramino;
        private Random rnd = new Random();

        public Tetramino()
        {
            tetramino = tetramini[rnd.Next(0, 7)];
            disegnaProxForma();
            
        }

        private void disegnaProxForma()
        {
            for (int i = 23; i < 33; ++i)
            {
                for (int j = 3; j < 10; j++)
                {
                    Console.SetCursorPosition(i, j);
                    Console.Write("  ");
                }

            }
            Grid.drawBorder();
            for (int i = 0; i < tetramino.GetLength(0); i++)
            {
                for (int j = 0; j < tetramino.GetLength(1); j++)
                {
                    if (tetramino[i, j] == 1)
                    {
                        Console.SetCursorPosition(((10 - tetramino.GetLength(1)) / 2 + j) * 2 + 20, i + 5);
                        Console.Write("■");
                    }
                }
            }
        }

        public void Spawn()
        {
            for (int i = 0; i < tetramino.GetLength(0); i++)
            {
                for (int j = 0; j < tetramino.GetLength(1); j++)
                {
                    if (tetramino[i, j] == 1)
                    {
                        location.Add(new int[] { i, (10 - tetramino.GetLength(1)) / 2 + j });
                    }
                }
            }
            Aggiorna();
        }


        public void Aggiorna()
        {
            for (int i = 0; i < 19; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    Grid.grid[i, j] = 0;
                }
            }
            for (int i = 0; i < 4; i++)
            {
                Grid.grid[location[i][0], location[i][1]] = 1;
            }
            Program.Disegna();
        }
    }
}
