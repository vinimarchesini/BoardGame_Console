using System;
using Board;

namespace ChessGame_Console
{
    class Program
    {
        static void Main(string[] args)
        {
            Board.Board b = new Board.Board(8, 8);
            Screen.PrintBoard(b);
        }
    }
}
