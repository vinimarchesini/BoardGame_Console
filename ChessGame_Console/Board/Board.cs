using System;
using System.Collections.Generic;
using System.Text;

namespace Board
{
    class Board
    {
        public int Lines { get; set; }
        public int Collums { get; set; }
        private Piece[,] pieces;

        public Board(int lines, int collums)
        {
            Lines = lines;
            Collums = collums;
            pieces = new Piece[lines, collums];
        }

        public Piece Piece(int line, int collum)
        {
            return pieces[line, collum];
        }
    }
}
