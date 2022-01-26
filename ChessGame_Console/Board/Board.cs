using System;
using System.Collections.Generic;
using System.Text;

namespace Board
{
    class Board
    {
        public int Lines { get; set; }
        public int Collums { get; set; }
        private Piece[,] Pieces { get; set; }

        public Board(int lines, int collums)
        {
            Lines = lines;
            Collums = collums;
            Pieces = new Piece[lines, collums];
        }
    }
}
