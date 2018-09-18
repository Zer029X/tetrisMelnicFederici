using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tetrisMelnicFederici {
    public static class Input {

        #region INPUT
        public static ConsoleKeyInfo key;
        public static bool isKeyPressed = false;
        #endregion

        public static void getKey () {
            if (Console.KeyAvailable) {
                key = Console.ReadKey ();
                isKeyPressed = true;
            } else
                isKeyPressed = false;

        }
        public static char getGameOverInput () {
            string input = Console.ReadLine ();
            input = input.Replace (" ", String.Empty);
            Console.Clear ();
            return input.ElementAt (0);
        }
    }
}