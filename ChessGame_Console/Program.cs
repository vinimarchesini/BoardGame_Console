using System;
using Board;
using Board.Enums;
using Chess;

namespace ChessGame_Console
{
    class Program
    {
        static void Main(string[] args)
        {
            Board.Board b = new Board.Board(8, 8);
            b.InputPiece(new King(b, Enum.Parse<Color>("White")), new Position(0, 0));
            Screen.PrintBoard(b);
        }
    }
}
