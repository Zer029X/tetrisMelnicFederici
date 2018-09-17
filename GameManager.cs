using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tetrisMelnicFederici {
    class GameManager {
        #region attributi tetramini e griglie

        private static List<char> lettere;
        private static Random rnd;
        public static Tetramino nextTetramino;
        public static Tetramino tetramino;

        public static Grid griglia;
        public static Grid grigliaTetraminiCaduti;

        #endregion
        #region  game parameters
        private Stopwatch timer;
        private Stopwatch timerCaduta;
        private Stopwatch timerInput;
        private int tempoCaduta;
        private int cadenzaCaduta;
        private int lineeTot;
        private int livello;
        private int punti;

        private bool isPaused;

        #endregion
        #region INPUT
        public static ConsoleKeyInfo key;
        public static bool isKeyPressed;
        #endregion
        #region Constructor
        public GameManager () {

            lettere = new List<char> () { 'I', 'O', 'T', 'S', 'Z', 'J', 'L' };
            rnd = new Random ();
            griglia = new Grid ();
            grigliaTetraminiCaduti = new Grid ();
            nextTetramino = new Tetramino (lettere[rnd.Next (0, 7)], griglia, grigliaTetraminiCaduti);
            tetramino = nextTetramino;
            // tetramino = new Tetramino (lettere[rnd.Next (0, 7)], griglia, grigliaTetraminiCaduti);
            nextTetramino = new Tetramino (lettere[rnd.Next (0, 7)], griglia, grigliaTetraminiCaduti);

        }
        #endregion

        #region Methods

        private void inizializzaParametri () {
            timer = new Stopwatch ();
            timerCaduta = new Stopwatch ();
            timerInput = new Stopwatch ();
            punti = 0;
            tempoCaduta = 300;
            cadenzaCaduta = 300;
            lineeTot = 0;
            livello = 1;
            grigliaTetraminiCaduti.resetGrigliaTetraminiCaduti ();
            isKeyPressed = false;
            isPaused = false;
            long time = timer.ElapsedMilliseconds;
        }
        public void iniziaGioco () {
            inizializzaParametri ();
            griglia.disegnaBordi ();
            Console.SetCursorPosition (4, 5);
            Console.WriteLine ("Premi un tasto");
            Console.SetCursorPosition (6, 6);
            Console.WriteLine ("qualunque");
            Console.ReadKey (true);
            griglia.pulisciGriglia ();
            timer.Start ();
            timerCaduta.Start ();
            scriviInfo ();
            tetramino.Spawn ();
            disegnaGriglia ();
            aggiorna ();
        }
        public bool gameOver () {
            bool continua;
            grigliaTetraminiCaduti.resetGrigliaTetraminiCaduti ();
            disegnaGriglia ();
            Console.SetCursorPosition (4, 7);
            Console.WriteLine ("Game Over");
            Console.SetCursorPosition (3, 8);
            Console.Write ("Rigiocare? (S/N)");
            string input = Console.ReadLine ();
            input = input.Replace (" ", String.Empty);
            if (input == "s" || input == "S") {

                Console.Clear ();
                continua = true;
            } else
                continua = false;

            return continua;
        }
        private void scriviPunti () {
            Console.SetCursorPosition (25, 0);
            Console.WriteLine ("Livello: " + livello);
            Console.SetCursorPosition (25, 1);
            Console.WriteLine ("Punti: " + punti);
            Console.SetCursorPosition (25, 2);
            Console.WriteLine ("Linee Completate: " + lineeTot);
        }
        private void scriviInfo () {
            scriviPunti ();
            Console.SetCursorPosition (25, 10);
            Console.Write ("Controlli:");
            Console.SetCursorPosition (25, 11);
            Console.Write (" - Freccia SX/ DX:       sposta a sx/ dx");
            Console.SetCursorPosition (25, 12);
            Console.Write (" - Freccia Sù:           ruota pezzo");
            Console.SetCursorPosition (25, 13);
            Console.Write (" - Freccia Giù:          aumenta la velocita di caduta");
            Console.SetCursorPosition (25, 14);
            Console.Write (" - Barra Spaziatrice:    fai cadere il pezzo");
            Console.SetCursorPosition (25, 15);
            Console.Write (" - P:                    attiva/ disattiva pausa");
        }
        private void clearLinea () {
            int combo = 0;
            for (int i = 0; i < 23; i++) {
                int j;
                for (j = 0; j < 10; j++) {
                    if (grigliaTetraminiCaduti.getElementoMatrice (i, j) == 0)
                        break;
                }
                if (j == 10) {
                    lineeTot++;
                    combo++;
                    for (j = 0; j < 10; j++) {
                        grigliaTetraminiCaduti.setElementoMatrice (i, j, 0);
                    }
                    int[, ] newdGridTetraminiCaduti = new int[23, 10];
                    for (int k = 1; k < i; k++) {
                        for (int l = 0; l < 10; l++) {
                            newdGridTetraminiCaduti[k + 1, l] = grigliaTetraminiCaduti.getElementoMatrice (k, l);
                        }
                    }
                    for (int k = 1; k < i; k++) {
                        for (int l = 0; l < 10; l++) {
                            grigliaTetraminiCaduti.setElementoMatrice (k, l, 0);
                        }
                    }
                    for (int k = 0; k < 23; k++)
                        for (int l = 0; l < 10; l++)
                            if (newdGridTetraminiCaduti[k, l] == 1)
                                grigliaTetraminiCaduti.setElementoMatrice (k, l, 1);
                    disegnaGriglia ();
                }
            }
            if (combo == 1)
                punti += 50 * livello;
            else if (combo == 2)
                punti += 100 * livello;
            else if (combo == 3)
                punti += 300 * livello;
            else if (combo > 3)
                punti += 300 * combo * livello;

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

            if (combo > 0) {
                scriviPunti ();
            }

            cadenzaCaduta = 300 - 10 * livello;
        }
        private void disegnaGriglia () {
            for (int i = 0; i < 23; ++i) {
                for (int j = 0; j < 10; j++) {
                    Console.SetCursorPosition (1 + 2 * j, i);
                    if (griglia.getElementoMatrice (i, j) == 1 |
                        grigliaTetraminiCaduti.getElementoMatrice (i, j) == 1) {
                        Console.Write ("\u25A0");
                    } else {
                        Console.Write (" ");
                    }
                }

            }
        }
        private void aggiorna () {
            bool isGameOver = false;
            while (!isGameOver) {
                tempoCaduta = (int) timerCaduta.ElapsedMilliseconds;
                if (tempoCaduta > cadenzaCaduta) {
                    tempoCaduta = 0;
                    timerCaduta.Restart ();

                    tetramino.Cade ();
                    disegnaGriglia ();
                }
                if (tetramino.checkCaduto ()) {
                    tetramino = nextTetramino;
                    nextTetramino = new Tetramino (lettere[rnd.Next (0, 7)], griglia, grigliaTetraminiCaduti);
                    tetramino.Spawn ();
                    disegnaGriglia ();
                }
                int j;
                for (j = 0; j < 10; j++) {
                    if (grigliaTetraminiCaduti.getElementoMatrice (0, j) == 1)
                        isGameOver = true;
                }
                clearLinea ();
                CheckInput ();
            }
        }

        private void CheckInput () {
            if (Console.KeyAvailable) {
                key = Console.ReadKey ();
                isKeyPressed = true;
            } else
                isKeyPressed = false;

            if (!isPaused & key.Key == ConsoleKey.LeftArrow & !tetramino.ceQualcosaSx () & isKeyPressed) {
                for (int i = 0; i < 4; i++) {
                    tetramino.posizioni[i][1] -= 1;
                }
                tetramino.Aggiorna ();
                disegnaGriglia ();
                Console.Beep ();
            } else if (!isPaused & key.Key == ConsoleKey.RightArrow & !tetramino.ceQualcosaDx () & isKeyPressed) {
                for (int i = 0; i < 4; i++) {
                    tetramino.posizioni[i][1] += 1;
                }
                tetramino.Aggiorna ();
                disegnaGriglia ();
            }
            if (!isPaused & key.Key == ConsoleKey.DownArrow & isKeyPressed) {
                tetramino.Cade ();
                disegnaGriglia ();
            }
            if (!isPaused & key.Key == ConsoleKey.Spacebar & isKeyPressed) {
                for (; tetramino.ceQualcosaSotto () != true;) {
                    tetramino.Cade ();
                    disegnaGriglia ();
                }
            }
            if (!isPaused & key.Key == ConsoleKey.UpArrow & isKeyPressed) {
                tetramino.Rotazione ();
                tetramino.Aggiorna ();
                disegnaGriglia ();
            }
            if (key.Key == ConsoleKey.P & isKeyPressed) {
                if (!isPaused) {
                    timer.Stop ();
                    timerCaduta.Stop ();
                    isPaused = true;
                } else {
                    timer.Start ();
                    timerCaduta.Start ();
                    isPaused = false;
                }

            }

        }
        #endregion
    }
}