using System;
using Board;
using Board.Enums;
using Chess;

namespace ChessGame_Console
{
    class Program
    {
        public static string Message { get; set; }
        static void Main(string[] args)
        {
            try
            {
                ChessMatch chessMatch = new ChessMatch();

                while (!chessMatch.Finished)
                {
                    Console.Clear();
                    Screen.PrintBoard(chessMatch.Board);
                    Console.WriteLine();
                    if (!(Message is null))
                    {
                        Console.WriteLine("Wrong Move!!");
                    }
                    Console.Write("Origem: ");
                    Position origin = Screen.ReadChessPosition().ToPosition();
                    Console.Write("Destino: ");
                    Position destination = Screen.ReadChessPosition().ToPosition();
                    if (chessMatch.Board.ValidPosition(origin) && chessMatch.Board.ValidPosition(destination))
                    {
                        chessMatch.DoMovement(origin, destination);
                        Message = null;
                    }
                    else
                    {
                        Message = "Wrong Move!!";
                    }
                }

            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
        }
    }
}
