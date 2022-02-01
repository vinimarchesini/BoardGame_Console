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

        public bool Xeque { get; private set; }

        public ChessMatch()
        {
            Board = new Board.Board(8, 8);
            Turn = 1;
            ActualPlayer = Color.White;
            Pieces = new HashSet<Piece>();
            Captureds = new HashSet<Piece>();
            Xeque = false;
            InputPieces();
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

        private Color Opponent(Color color)
        {
            if (color == Color.White)
            {
                return Color.Black;
            }
            return Color.White;
        }

        private Piece King(Color color)
        {
            foreach(Piece x in PiecesInGame(color))
            {
                if (x is King)
                {
                    return x;
                }
            }
            return null;
        }

        public bool IsinXeque(Color color)
        {
            Piece king = King(color);
            if (king is null)
            {
                throw new BoardException("King doesn't exist in Board!");
            }
            foreach (Piece x in PiecesInGame(Opponent(color)))
            {
                bool[,] mat = x.PossibleMovements();
                if (mat[king.Position.Line, king.Position.Collum])
                {
                    return true;
                }
            }
            return false;
        }

        public bool TestXequeMate(Color color)
        {
            if (!IsinXeque(color))
            { 
                return false; 
            }   
            
            foreach(Piece x in PiecesInGame(color))
            {
                bool[,] mat = x.PossibleMovements();
                for(int i = 0; i < Board.Lines; i++)
                {
                    for (int j = 0; j < Board.Collums; j++)
                    {
                        if (mat[i, j])
                        {
                            Position origin = x.Position;
                            Position destination = new Position(i, j);
                            Piece capturedPiece = DoMovement(origin, destination);
                            bool testXeque = IsinXeque(color);
                            UndoMovement(origin, destination, capturedPiece);
                            if (!testXeque)
                            {
                                return false;
                            }

                        }
                    }
                }
            }
            return true;
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

        public Piece DoMovement(Position origin, Position destination)
        {
            Piece p = Board.WithDrawPiece(origin);
            p.IncrementQttMoves();
            Piece capturedPiece = Board.WithDrawPiece(destination);
            Board.InputPiece(p, destination);
            if (capturedPiece != null)
            {
                Captureds.Add(capturedPiece);
            }
            return capturedPiece;
        }

        public void MakeMove(Position origin, Position destination)
        {
            Piece capturedPiece = DoMovement(origin, destination);
            if (IsinXeque(ActualPlayer))
            {
                UndoMovement(origin, destination, capturedPiece);
                throw new BoardException("You cannot put yourself in Xeque!");
            }
            if (IsinXeque(Opponent(ActualPlayer)))
            {
                Xeque = true;
            }
            else
            {
                Xeque = false;
            }

            if (TestXequeMate(Opponent(ActualPlayer)))
            {
                Finished = true;
            }
            Turn++;
            ChangePlayer();
        }

        public void UndoMovement(Position origin, Position destination, Piece capturedPiece)
        {
            Piece p = Board.WithDrawPiece(destination);
            p.DecrementQttMoves();
            Board.InputPiece(p, origin);
            if (capturedPiece != null)
            {
                Board.InputPiece(capturedPiece, destination);
                Captureds.Remove(capturedPiece);
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
            InputNewPieces('c', 1, new King(Board, Color.White));
            InputNewPieces('c', 2, new Rook(Board, Color.White));
            InputNewPieces('h', 7, new Rook(Board, Color.White));
            InputNewPieces('a', 8, new King(Board, Color.Black));
            InputNewPieces('b', 8, new Rook(Board, Color.Black));
        }
    }
}
