using System;
using System.Collections.Generic;
using System.Text;
using Board;
using Board.Enums;

namespace Chess
{
    class Knight : Piece
    {
        public Knight(Board.Board board, Color color) : base(color, board)
        {
        }

        public override string ToString()
        {
            return "N";
        }

        public override bool[,] PossibleMovements()
        {
            bool[,] mat = new bool[Board.Lines, Board.Collums];
            Position pos = new Position(0, 0);

            //lud1
            pos.DefineValues(Position.Line - 1, Position.Collum - 2);
            if (Board.ValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Collum] = true;
            }

            //lud2
            pos.DefineValues(Position.Line - 2, Position.Collum - 1);
            if (Board.ValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Collum] = true;
            }

            //rud1
            pos.DefineValues(Position.Line - 1, Position.Collum + 2);
            if (Board.ValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Collum] = true;
            }

            //rud2
            pos.DefineValues(Position.Line - 2, Position.Collum + 1);
            if (Board.ValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Collum] = true;
            }

            //ldd1
            pos.DefineValues(Position.Line + 1, Position.Collum - 2);
            if (Board.ValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Collum] = true;
            }

            //ldd2
            pos.DefineValues(Position.Line + 2, Position.Collum - 1);
            if (Board.ValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Collum] = true;
            }

            //rdd1
            pos.DefineValues(Position.Line + 1, Position.Collum + 2);
            if (Board.ValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Collum] = true;
            }

            //rdd2
            pos.DefineValues(Position.Line + 2, Position.Collum + 1);
            if (Board.ValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Collum] = true;
            }

            return mat;
        }
    }
}
