using System;
using System.Collections.Generic;
using System.Text;
using Board;
using Board.Enums;

namespace Chess
{
    class Queen : Piece
    {
        public Queen(Board.Board board, Color color) : base(color, board)
        {
        }

        public override string ToString()
        {
            return "Q";
        }

        public override bool[,] PossibleMovements()
        {
            bool[,] mat = new bool[Board.Lines, Board.Collums];
            Position pos = new Position(0, 0);

            //lud
            pos.DefineValues(Position.Line - 1, Position.Collum - 1);
            while (Board.ValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Collum] = true;
                if (OpponentPiece)
                {
                    break;
                }
                pos.Line -= 1;
                pos.Collum -= 1;
            }

            //rud
            pos.DefineValues(Position.Line - 1, Position.Collum + 1);
            while (Board.ValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Collum] = true;
                if (OpponentPiece)
                {
                    break;
                }
                pos.Line -= 1;
                pos.Collum += 1;
            }

            //ldd
            pos.DefineValues(Position.Line + 1, Position.Collum - 1);
            while (Board.ValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Collum] = true;
                if (OpponentPiece)
                {
                    break;
                }
                pos.Line += 1;
                pos.Collum -= 1;
            }

            //rdd
            pos.DefineValues(Position.Line + 1, Position.Collum + 1);
            while (Board.ValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Collum] = true;
                if (OpponentPiece)
                {
                    break;
                }
                pos.Line += 1;
                pos.Collum += 1;
            }

            //up
            pos.DefineValues(Position.Line - 1, Position.Collum);
            while (Board.ValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Collum] = true;
                if (OpponentPiece)
                {
                    break;
                }
                pos.Line -= 1;
            }

            //down
            pos.DefineValues(Position.Line + 1, Position.Collum);
            while (Board.ValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Collum] = true;
                if (OpponentPiece)
                {
                    break;
                }
                pos.Line += 1;
            }

            //left
            pos.DefineValues(Position.Line, Position.Collum - 1);
            while (Board.ValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Collum] = true;
                if (OpponentPiece)
                {
                    break;
                }
                pos.Collum -= 1;
            }


            //right
            pos.DefineValues(Position.Line, Position.Collum + 1);
            while (Board.ValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Collum] = true;
                if (OpponentPiece)
                {
                    break;
                }
                pos.Collum += 1;
            }

            return mat;
        }
    }
}
