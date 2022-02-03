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

        public bool PieceExist(Position pos)
        {
            ValidatePosition(pos);
            return Piece(pos) != null;
        }

        public void InputPiece(Piece piece, Position pos)
        {
            if (PieceExist(pos))
            {
                throw new BoardException("Piece already exist in this position");
            }
            pieces[pos.Line, pos.Collum] = piece;
            piece.Position = pos;
        }

        public Piece WithDrawPiece(Position pos)
        {
            if (Piece(pos) == null)
            {
                return null;
            }
            Piece aux = Piece(pos);
            aux.Position = null;
            pieces[pos.Line, pos.Collum] = null;
            return aux;

        }
        public Piece Piece(int line, int collum)
        {
            return pieces[line, collum];
        }

        public Piece Piece(Position pos)
        {
            return pieces[pos.Line, pos.Collum];
        }

        public bool ValidPosition(Position pos)
        {
            if (pos.Line < 0 || pos.Line >= Lines || pos.Collum < 0 || pos.Collum >= Collums)
            {
                return false;
            }
            return true;
        }

        public void ValidatePosition(Position pos)
        {
            if (!ValidPosition(pos))
            {
                throw new BoardException("Board position is not valid!");
            }
        }
    }
}
