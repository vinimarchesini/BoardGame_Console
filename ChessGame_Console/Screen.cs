using System;
using System.Collections.Generic;
using System.Text;
using Board;
using Board.Enums;
using Chess;

namespace ChessGame_Console
{
    class Screen
    {
        public static void PrintBoard(Board.Board board)
        {
            for (int i = 0; i < board.Lines; i++)
            {
                Console.Write(8 - i + " ");
                for (int j = 0; j < board.Collums; j++)
                {
                    PrintPiece(board.Piece(i, j));
                }
                Console.WriteLine();
            }
            Console.WriteLine("  a b c d e f g h");
        }

        public static void PrintMatch(ChessMatch chessMatch)
        {
            Screen.PrintBoard(chessMatch.Board);
            Console.WriteLine();
            PrintCapturedPieces(chessMatch);
            Console.WriteLine();
            Console.WriteLine($"Turn: {chessMatch.Turn}");
            Console.WriteLine($"it's the {chessMatch.ActualPlayer} player's turn");
            Console.WriteLine();
            if (chessMatch.Xeque)
            { 
                Console.WriteLine("XEQUE!"); 
            }
        }

        public static void PrintCapturedPieces(ChessMatch chessMatch)
        {
            Console.WriteLine("Captured Pieces: ");
            Console.Write("White: ");
            PrintSet(chessMatch.CapturedPieces(Color.White));
            Console.WriteLine();
            ConsoleColor aux = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("Black: ");
            PrintSet(chessMatch.CapturedPieces(Color.Black));
            Console.ForegroundColor = aux;
            Console.WriteLine();
        }

        public static void PrintSet(HashSet<Piece> set)
        {
            Console.Write("{");
            foreach (Piece x in set)
            {
                Console.Write(x + " ");
            }
            Console.Write("}");
        }

        public static void PrintBoard(Board.Board board, bool[,] possiblePositions)
        {
            ConsoleColor originalBgColor = Console.BackgroundColor;
            ConsoleColor changedBgColor = ConsoleColor.DarkGray;

            for (int i = 0; i < board.Lines; i++)
            {
                Console.Write(8 - i + " ");
                for (int j = 0; j < board.Collums; j++)
                {
                    if (possiblePositions[i, j])
                    {
                        Console.BackgroundColor = changedBgColor;
                    }
                    else
                    {
                        Console.BackgroundColor = originalBgColor;
                    }
                    PrintPiece(board.Piece(i, j));
                    Console.BackgroundColor = originalBgColor;
                }
                Console.WriteLine();
            }
            Console.WriteLine("  a b c d e f g h");
        }

        public static void PrintPiece(Piece piece)
        {
            if (piece == null)
            {
                Console.Write("- ");
            }
            else
            {
                if (piece.Color == Color.White)
                {
                    Console.Write(piece);
                }
                else
                {
                    ConsoleColor aux = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(piece);
                    Console.ForegroundColor = aux;
                }
                Console.Write(" ");
            }
        }

        public static ChessPosition ReadChessPosition()
        {
            string s = Console.ReadLine();
            char collum = s[0];
            int line = int.Parse(s[1] + "");
            return new ChessPosition(collum, line);
        }
    }
}
