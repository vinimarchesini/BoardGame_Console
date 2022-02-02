using System;
using System.Collections.Generic;
using System.Text;
using Board;
using Board.Enums;

namespace Chess
{
    class Pawn : Piece
    {
        public Pawn(Board.Board board, Color color) : base(color, board)
        {
        }

        public override string ToString()
        {
            return "P";
        }
        public override bool[,] PossibleMovements()
        {
            bool[,] mat = new bool[Board.Lines, Board.Collums];
            Position actualPosition = new Position(Position.Line, Position.Collum);
            Position pos = new Position(0, 0);

            //up - Player One
            if (this.Color == Color.White)
            {
                //for move!
                pos.DefineValues(Position.Line - 1, Position.Collum);
                if (Board.ValidPosition(pos) && Board.Piece(pos) == null)
                {
                    mat[pos.Line, pos.Collum] = true;
                    if (QttMoves == 0)
                    {
                        pos.Line -= 1;
                        if (Board.ValidPosition(pos) && Board.Piece(pos) == null)
                        {
                            mat[pos.Line, pos.Collum] = true;
                        }
                    }
                }
                //for take!
                //left
                pos.DefineValues(Position.Line - 1, Position.Collum - 1);
                if (Board.ValidPosition(pos) && CanMove(pos) && OpponentPiece)
                {
                    mat[pos.Line, pos.Collum] = true;
                }
                //right
                pos.DefineValues(Position.Line - 1, Position.Collum + 1);
                if (Board.ValidPosition(pos) && CanMove(pos) && OpponentPiece)
                {
                    mat[pos.Line, pos.Collum] = true;
                }
            }

            //down - Player Two
            if (this.Color == Color.Black)
            {
                //for move!
                pos.DefineValues(Position.Line + 1, Position.Collum);
                if (Board.ValidPosition(pos) && Board.Piece(pos) == null)
                {
                    mat[pos.Line, pos.Collum] = true;
                    if (QttMoves == 0)
                    {
                        pos.Line += 1;
                        if (Board.ValidPosition(pos) && Board.Piece(pos) == null)
                        {
                            mat[pos.Line, pos.Collum] = true;
                        }
                    }
                }
                //for take!
                //left
                pos.DefineValues(Position.Line + 1, Position.Collum - 1);
                if (Board.ValidPosition(pos) && CanMove(pos) && OpponentPiece)
                {
                    mat[pos.Line, pos.Collum] = true;
                }
                //right
                pos.DefineValues(Position.Line + 1, Position.Collum + 1);
                if (Board.ValidPosition(pos) && CanMove(pos) && OpponentPiece)
                {
                    mat[pos.Line, pos.Collum] = true;
                }
            }

            return mat;
        }
    }
}
