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

        public void InputPiece(Piece piece, Position pos)
        {
            pieces[pos.Line, pos.Collum] = piece;
            piece.Position = pos;
        }
        public Piece Piece(int line, int collum)
        {
            return pieces[line, collum];
        }
    }
}
