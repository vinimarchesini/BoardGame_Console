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
            ChessMatch chessMatch = new ChessMatch();

            while (!chessMatch.Finished)
            {
                try
                {
                    Console.Clear();
                    Screen.PrintMatch(chessMatch);
                    Console.Write("Origin: ");
                    Position origin = Screen.ReadChessPosition().ToPosition();
                    chessMatch.ValidateOriginPosition(origin);
                    bool[,] possiblePositions = chessMatch.Board.Piece(origin).PossibleMovements();
                    Console.Clear();
                    Screen.PrintBoard(chessMatch.Board, possiblePositions);
                    Console.WriteLine();
                    Console.Write("Destination: ");
                    Position destination = Screen.ReadChessPosition().ToPosition();
                    chessMatch.ValidateDestinationPosition(origin, destination);
                    chessMatch.MakeMove(origin, destination);
                }
                catch (BoardException ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.ReadLine();
                }
            }
            Console.Clear();
            Screen.PrintMatch(chessMatch);


        }
    }
}
