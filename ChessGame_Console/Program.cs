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
            try
            { 
            ChessMatch chessMatch = new ChessMatch();

                while(!chessMatch.Finished)
                {
                    Console.Clear();
                    Screen.PrintBoard(chessMatch.Board);
                    Console.WriteLine();
                    Console.Write("Origem: ");
                    Position origin = Screen.ReadChessPosition().ToPosition();
                    Console.Write("Destino: ");
                    Position destination = Screen.ReadChessPosition().ToPosition();
                    chessMatch.DoMovement(origin, destination);

                }

            } catch { }
        }
    }
}
