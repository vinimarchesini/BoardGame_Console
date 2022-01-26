using System;
using System.Collections.Generic;
using System.Text;
using Board.Enums;

namespace Board
{
    class Piece
    {
        public Position Position { get; set; }
        public Color Color { get; protected set; }
        public int QttMoves { get; protected set; }
        public Board Board { get; set; }

        public Piece(Position position, Color color, Board board)
        {
            Position = position;
            Color = color;
            QttMoves = 0;
            Board = board;
        }
    }
}
