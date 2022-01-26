using System;
using System.Collections.Generic;
using System.Text;
using Board;
using Board.Enums;

namespace Chess
{
    class ChessMatch
    {
        public Board.Board Board { get; set; }
        public int Shift { get; set; }
        public Color ActualPlayer { get; set; }
        public bool Finished { get; set; }

        public ChessMatch()
        {
            Board = new Board.Board(8, 8);
            Shift = 1;
            ActualPlayer = Color.White;
            InputPieces();
        }

        public void DoMovement(Position origin, Position destination)
        {
            Piece p = Board.WithDrawPiece(origin);
            p.IncrementQttMoves();
            Piece capturedPiece = Board.WithDrawPiece(destination);
            Board.InputPiece(p, destination);
        }

        private void InputPieces()
        {
            Board.InputPiece(new Rook(Board, Color.White), new ChessPosition('c', 1).ToPosition());
        }
    }
}
