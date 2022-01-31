using System;
using System.Collections.Generic;
using System.Text;
using Board;
using Board.Enums;

namespace Chess
{
    class Rook : Piece
    {
        public Rook(Board.Board board, Color color) : base(color, board)
        {
        }

        public override string ToString()
        {
            return "R";
        }

        private bool CanMove(Position pos)
        {
            Piece p = Board.Piece(pos);
            return p == null || p.Color != this.Color;
        }
        public override bool[,] PossibleMovements()
        {
            bool[,] mat = new bool[Board.Lines, Board.Collums];
            Position pos = new Position(0, 0);

            //up
            for (int i = Position.Line; i > 0; i--)
            {
                pos.DefineValues(i - 1, Position.Collum);
                if (Board.ValidPosition(pos) && CanMove(pos))
                {
                    mat[pos.Line, pos.Collum] = true;
                }
                else break;
            }

            //down
            for (int i = Position.Line; i < Board.Lines; i++)
            {
                pos.DefineValues(i + 1, Position.Collum);
                if (Board.ValidPosition(pos) && CanMove(pos))
                {
                    mat[pos.Line, pos.Collum] = true;
                }
                else break;
            }

            //Left
            for (int i = Position.Collum; i > 0; i--)
            {
                pos.DefineValues(Position.Line, i - 1);
                if (Board.ValidPosition(pos) && CanMove(pos))
                {
                    mat[pos.Line, pos.Collum] = true;
                }
                else break;
            }

            //Right
            for (int i = Position.Collum; i < Board.Collums; i++)
            {
                pos.DefineValues(Position.Line, i + 1);
                if (Board.ValidPosition(pos) && CanMove(pos))
                {
                    mat[pos.Line, pos.Collum] = true;
                }
                else break;
            }
            return mat;
        }
    }
}
