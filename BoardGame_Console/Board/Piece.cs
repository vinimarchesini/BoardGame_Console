using System;
using System.Collections.Generic;
using System.Text;
using Board.Enums;

namespace Board
{
    abstract class Piece
    {
        public Position Position { get; set; }
        public Color Color { get; protected set; }
        public int QttMoves { get; protected set; }
        public Board Board { get; set; }
        public bool OpponentPiece { get; set; }

        public Piece(Color color, Board board)
        {
            Position = null;
            Color = color;
            QttMoves = 0;
            Board = board;
        }

        public void IncrementQttMoves()
        {
            QttMoves++;
        }

        public void DecrementQttMoves()
        {
            QttMoves--;
        }

        public bool ExistPossibleMovements()
        {
            bool[,] mat = PossibleMovements();
            for (int i = 0; i < Board.Lines; i++)
            {
                for (int j = 0; j < Board.Collums; j++)
                {
                    if (mat[i, j] == true)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        protected bool CanMove(Position pos)
        {
            Piece p = Board.Piece(pos);
            if (p != null && p.Color != this.Color)
            {
                OpponentPiece = true;
            } else { OpponentPiece = false; }
            return p == null || p.Color != this.Color;
        }

        public bool CanMoveTo(Position pos)
        {
            return PossibleMovements()[pos.Line, pos.Collum];
        }
        public abstract bool[,] PossibleMovements();

    }
}
