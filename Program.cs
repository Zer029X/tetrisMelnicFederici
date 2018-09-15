using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Media;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace tetrisMelnicFederici {
    class Program {
        public static GameManager manager;

        // Variabili per le key


        static void Main (string[] args) {
            manager = new GameManager ();
            bool continua = true;
            while (continua) {
                manager.iniziaGioco ();
                continua = manager.gameOver ();
            }
        } // End Main
    }
}