using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrisGame
{
    class GameManager
    {
        #region  Attributes
        private static Stopwatch timer;
        private static Stopwatch dropTimer;
        private static Stopwatch inputTimer;
        private static int dropTime;
        private static int dropRate;
        private static int points;
        private static int linesCleared;
        private static int level;
        #endregion

        #region Constructor
        public GameManager()
        {
            timer = new Stopwatch();
            dropTimer = new Stopwatch();
            inputTimer = new Stopwatch();
            points = 0;
            dropTime = 300;
            dropRate = 300;
            linesCleared = 0;
            level = 1;

        }
        #endregion

        #region Methods



        #endregion
    }
}
