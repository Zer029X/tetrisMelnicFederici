using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Timers;
using System.Diagnostics;


using System.Media;
using System.Resources;
using System.IO;
using System.Reflection;


namespace TetrisGame
{
    class Program
    {
        // Tetramini
        public static Tetramino nextForma;
        public static Tetramino forma;
        
        // Statistiche di gioco
        public static int lineeTot = 0, 
                          score = 0,
                          livello = 1;

        // Timer di gioco, timer di caduta, timer input
        public static Stopwatch timer = new Stopwatch(),
                                timerCaduta = new Stopwatch(),
                                inputTimer = new Stopwatch();

        public static int tempoCaduta,          // Tempo di caduta del tetramino
                          rateCaduta = 300;     // Tempo effettivo di un tetramino che cade

        // Variabile booleana che ci indica se il tetramino è caduto o no
        public static bool caduto = false;

        // Variabili per le key
        public static ConsoleKeyInfo key;
        public static bool isKeyPressed = false;
        
        static void Main(string[] args)
        {
            Grid.drawBorder();
            Console.SetCursorPosition(4, 5);
            Console.WriteLine("Premi un tasto");
            Console.SetCursorPosition(6, 6);
            Console.WriteLine("qualunque");

            Console.ReadKey(true);

            // Premuto un tasto parte il gioco con i rispettivi timer
            timer.Start();
            timerCaduta.Start();

            // Scrittura in console di tutte le informazioni di gioco
            Console.SetCursorPosition(25, 0);
            Console.WriteLine("Livello: " + livello);
            Console.SetCursorPosition(25, 1);
            Console.WriteLine("Score: " + score);
            Console.SetCursorPosition(25, 2);
            Console.WriteLine("Linee Completate: " + lineeTot);

            // Si creano i due tetramini 
            // tetramino 1 (forma): forma che dovrà scendere
            // tetramino 2 (nextForma): forma che spawnerà dopo che "forma" toccherà la base
            nextForma = new Tetramino();
            forma = nextForma;
            forma.Spawn();
            nextForma = new Tetramino();

            Aggiorna();

            Console.SetCursorPosition(4, 5);
            Console.WriteLine("GAME OVER");
        }// End Main

        private static void Aggiorna()
        {
            while (true)//Update Loop
            {
                tempoCaduta = (int)timerCaduta.ElapsedMilliseconds;
                if (tempoCaduta > rateCaduta)
                {
                    //se il tempo di caduta è maggiore di 300 allora significa che il pezzo sta cadendo caduto
                    tempoCaduta = 0;
                    timerCaduta.Restart();

                    forma.Cade();
                }
                if (caduto == true)
                {
                    forma = nextForma;
                    nextForma = new Tetramino();
                    forma.Spawn();

                    caduto = false;
                }
                int j;
                for (j = 0; j < 10; j++)
                {
                    if (Grid.gridTetraminiCaduti[0, j] == 1)
                        return;
                }

                CheckInput();
                ClearBlock();
            } //end Loop
        }// End aggiorna
        
        public static void Disegna()
        {
            for (int i = 0; i < 23; ++i)
            {
                for (int j = 0; j < 10; j++)
                {
                    Console.SetCursorPosition(1 + 2 * j, i);
                    if (Grid.grid[i, j] == 1 | Grid.gridTetraminiCaduti[i, j] == 1)
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
        }// End Disegna

        public static void CheckInput()
        {
            if (Console.KeyAvailable)
            {
                key = Console.ReadKey();
                isKeyPressed = true;
            }
            else
                isKeyPressed = false;

            if (key.Key == ConsoleKey.LeftArrow & !forma.ceQualcosaSx() & isKeyPressed)
            {
                for (int i = 0; i < 4; i++)
                {
                    forma.location[i][1] -= 1;
                }
                forma.Aggiorna();
                // Console.Beep();
            }
            else if (key.Key == ConsoleKey.RightArrow & !forma.ceQualcosaDx() & isKeyPressed)
            {
                for (int i = 0; i < 4; i++)
                {
                    forma.location[i][1] += 1;
                }
                forma.Aggiorna();
            }
            if (key.Key == ConsoleKey.DownArrow & isKeyPressed)
            {
                forma.Cade();
            }
            if (key.Key == ConsoleKey.UpArrow & isKeyPressed)
            {
                for (; forma.ceQualcosaSotto() != true;)
                {
                    forma.Cade();
                }
            }
            if (Program.key.Key == ConsoleKey.Spacebar & isKeyPressed)
            {
                //rotate
                /*tet.Rotate();
                tet.Update();*/
            }
        }// End Input

        private static void ClearBlock()
        {
            int combo = 0;
            for (int i = 0; i < 23; i++)
            {
                int j;
                for (j = 0; j < 10; j++)
                {
                    if (Grid.gridTetraminiCaduti[i, j] == 0)
                        break;
                }
                if (j == 10)
                {
                    lineeTot++;
                    combo++;
                    for (j = 0; j < 10; j++)
                    {
                        Grid.gridTetraminiCaduti[i, j] = 0;
                    }
                    int[,] newdGridTetraminiCaduti = new int[23, 10];
                    for (int k = 1; k < i; k++)
                    {
                        for (int l = 0; l < 10; l++)
                        {
                            newdGridTetraminiCaduti[k + 1, l] = Grid.gridTetraminiCaduti[k, l];
                        }
                    }
                    for (int k = 1; k < i; k++)
                    {
                        for (int l = 0; l < 10; l++)
                        {
                            Grid.gridTetraminiCaduti[k, l] = 0;
                        }
                    }
                    for (int k = 0; k < 23; k++)
                        for (int l = 0; l < 10; l++)
                            if (newdGridTetraminiCaduti[k, l] == 1)
                                Grid.gridTetraminiCaduti[k, l] = 1;
                    Disegna();
                }
            }
            if (combo == 1)
                score += 40 * livello;
            else if (combo == 2)
                score += 100 * livello;
            else if (combo == 3)
                score += 300 * livello;
            else if (combo > 3)
                score += 300 * combo * livello;

            if (lineeTot < 5) livello = 1;
            else if (lineeTot < 10) livello = 2;
            else if (lineeTot < 15) livello = 3;
            else if (lineeTot < 25) livello = 4;
            else if (lineeTot < 35) livello = 5;
            else if (lineeTot < 50) livello = 6;
            else if (lineeTot < 70) livello = 7;
            else if (lineeTot < 90) livello = 8;
            else if (lineeTot < 110) livello = 9;
            else if (lineeTot < 150) livello = 10;


            if (combo > 0)
            {
                Console.SetCursorPosition(25, 0);
                Console.WriteLine("Livello: " + livello);
                Console.SetCursorPosition(25, 1);
                Console.WriteLine("Score: " + score);
                Console.SetCursorPosition(25, 2);
                Console.WriteLine("Linee Completate: " + lineeTot);
            }

            rateCaduta = 300 - 22 * livello;
        }// End ClearBox
    }// End Classe
}
