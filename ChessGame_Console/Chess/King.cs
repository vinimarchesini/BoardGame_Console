using System;
using System.Collections.Generic;
using System.Text;
using Board;
using Board.Enums;

namespace Chess
{
    class King : Piece
    {
        public King(Board.Board board, Color color) : base(color, board)
        {
        }

        public override string ToString()
        {
            return "K";
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
            pos.DefineValues(Position.Line - 1, Position.Collum);
            if (Board.ValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Collum] = true;
            }

            //down
            pos.DefineValues(Position.Line + 1, Position.Collum);
            if (Board.ValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Collum] = true;
            }

            //left
            pos.DefineValues(Position.Line, Position.Collum - 1);
            if (Board.ValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Collum] = true;
            }

            //right
            pos.DefineValues(Position.Line, Position.Collum + 1);
            if (Board.ValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Collum] = true;
            }

            //udr
            pos.DefineValues(Position.Line - 1, Position.Collum + 1);
            if (Board.ValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Collum] = true;
            }

            //udl
            pos.DefineValues(Position.Line - 1, Position.Collum - 1);
            if (Board.ValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Collum] = true;
            }

            //ddl
            pos.DefineValues(Position.Line + 1, Position.Collum - 1);
            if (Board.ValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Collum] = true;
            }

            //ddr
            pos.DefineValues(Position.Line + 1, Position.Collum + 1);
            if (Board.ValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Collum] = true;
            }
            return mat;
        }
    }
}
