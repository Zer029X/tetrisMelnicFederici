using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tetrisMelnicFederici {
    public class Grid {
        public static int[, ] grid = new int[23, 10];

        //Griglia contenente tutti i tetramini caduti
        public static int[, ] gridTetraminiCaduti = new int[23, 10];

        public Grid () {

        }
        public static void drawBorder () {
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
        public static void clearGrid () {
            for (int i = 0; i < 23; ++i) {
                for (int j = 0; j < 10; j++) {
                    Console.SetCursorPosition (2 * j, i);
                    Console.Write (" ");
                }

            }
        }
    }
}