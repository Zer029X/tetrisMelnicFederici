using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tetrisMelnicFederici {
    public class Tetramino {

        public List<int[]> posizioni;
        private int[, ] matriceTetramino;
        private bool verticale;
        private bool caduto;
        private Grid griglia;
        private Grid grigliaTetraminiCaduti;
        private char lettera;

        public Tetramino (char lettera, Grid griglia, Grid grigliaTetraminiCaduti) {
            caduto = false;
            verticale = false;
            posizioni = new List<int[]> ();
            this.griglia = griglia;
            this.grigliaTetraminiCaduti = grigliaTetraminiCaduti;
            this.lettera = lettera;
            assegnaTetramino ();
        } // End Costruttore

        private void assegnaTetramino () {

            switch (lettera) {
                case 'I':
                    matriceTetramino = new int[1, 4] { { 1, 1, 1, 1 } };
                    break;
                case 'O':
                    matriceTetramino = new int[2, 2] { { 1, 1 }, { 1, 1 } };
                    break;
                case 'T':
                    matriceTetramino = new int[2, 3] { { 0, 1, 0 }, { 1, 1, 1 } };
                    break;
                case 'S':
                    matriceTetramino = new int[2, 3] { { 0, 1, 1 }, { 1, 1, 0 } };
                    break;
                case 'Z':
                    matriceTetramino = new int[2, 3] { { 1, 1, 0 }, { 0, 1, 1 } };
                    break;
                case 'J':
                    matriceTetramino = new int[2, 3] { { 1, 0, 0 }, { 1, 1, 1 } };
                    break;
                case 'L':
                    matriceTetramino = new int[2, 3] { { 0, 0, 1 }, { 1, 1, 1 } };
                    break;

            }
        }
        public void Spawn () {
            for (int i = 0; i < matriceTetramino.GetLength (0); i++) {
                for (int j = 0; j < matriceTetramino.GetLength (1); j++) {
                    if (matriceTetramino[i, j] == 1) {
                        posizioni.Add (new int[] { i, (10 - matriceTetramino.GetLength (1)) / 2 + j });
                    }
                }
            }
            Aggiorna ();
        } // End Spawn

        public void Aggiorna () {
            for (int i = 0; i < 23; i++) {
                for (int j = 0; j < 10; j++) {
                    griglia.setElementoMatrice (i, j, 0);
                }
            }
            for (int i = 0; i < 4; i++) {
                griglia.setElementoMatrice (posizioni[i][0], posizioni[i][1], 1);
            }
        } // End Aggiorna

        public void Cade () {
            if (ceQualcosaSotto ()) {
                for (int i = 0; i < 4; i++) {
                    grigliaTetraminiCaduti.setElementoMatrice (posizioni[i][0], posizioni[i][1], 1);
                }
                caduto = true;
            } else {
                caduto = false;
                for (int numCount = 0; numCount < 4; numCount++) {
                    // Sta cadendo
                    posizioni[numCount][0] += 1;

                }
                Aggiorna ();
            }
        } // End Cade
        
        public bool checkCaduto(){
            return caduto;
        }
        public bool ceQualcosaSotto () {
            for (int i = 0; i < 4; i++) {
                if (posizioni[i][0] + 1 >= 23)
                    return true;
                if (posizioni[i][0] + 1 < 23) {
                    if (grigliaTetraminiCaduti.getElementoMatrice (posizioni[i][0] + 1, posizioni[i][1]) == 1) {
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
                } else if (grigliaTetraminiCaduti.getElementoMatrice (posizioni[i][0], posizioni[i][1] - 1) == 1) {
                    return true;
                }
            }
            return false;
        }
        public bool ceQualcosaDx () {
            for (int i = 0; i < 4; i++) {
                if (posizioni[i][1] == 9) {
                    return true;
                } else if (grigliaTetraminiCaduti.getElementoMatrice (posizioni[i][0], posizioni[i][1] + 1) == 1) {
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
                        if (grigliaTetraminiCaduti.getElementoMatrice (posizioni[i][0], posizioni[i][1]) == 1) {
                            return true;
                        }
                    }

                } else {
                    if (ycoords.Max () == posizioni[i][0]) {
                        if (grigliaTetraminiCaduti.getElementoMatrice (posizioni[i][0], posizioni[i][1]) == 1) {
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
                        if (grigliaTetraminiCaduti.getElementoMatrice (posizioni[i][0], posizioni[i][1]) == 1) {
                            return true;
                        }
                    }

                } else {
                    if (xcoords.Min () == posizioni[i][1]) {
                        if (grigliaTetraminiCaduti.getElementoMatrice (posizioni[i][0], posizioni[i][1]) == 1) {
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
                        if (grigliaTetraminiCaduti.getElementoMatrice (posizioni[i][0], posizioni[i][1]) == 1) {
                            return true;
                        }
                    }

                } else {
                    if (xcoords.Max () == posizioni[i][1]) {
                        if (grigliaTetraminiCaduti.getElementoMatrice (posizioni[i][0], posizioni[i][1]) == 1) {
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
            if (lettera != 'O') {
                for (int i = 0; i < matriceTetramino.GetLength (0); i++) {
                    for (int j = 0; j < matriceTetramino.GetLength (1); j++) {
                        if (matriceTetramino[i, j] == 1) {
                            tempPos.Add (new int[] { i, (10 - matriceTetramino.GetLength (1)) / 2 + j });
                        }
                    }
                }
                switch (lettera) {
                    case 'I':
                        if (verticale) {
                            direzione = "antiorario";
                        }
                        break;

                    case 'S':
                        pivot = posizioni[3];
                        break;

                    default:
                        break;

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
        private int[] transformaMatrice (int[] coord, int[] coordPivot, String senso) {
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
        public int[, ] getMatriceTetramino(){
            return matriceTetramino;
        }
    }

}