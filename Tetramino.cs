using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tetrisMelnicFederici {
    public class Tetramino {
        //  Crea tutti i tipi di tetramino
        private static int[, ] I = new int[1, 4] { { 1, 1, 1, 1 } };
        private static int[, ] O = new int[2, 2] { { 1, 1 }, { 1, 1 } };
        private static int[, ] T = new int[2, 3] { { 0, 1, 0 }, { 1, 1, 1 } };
        private static int[, ] S = new int[2, 3] { { 0, 1, 1 }, { 1, 1, 0 } };
        private static int[, ] Z = new int[2, 3] { { 1, 1, 0 }, { 0, 1, 1 } };
        private static int[, ] J = new int[2, 3] { { 1, 0, 0 }, { 1, 1, 1 } };
        private static int[, ] L = new int[2, 3] { { 0, 0, 1 }, { 1, 1, 1 } };
        // Gli aggiunge in una lista
        private static List<int[, ]> tetramini = new List<int[, ]> () { I, O, T, S, Z, J, L };
        public List<int[]> posizioni = new List<int[]> ();
        private int[, ] tetramino;
        private Random rnd = new Random ();
        private bool verticale = false;

        public Tetramino () {
            tetramino = tetramini[rnd.Next (0, 7)];
            disegnaProxForma ();
        } // End Costruttore

        private void disegnaProxForma () {
            for (int i = 23; i < 33; ++i) {
                for (int j = 3; j < 10; j++) {
                    Console.SetCursorPosition (i, j);
                    Console.Write (" ");
                }

            }
            Grid.drawBorder ();
            for (int i = 0; i < tetramino.GetLength (0); i++) {
                for (int j = 0; j < tetramino.GetLength (1); j++) {
                    if (tetramino[i, j] == 1) {
                        Console.SetCursorPosition (((10 - tetramino.GetLength (1)) / 2 + j) * 2 + 20, i + 5);
                        Console.Write ("\u25A0");
                    }
                }
            }
        } // End disegnaProxForma

        public void Spawn () {
            for (int i = 0; i < tetramino.GetLength (0); i++) {
                for (int j = 0; j < tetramino.GetLength (1); j++) {
                    if (tetramino[i, j] == 1) {
                        posizioni.Add (new int[] { i, (10 - tetramino.GetLength (1)) / 2 + j });
                    }
                }
            }
            Aggiorna ();
        } // End Spawn

        public void Aggiorna () {
            for (int i = 0; i < 23; i++) {
                for (int j = 0; j < 10; j++) {
                    Grid.grid[i, j] = 0;
                }
            }
            for (int i = 0; i < 4; i++) {
                Grid.grid[posizioni[i][0], posizioni[i][1]] = 1;
            }
            Program.Disegna ();
        } // End Aggiorna

        public void Cade () {
            if (ceQualcosaSotto ()) {
                for (int i = 0; i < 4; i++) {
                    Grid.gridTetraminiCaduti[posizioni[i][0], posizioni[i][1]] = 1;
                }
                Program.caduto = true;
            } else {
                for (int numCount = 0; numCount < 4; numCount++) {
                    // Sta cadendo
                    posizioni[numCount][0] += 1;
                }
                Aggiorna ();
            }
        } // End Cade

        public bool ceQualcosaSotto () {
            for (int i = 0; i < 4; i++) {
                if (posizioni[i][0] + 1 >= 23)
                    return true;
                if (posizioni[i][0] + 1 < 23) {
                    if (Grid.gridTetraminiCaduti[posizioni[i][0] + 1, posizioni[i][1]] == 1) {
                        return true;
                    }
                }
            }
            return false;
        } // End ceQualcosaSotto
        public bool ceQualcosaSx () {
            for (int i = 0; i < 4; i++) {
                if (posizioni[i][1] == 0) {
                    return true;
                } else if (Grid.gridTetraminiCaduti[posizioni[i][0], posizioni[i][1] - 1] == 1) {
                    return true;
                }
            }
            return false;
        }
        public bool ceQualcosaDx () {
            for (int i = 0; i < 4; i++) {
                if (posizioni[i][1] == 9) {
                    return true;
                } else if (Grid.gridTetraminiCaduti[posizioni[i][0], posizioni[i][1] + 1] == 1) {
                    return true;
                }
            }
            return false;
        }

        public bool? ceOverlaySotto (List<int[]> posizioni) {
            List<int> ycoords = new List<int> ();
            for (int i = 0; i < 4; i++) {
                ycoords.Add (posizioni[i][0]);
                if (posizioni[i][0] >= 23)
                    return true;
                if ((posizioni[i][0] < 0) || (posizioni[i][1] < 0) || (posizioni[i][1] > 9))
                    return null;
            }
            for (int i = 0; i < 4; i++) {
                if (ycoords.Max () - ycoords.Min () == 3) {
                    if (ycoords.Max () == posizioni[i][0] | ycoords.Max () - 1 == posizioni[i][0]) {
                        if (Grid.gridTetraminiCaduti[posizioni[i][0], posizioni[i][1]] == 1) {
                            return true;
                        }
                    }

                } else {
                    if (ycoords.Max () == posizioni[i][0]) {
                        if (Grid.gridTetraminiCaduti[posizioni[i][0], posizioni[i][1]] == 1) {
                            return true;
                        }
                    }
                }
            }

            return false;
        }
        public bool? ceOverlaySx (List<int[]> posizioni) {
            List<int> xcoords = new List<int> ();
            for (int i = 0; i < 4; i++) {
                xcoords.Add (posizioni[i][1]);
                if (posizioni[i][1] < 0) {
                    return true;
                }
                if ((posizioni[i][1] > 9) || (posizioni[i][0] >= 23) || (posizioni[i][0] < 0)) {
                    return false;
                }
            }
            for (int i = 0; i < 4; i++) {
                if (xcoords.Max () - xcoords.Min () == 3) {
                    if (xcoords.Min () == posizioni[i][1] | xcoords.Min () + 1 == posizioni[i][1]) {
                        if (Grid.gridTetraminiCaduti[posizioni[i][0], posizioni[i][1]] == 1) {
                            return true;
                        }
                    }

                } else {
                    if (xcoords.Min () == posizioni[i][1]) {
                        if (Grid.gridTetraminiCaduti[posizioni[i][0], posizioni[i][1]] == 1) {
                            return true;
                        }
                    }
                }
            }
            return false;
        }
        public bool? ceOverlayDx (List<int[]> posizioni) {
            List<int> xcoords = new List<int> ();
            for (int i = 0; i < 4; i++) {
                xcoords.Add (posizioni[i][1]);
                if (posizioni[i][1] > 9) {
                    return true;
                }
                if (posizioni[i][1] < 0) {
                    return false;
                }
                if ((posizioni[i][0] >= 23) || (posizioni[i][0] < 0))
                    return null;
            }
            for (int i = 0; i < 4; i++) {
                if (xcoords.Max () - xcoords.Min () == 3) {
                    if (xcoords.Max () == posizioni[i][1] | xcoords.Max () - 1 == posizioni[i][1]) {
                        if (Grid.gridTetraminiCaduti[posizioni[i][0], posizioni[i][1]] == 1) {
                            return true;
                        }
                    }

                } else {
                    if (xcoords.Max () == posizioni[i][1]) {
                        if (Grid.gridTetraminiCaduti[posizioni[i][0], posizioni[i][1]] == 1) {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        public void Rotazione () {
            List<int[]> tempPos = new List<int[]> ();
            int[] pivot = posizioni[2];
            String direzione = "orario";
            int count = 0;

            if (tetramino != tetramini[1]) {
                for (int i = 0; i < tetramino.GetLength (0); i++) {
                    for (int j = 0; j < tetramino.GetLength (1); j++) {
                        if (tetramino[i, j] == 1) {
                            tempPos.Add (new int[] { i, (10 - tetramino.GetLength (1)) / 2 + j });
                        }
                    }
                }

                if (tetramino == tetramini[0]) {
                    if (verticale) {
                        direzione = "antiorario";
                    }
                } else if (tetramino == tetramini[3]) {
                    pivot = posizioni[3];
                }
                for (int i = 0; i < posizioni.Count; i++) {
                    tempPos[i] = transformaMatrice (posizioni[i], pivot, direzione);
                }

                while (ceOverlaySx (tempPos) != false | ceOverlayDx (tempPos) != false | ceOverlaySotto (tempPos) != false) {
                    if (ceOverlaySx (tempPos) == true) {
                        for (int i = 0; i < posizioni.Count; i++) {
                            tempPos[i][1] += 1;
                        }
                    }

                    if (ceOverlayDx (tempPos) == true) {
                        for (int i = 0; i < posizioni.Count; i++) {
                            tempPos[i][1] -= 1;
                        }
                    }
                    if (ceOverlaySotto (tempPos) == true) {
                        for (int i = 0; i < posizioni.Count; i++) {
                            tempPos[i][0] -= 1;
                        }
                    }
                    if (count == 3) {
                        return;
                    }
                    count++;
                }

                posizioni = tempPos;
            }

        } // Rotazione
        public int[] transformaMatrice (int[] coord, int[] coordPivot, String senso) {
            int[] nuoveCoordinate = {
                coord[0] - coordPivot[0],
                coord[1] - coordPivot[1]
            };
            if (senso == "antiorario") {
                nuoveCoordinate = new int[] {-nuoveCoordinate[1], nuoveCoordinate[0] };
            } else if (senso == "orario") {
                nuoveCoordinate = new int[] { nuoveCoordinate[1], -nuoveCoordinate[0] };
            }

            nuoveCoordinate = new int[] {
                nuoveCoordinate[0] + coordPivot[0],
                nuoveCoordinate[1] + coordPivot[1]
            };
            return nuoveCoordinate;
        }
    } // End Classe
}