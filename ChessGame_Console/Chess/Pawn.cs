using System;
using System.Collections.Generic;
using System.Text;
using Board;
using Board.Enums;

namespace Chess
{
    class Pawn : Piece
    {
        private ChessMatch chessMatch;
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
                // #special move en passant
                if (Position.Line == 3)
                {
                    Position left = new Position(Position.Line, Position.Collum - 1);
                    if (Board.ValidPosition(left) && CanMove(left) && OpponentPiece && Board.Piece(left) == chessMatch.VulnerableEnPassant)
                    {
                        mat[left.Line - 1, left.Collum] = true;
                    }
                    Position right = new Position(Position.Line, Position.Collum + 1);
                    if (Board.ValidPosition(right) && CanMove(right) && OpponentPiece && Board.Piece(right) == chessMatch.VulnerableEnPassant)
                    {
                        mat[right.Line - 1, right.Collum] = true;
                    }
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

                // #special move en passant
                if (Position.Line == 4)
                {
                    Position left = new Position(Position.Line, Position.Collum - 1);
                    if (Board.ValidPosition(left) && CanMove(left) && OpponentPiece && Board.Piece(left) == chessMatch.VulnerableEnPassant)
                    {
                        mat[left.Line + 1, left.Collum] = true;
                    }
                    Position right = new Position(Position.Line, Position.Collum + 1);
                    if (Board.ValidPosition(right) && CanMove(right) && OpponentPiece && Board.Piece(right) == chessMatch.VulnerableEnPassant)
                    {
                        mat[right.Line + 1, right.Collum] = true;
                    }
                }
            }

            return mat;
        }
    }
}
