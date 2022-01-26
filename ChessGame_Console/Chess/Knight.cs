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
            return "H";
        }
    }
}
