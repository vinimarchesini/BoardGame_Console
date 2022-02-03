using System;
using System.Collections.Generic;
using System.Text;
using Board;
using Board.Enums;

namespace Chess
{
    class King : Piece
    {
        private ChessMatch chessMatch;
        public King(Board.Board board, Color color, ChessMatch chessMatch) : base(color, board)
        {
        }

        private bool TestRooktoRoque(Position pos)
        {
            Piece p = Board.Piece(pos);
            return p != null && p is Rook && p.Color == Color && p.QttMoves == 0;
        }

        public override string ToString()
        {
            return "K";
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

            // #Special move: Roque
            if (QttMoves == 0 && !chessMatch.Xeque)
            {
                // #Special move: Little Roque
                Position posT1 = new Position(Position.Line, Position.Collum + 3);
                if (TestRooktoRoque(posT1))
                {
                    Position p1 = new Position(Position.Line, Position.Collum + 1);
                    Position p2 = new Position(Position.Line, Position.Collum + 2);
                    if (Board.Piece(p1) == null && Board.Piece(p2) == null)
                    {
                        mat[Position.Line, Position.Collum + 2] = true;
                    }
                }
            }
            //Special move: Big Roque
            Position posT2 = new Position(Position.Line, Position.Collum - 4);
            if (TestRooktoRoque(posT2))
            {
                Position p1 = new Position(Position.Line, Position.Collum - 1);
                Position p2 = new Position(Position.Line, Position.Collum - 2);
                Position p3 = new Position(Position.Line, Position.Collum - 3);
                if (Board.Piece(p1) == null && Board.Piece(p2) == null && Board.Piece(p3) == null)
                {
                    mat[Position.Line, Position.Collum - 2] = true;
                }

            }
            return mat;
        }
    }
}
