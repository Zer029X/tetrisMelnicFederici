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