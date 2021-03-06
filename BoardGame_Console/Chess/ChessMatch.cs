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
        public Piece VulnerableEnPassant { get; private set; }

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
            foreach (Piece x in PiecesInGame(color))
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

            foreach (Piece x in PiecesInGame(color))
            {
                bool[,] mat = x.PossibleMovements();
                for (int i = 0; i < Board.Lines; i++)
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

            // #jogadaespecial roque pequeno
            if (p is King && destination.Collum == origin.Collum + 2)
            {
                Position originT = new Position(origin.Line, origin.Collum + 3);
                Position destinationT = new Position(origin.Line, origin.Collum + 1);
                Piece T = Board.WithDrawPiece(originT);
                T.IncrementQttMoves();
                Board.InputPiece(T, destinationT);
            }

            // #jogadaespecial roque grande
            if (p is King && destination.Collum == origin.Collum - 2)
            {
                Position originT = new Position(origin.Line, origin.Collum - 4);
                Position destinationT = new Position(origin.Line, origin.Collum - 1);
                Piece T = Board.WithDrawPiece(originT);
                T.IncrementQttMoves();
                Board.InputPiece(T, destinationT);
            }

            // #jogadaespecial en passant
            if (p is Pawn)
            {
                if (origin.Collum != destination.Collum && capturedPiece == null)
                {
                    Position posP;
                    if (p.Color == Color.White)
                    {
                        posP = new Position(destination.Line + 1, destination.Collum);
                    }
                    else
                    {
                        posP = new Position(destination.Line - 1, destination.Collum);
                    }
                    capturedPiece = Board.WithDrawPiece(posP);
                    Captureds.Add(capturedPiece);
                }
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
            Piece p = Board.Piece(destination);

            // #special move promotion
            if (p is Pawn)
            {
                if ((p.Color == Color.White && destination.Line == 0) || (p.Color == Color.Black && destination.Line == 7))
                {
                    p = Board.WithDrawPiece(destination);
                    Pieces.Remove(p);
                    Piece rook = new Rook(Board, p.Color);
                    Board.InputPiece(rook, destination);
                    Pieces.Add(rook);
                }
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

            // #special move en passant
            if (p is Pawn && (destination.Line == origin.Line - 2 || destination.Line == origin.Line + 2))
            {
                VulnerableEnPassant = p;
            }
            else
            {
                VulnerableEnPassant = null;
            }
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
            // #jogadaespecial roque pequeno
            if (p is King && destination.Collum == origin.Collum + 2)
            {
                Position originT = new Position(origin.Line, origin.Collum + 3);
                Position destinationT = new Position(origin.Line, origin.Collum + 1);
                Piece T = Board.WithDrawPiece(destinationT);
                T.DecrementQttMoves();
                Board.InputPiece(T, originT);
            }

            // #jogadaespecial roque grande
            if (p is King && destination.Collum == origin.Collum - 2)
            {
                Position originT = new Position(origin.Line, origin.Collum - 4);
                Position destinationT = new Position(origin.Line, origin.Collum - 1);
                Piece T = Board.WithDrawPiece(destinationT);
                T.DecrementQttMoves();
                Board.InputPiece(T, originT);
            }

            // #jogadaespecial en passant
            if (p is Pawn)
            {
                if (origin.Collum != destination.Collum && capturedPiece == VulnerableEnPassant)
                {
                    Piece peao = Board.WithDrawPiece(destination);
                    Position posP;
                    if (p.Color == Color.White)
                    {
                        posP = new Position(3, destination.Collum);
                    }
                    else
                    {
                        posP = new Position(4, destination.Collum);
                    }
                    Board.InputPiece(peao, posP);
                }
            }
        }

        public HashSet<Piece> CapturedPieces(Color color)
        {
            HashSet<Piece> aux = new HashSet<Piece>();
            foreach (Piece x in Captureds)
            {
                if (x.Color == color)
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
            InputNewPieces('a', 1, new Rook(Board, Color.White));
            InputNewPieces('b', 1, new Knight(Board, Color.White));
            InputNewPieces('c', 1, new Bishop(Board, Color.White));
            InputNewPieces('d', 1, new Queen(Board, Color.White));
            InputNewPieces('e', 1, new King(Board, Color.White, this));
            InputNewPieces('f', 1, new Bishop(Board, Color.White));
            InputNewPieces('g', 1, new Knight(Board, Color.White));
            InputNewPieces('h', 1, new Rook(Board, Color.White));
            InputNewPieces('a', 2, new Pawn(Board, Color.White));
            InputNewPieces('b', 2, new Pawn(Board, Color.White));
            InputNewPieces('c', 2, new Pawn(Board, Color.White));
            InputNewPieces('d', 2, new Pawn(Board, Color.White));
            InputNewPieces('e', 2, new Pawn(Board, Color.White));
            InputNewPieces('f', 2, new Pawn(Board, Color.White));
            InputNewPieces('g', 2, new Pawn(Board, Color.White));
            InputNewPieces('h', 2, new Pawn(Board, Color.White));

            InputNewPieces('a', 8, new Rook(Board, Color.Black));
            InputNewPieces('b', 8, new Knight(Board, Color.Black));
            InputNewPieces('c', 8, new Bishop(Board, Color.Black));
            InputNewPieces('d', 8, new Queen(Board, Color.Black));
            InputNewPieces('e', 8, new King(Board, Color.Black, this));
            InputNewPieces('f', 8, new Bishop(Board, Color.Black));
            InputNewPieces('g', 8, new Knight(Board, Color.Black));
            InputNewPieces('h', 8, new Rook(Board, Color.Black));
            InputNewPieces('a', 7, new Pawn(Board, Color.Black));
            InputNewPieces('b', 7, new Pawn(Board, Color.Black));
            InputNewPieces('c', 7, new Pawn(Board, Color.Black));
            InputNewPieces('d', 7, new Pawn(Board, Color.Black));
            InputNewPieces('e', 7, new Pawn(Board, Color.Black));
            InputNewPieces('f', 7, new Pawn(Board, Color.Black));
            InputNewPieces('g', 7, new Pawn(Board, Color.Black));
            InputNewPieces('h', 7, new Pawn(Board, Color.Black));
        }
    }
}
