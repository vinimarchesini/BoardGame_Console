using System;
using System.Collections.Generic;
using System.Text;
using Board;
using Board.Enums;

namespace Chess
{
    class ChessMatch
    {
        public Board.Board Board { get; private set; }
        public int Turn { get; private set; }
        public Color ActualPlayer { get; private set; }
        public bool Finished { get; private set; }
        public HashSet<Piece> Pieces { get; private set; }
        public HashSet<Piece> Captureds { get; private set; }

        public ChessMatch()
        {
            Board = new Board.Board(8, 8);
            Turn = 1;
            ActualPlayer = Color.White;
            Pieces = new HashSet<Piece>();
            Captureds = new HashSet<Piece>();
            InputPieces();
        }

        public void MakeMove(Position origin, Position destination)
        {
            DoMovement(origin, destination);
            Turn++;
            ChangePlayer();
        }

        public void ValidateOriginPosition(Position pos)
        {
            if (Board.Piece(pos) == null)
            {
                throw new BoardException("Does'n exist piece in that position!");
            }
            if (ActualPlayer != Board.Piece(pos).Color)
            {
                throw new BoardException("This piece is not yours!");
            }
            if (!Board.Piece(pos).ExistPossibleMovements())
            {
                throw new BoardException("Does'n exist possible movements for this Piece!");
            }
        }

        public void ValidateDestinationPosition(Position origin, Position destination)
        {
            if (!Board.Piece(origin).CanMoveTo(destination))
            {
                throw new BoardException("Destination Position is invalid!");
            }
        }
        public void ChangePlayer()
        {
            if (ActualPlayer == Color.White)
            {
                ActualPlayer = Color.Black;
            }
            else
            {
                ActualPlayer = Color.White;
            }
        }

        public void DoMovement(Position origin, Position destination)
        {
            Piece p = Board.WithDrawPiece(origin);
            p.IncrementQttMoves();
            Piece capturedPiece = Board.WithDrawPiece(destination);
            Board.InputPiece(p, destination);
            if (capturedPiece != null)
            {
                Captureds.Add(capturedPiece);
            }
        }

        public HashSet<Piece> CapturedPieces(Color color)
        {
            HashSet<Piece> aux = new HashSet<Piece>();
            foreach(Piece x in Captureds)
            {
                if(x.Color == color)
                {
                    aux.Add(x);
                }
            }
            return aux;
        }

        public HashSet<Piece> PiecesInGame(Color color)
        {
            HashSet<Piece> aux = new HashSet<Piece>();
            foreach (Piece x in Pieces)
            {
                if (x.Color == color)
                {
                    aux.Add(x);
                }
            }
            aux.ExceptWith(CapturedPieces(color));
            return aux;
        }

        private void InputNewPieces(char collum, int line, Piece piece)
        {
            Board.InputPiece(piece, new ChessPosition(collum, line).ToPosition());
            Pieces.Add(piece);
        }
        private void InputPieces()
        {
            InputNewPieces('c', 1, new Rook(Board, Color.White));
            InputNewPieces('c', 8, new Rook(Board, Color.Black));
        }
    }
}
