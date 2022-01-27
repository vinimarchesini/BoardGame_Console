﻿using System;
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

        public abstract bool[,] PossibleMovements();

    }
}
