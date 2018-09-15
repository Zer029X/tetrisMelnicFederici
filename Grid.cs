using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tetrisMelnicFederici {
    public class Grid {
        private int[, ] matriceGriglia;
        public Grid () {
            matriceGriglia = new int[23, 10];
        }
        public void disegnaBordi () {
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
        public void pulisciGriglia () {

            for (int i = 0; i < 23; ++i) {
                for (int j = 0; j < 10; j++) {
                    Console.SetCursorPosition (2 * j, i);
                    Console.Write (" ");
                }

            }
            disegnaBordi();
        }

        public int getElementoMatrice (int i, int j) {
            return matriceGriglia[i, j];
        }

        public void setElementoMatrice (int i, int j, int data) {
            matriceGriglia[i, j] = data;
        }

        public void resetGrigliaTetraminiCaduti () {
            for (int i = 0; i < 23; i++) {
                for (int j = 0; j < 10; j++) {
                    matriceGriglia[i, j] = 0;
                }
            }
        }
    }

}