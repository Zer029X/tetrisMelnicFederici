using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tetrisMelnicFederici {
    public static class Output {
        public static void stampaInfo () {
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
        public static void stampaStats (int livello, int punti, int lineeTot) {
            Console.SetCursorPosition (25, 0);
            Console.WriteLine ("Livello: " + livello);
            Console.SetCursorPosition (25, 1);
            Console.WriteLine ("Punti: " + punti);
            Console.SetCursorPosition (25, 2);
            Console.WriteLine ("Linee Completate: " + lineeTot);
        }

        public static void disegnaBordi () {
            for (int lengthCount = 0; lengthCount <= 22; ++lengthCount) {
                Console.SetCursorPosition (0, lengthCount);
                Console.Write ("|");
                Console.SetCursorPosition (20, lengthCount);
                Console.Write ("|");
            }
            Console.SetCursorPosition (0, 23);
            for (int widthCount = 0; widthCount < 21; widthCount++) {
                Console.Write ("=");
            }
        }

        public static void stampaInizio () {
            disegnaBordi ();
            Console.SetCursorPosition (4, 5);
            Console.WriteLine ("Premi un tasto");
            Console.SetCursorPosition (6, 6);
            Console.WriteLine ("qualunque");
            stampaStats (0, 0, 0);
            stampaInfo ();
        }

        public static void pulisciCampo () {
            for (int i = 0; i < 23; ++i) {
                for (int j = 0; j < 10; j++) {
                    Console.SetCursorPosition (2 * j, i);
                    Console.Write (" ");
                }

            }
            disegnaBordi ();
        }

        public static void disegnaBlocchi (Grid griglia, Grid grigliaTetraminiCaduti) {
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

        public static void stampaGameOver () {
            Console.SetCursorPosition (4, 7);
            Console.WriteLine ("Game Over");
            Console.SetCursorPosition (3, 8);
            Console.Write ("Rigiocare? (S/N)");
        }

        public static void disegnaProxForma (int[, ] matriceTetramino) {
            for (int i = 23; i < 33; ++i) {
                for (int j = 3; j < 10; j++) {
                    Console.SetCursorPosition (i, j);
                    Console.Write (" ");
                }

            }
            // griglia.disegnaBordi ();
            for (int i = 0; i < matriceTetramino.GetLength (0); i++) {
                for (int j = 0; j < matriceTetramino.GetLength (1); j++) {
                    if (matriceTetramino[i, j] == 1) {
                        Console.SetCursorPosition (((10 - matriceTetramino.GetLength (1)) / 2 + j) * 2 + 20, i + 5);
                        Console.Write ("\u25A0");
                    }
                }
            }
        }
        public static void fixGriglia () {
            Console.SetCursorPosition (20, 22);
            Console.Write ("|");
            Console.SetCursorPosition (20, 22);
        }
    }
}